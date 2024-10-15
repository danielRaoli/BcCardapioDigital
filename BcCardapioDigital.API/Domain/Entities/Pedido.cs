using BcCardapioDigital.API.Domain.Enums;
using System.Runtime.CompilerServices;

namespace BcCardapioDigital.API.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public string TelefoneCliente { get; set; } = string.Empty;
        public string NomeCliente { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public List<ItemPedido> Items { get; set; } = [];
        public Status Status { get; set; } = Status.Processando;
        public FormaDePagamento FormaDePagamento { get; set; }
        public string Code { get; private set; } = string.Empty;
        public DateTime Data { get; set; }
        public decimal TotalPrice => Items.Sum(i => i.PrecoUnitario * i.Quantidade );
       


        public void GerarCodigo()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

           
            var prefixo =  new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            Code =  prefixo + $"{this.Id}";
        }
    }
}
