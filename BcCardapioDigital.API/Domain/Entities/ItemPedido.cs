namespace BcCardapioDigital.API.Domain.Entities
{
    public class ItemPedido
    {
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int PedidoId { get; set; }
        public  int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }


    }
}
