using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Enums;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BcCardapioDigital.API.Infrastructure
{
    public class RepositorioPedido(AppDbContext context) : IRepositorioPedido
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> AtualizarPedido(Pedido pedido)
        {
             _context.Pedidos.Update(pedido);

            var result = await _context.SaveChangesAsync();

            return result > 0;

        }

        public async Task<Pedido?> BuscarPedidoPorCodigo(string codigoPedido)
        {
            var pedido = await _context.Pedidos.Include(p => p.Items).ThenInclude(i => i.Produto).FirstOrDefaultAsync(p => p.Code == codigoPedido);

            return pedido;
        }

        public async Task<Pedido?> BuscarPedidoPorId(int pedidoId)
        { 
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == pedidoId);
            return pedido;
        }

        public async Task<List<Pedido>> BuscarPedidos(Status status, DateTime diaAtual)
        {
            var pedidos = await _context.Pedidos.Include(p => p.Items).Where(p => p.Status == status && p.Data.Date == diaAtual.Date).ToListAsync();
            return pedidos;
        }

        public async Task<List<Pedido>> BuscarPedidos()
        {
            DateTime inicioDoDia = DateTime.UtcNow.AddHours(-3).Date;
            DateTime fimDoDia = inicioDoDia.AddDays(1).AddTicks(-1);

            var pedidos = await _context.Pedidos
                .Where(p => (p.Status == Status.Finalizado || p.Status == Status.Cancelado)
                            && p.Data >= inicioDoDia
                            && p.Data <= fimDoDia)
                .ToListAsync();
            return pedidos;
        }

        public async Task<string> CriarPedido(Pedido pedido)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                _context.Pedidos.Add(pedido);

                
                await _context.SaveChangesAsync();

                pedido.GerarCodigo();
               
                foreach (var item in pedido.Items)
                {
                    item.PedidoId = pedido.Id;
                }

             
                await _context.SaveChangesAsync();

               
                await transaction.CommitAsync();

                return pedido.Code;
            }
            catch (Exception)
            {
               await transaction.RollbackAsync();
                throw; // Opcionalmente, relança a exceção
            }

        }



    }
}
