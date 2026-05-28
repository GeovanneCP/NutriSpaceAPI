using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSpaceAPI.Models
{
    [Table("TB_NS_ATRONAUTA")] 
    public class Astronauta
    {
        [Key]
        [Column("id_astronauta")]
        public int IdAstronauta { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("cargo")]
        public string Cargo { get; set; } = string.Empty;

        [Required]
        [MaxLength(60)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("senha")]
        public string Senha { get; set; } = string.Empty;

        // Propriedade de Navegação: Um astronauta pode monitorar várias estufas
        public ICollection<Estufa> Estufas { get; set; } = new List<Estufa>();
    }
}