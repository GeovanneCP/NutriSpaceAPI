using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSpaceAPI.Models
{
    [Table("TB_NS_PLANTA")]
    public class Planta
    {
        [Key]
        [Column("id_planta")]
        public int IdPlanta { get; set; }

        [Required]
        [MaxLength(80)]
        [Column("nome_planta")]
        public string NomePlanta { get; set; } = string.Empty;

        [Required]
        [Column("temp_min_ideal")]
        public decimal TempMinIdeal { get; set; }

        [Required]
        [Column("temp_max_ideal")]
        public decimal TempMaxIdeal { get; set; }

        [Required]
        [Column("umi_min_ideal")]
        public decimal UmiMinIdeal { get; set; }

        // Propriedade de Navegação: Uma mesma espécie de planta pode ser cultivada em diferentes estufas
        public ICollection<Estufa> Estufas { get; set; } = new List<Estufa>();
    }
}