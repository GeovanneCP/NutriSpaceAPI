using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSpaceAPI.Data;
using NutriSpaceAPI.DTOs;
using NutriSpaceAPI.Models;

namespace NutriSpaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeituraSensorController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injeção de Dependência: O .NET passa o contexto do banco automaticamente para o construtor
        public LeituraSensorController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/LeituraSensor
        // Usado pelo dispositivo IoT (ESP32) para enviar os dados climáticos em tempo real
        [HttpPost]
        public async Task<IActionResult> CadastrarLeitura([FromBody] LeituraSensorCreateDto dto)
        {
           
            var estufa = await _context.Estufas
                .FirstOrDefaultAsync(e => e.IdEstufa == dto.IdEstufa);

            if (estufa == null)
            {
                return NotFound(new { mensagem = "Estufa não encontrada no sistema." });
            }

            // 2. Transforma o DTO em uma Entidade para salvar no banco
            var novaLeitura = new LeituraSensor
            {
                IdEstufa = dto.IdEstufa,
                TemperaturaLida = dto.TemperaturaLida,
                UmidadeLida = dto.UmidadeLida,
                DtHrLeitura = DateTime.Now
            };

            _context.LeiturasSensores.Add(novaLeitura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterLeituraPorId), new { id = novaLeitura.IdLeitor }, novaLeitura);
        }

        // GET: api/LeituraSensor/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterLeituraPorId(int id)
        {
            var leitura = await _context.LeiturasSensores.FindAsync(id);
            if (leitura == null) return NotFound();
            return Ok(leitura);
        }
    }
}