using N1_Rosalem.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BarApp.Models
{
    public class Bebida
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        public int Estoque { get; set; }
        public string ImagemURL { get; set; }

        public int OrigemId { get; set; }
        public int ReceitaId { get; set; }

      
    }
}
