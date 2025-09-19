using System.ComponentModel.DataAnnotations;

namespace LanchesApp.Models
{
    public class CarrinhoCompraIntem
    {
        public int CarrinhoCompraIntemId { get; set; }
        public Lanche Lanche { get; set; }
        public int Quantidade { get; set; }

        [StringLength(200)]
        public string  CarrinhoCompraId { get; set; }

    }
}
