using System.ComponentModel.DataAnnotations;

namespace NutriSpaceAPI.DTOs
{
    public class LeituraSensorCreateDto
    {
        [Required(ErrorMessage = "O ID da estufa é obrigatório.")]
        public int IdEstufa { get; set; }

        [Required(ErrorMessage = "A temperatura lida é obrigatória.")]
        [Range(-50, 100, ErrorMessage = "Temperatura fora dos limites aceitáveis.")]
        public decimal TemperaturaLida { get; set; }

        [Required(ErrorMessage = "A umidade lida é obrigatória.")]
        [Range(0, 100, ErrorMessage = "A umidade deve ser entre 0% e 100%.")]
        public decimal UmidadeLida { get; set; }
    }
}