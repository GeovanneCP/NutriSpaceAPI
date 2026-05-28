using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSpaceAPI.Models
{
    [Table("TB_NS_ESTUFA")]
    public class Estufa
    {
        [Key]
        [Column("id_estufa")]
        public int IdEstufa { get; set; }

        [Required]
        [Column("id_astronauta")]
        public int IdAstronauta { get; set; }

        [Required]
        [Column("id_planta")]
        public int IdPlanta { get; set; }

        [Required]
        [MaxLength(60)]
        [Column("nome_estufa")]
        public string NomeEstufa { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("status_bomba")]
        public string StatusBomba { get; set; } = string.Empty;

        // Propriedade de Navegação: Diz para o .NET que uma Estufa possui muitas leituras
        public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();
    }
}