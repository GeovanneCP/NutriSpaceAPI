using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSpaceAPI.Models
{
    [Table("TB_NS_LEITURA_SENSOR")]
    public class LeituraSensor
    {
        [Key]
        [Column("id_leitor")]
        public int IdLeitor { get; set; }

        [Required]
        [Column("id_estufa")]
        public int IdEstufa { get; set; }

        [Required]
        [Column("temperatura_lida")]
        public decimal TemperaturaLida { get; set; }

        [Required]
        [Column("umidade_lida")]
        public decimal UmidadeLida { get; set; }

        [Required]
        [Column("dt_hr_leitura")]
        public DateTime DtHrLeitura { get; set; }

        // Relacionamento: Cada leitura pertence a uma Estufa específica
        [ForeignKey("IdEstufa")]
        public Estufa? Estufa { get; set; }
    }
}