using BcCardapioDigital.API.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BcCardapioDigital.API.Application.Requests.Pedidos
{
    public record AtualizarStatusPedidoRequest
    {
        
        public int PedidoId  { get; set; }
        [Required(ErrorMessage = "Preencha os campos obrigatorios")]
        public Status StatusAtualizado { get; set; }
    }
}
