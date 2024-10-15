using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BcCardapioDigital.API.Application.Requests.Pedidos
{
    public record FazerPedidoRequest
    {
        [Required(ErrorMessage = "preencha os campos obrigatorios")]
        public FormaDePagamento FormaDePagamento { get; set; } = FormaDePagamento.NaEntrega;
        [Phone(ErrorMessage = "Insira um numero de telefone valido")]
        public string TelefoneCliente { get; set; } = string.Empty;
        public string NomeCliente { get; set; } = string.Empty;
        public List<ItemPedidoRequest> Items { get; set; } = [];
        public DateTime Data { get; set; }
        public Pedido ToEntity()
        {
            return new Pedido {TelefoneCliente = this.TelefoneCliente, NomeCliente = this.NomeCliente, FormaDePagamento = this.FormaDePagamento, Items = this.Items.Select(i => i.ToEntity()).ToList(), Data = DateTime.Now };
        }
    }
    public record ItemPedidoRequest
    {
        [Required(ErrorMessage = "preencha os campos obrigatorios")]
        [JsonIgnore]
        public decimal PrecoUnitario { get; set; }
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "preencha os campos obrigatorios")]
        public int Quantidade { get; set; }

        public ItemPedido ToEntity()
        {
            return new ItemPedido { PrecoUnitario = this.PrecoUnitario, ProdutoId = this.ProdutoId, Quantidade = this.Quantidade };
        }
    }
}

