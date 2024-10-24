using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Pedidos;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BcCardapioDigital.API.Application.Services
{
    public class ServicoPedido(IRepositorioPedido repositorio, IRepositorioHorario repositorioHorario, IRepositorioProduto repositorioProduto) : IServicoPedido
    {
        private readonly IRepositorioPedido _repositorio = repositorio;
        private readonly IRepositorioHorario _repositorioHorario = repositorioHorario;
        private readonly IRepositorioProduto _repositorioProduto = repositorioProduto;

        public async Task<Response<Pedido>> AtualizarStatus(AtualizarStatusPedidoRequest request)
        {
            var pedido = await _repositorio.BuscarPedidoPorId(request.PedidoId) ?? throw new NotFoundException("Pedido nao encontrado");

            pedido.Status = request.StatusAtualizado;
            var result = await _repositorio.AtualizarPedido(pedido);

            return result ? new Response<Pedido>(null, 200, "Pedido atualizado com sucesso") : new Response<Pedido>(null, 500, "Nao foi possivel atualizar o status desse pedido no momento");

        }

        public async Task<Response<Pedido>> BuscarPedido(BuscarPedidoRequest request)
        {
            var pedido = await _repositorio.BuscarPedidoPorCodigo(request.CodigoProduto) ?? throw new NotFoundException("Pedido nao encontrado");

            return new Response<Pedido>(pedido);
        }

        public async Task<Response<List<Pedido>>> BuscarPedidos(BuscarPedidosDoDiaRequest request)
        {
            var pedidos = await _repositorio.BuscarPedidos(request.Status, request.DiaAtual);
            return new Response<List<Pedido>>(pedidos);
        }

        public async Task<Response<List<Pedido>>> BuscarPedidos()
        {
            var pedidos = await _repositorio.BuscarPedidos();
            return new Response<List<Pedido>>(pedidos);
        }

        public async Task<Response<string?>> FazerPedido(FazerPedidoRequest request)
        {
            request.Data = DateTime.Now;    
            var diaSemana = request.Data.DayOfWeek;
            var horarioFuncionamento = await _repositorioHorario.Buscar(diaSemana) ?? throw new NotFoundException("Erro ao consultar horario de funcionamento do estabelecimento, tente novamente");


            if (!horarioFuncionamento.Funcionando)
            {
                return new Response<string?>(null, 400, "O Restaurante não esta funcionando no momento");
            }

            if (request.Data.TimeOfDay < horarioFuncionamento.HoraAbertura || request.Data.TimeOfDay > horarioFuncionamento.HoraFechamento)
            {
                return new Response<string?>(null, 400, "O restaurante não esta funcionando no momento");
            }

            foreach (ItemPedidoRequest item in request.Items)
            {
                var produto = await _repositorioProduto.BuscarProduto(item.ProdutoId) ?? throw new NotFoundException("Algum produto escolhido não foi encontrado, verifique os produtos inseridos");

                item.PrecoUnitario = produto.Preco;
            }

            var entity = request.ToEntity();
            entity.GerarPreco();

            var result = await _repositorio.CriarPedido(entity);

            return result.IsNullOrEmpty() ? new Response<string?>(null, 500, "Nao foi possivel concluir o pedido no momento") : new Response<string?>(result, 200, "Pedido feito com sucesso");
        }
    }
}
