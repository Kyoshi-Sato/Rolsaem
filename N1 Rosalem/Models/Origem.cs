using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BarApp.Models;

namespace N1_Rosalem.Models
{ 
        public class Origem
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int id { get; set; }

            [Required]
            [StringLength(255)]
            public string origem { get; set; }
            public ICollection<Bebida> Bebidas { get; set; }
    }
    
}
