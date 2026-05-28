using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSpaceAPI.Data;
using NutriSpaceAPI.Models;

namespace NutriSpaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlantaController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE: Cadastrar uma nova espécie de cultivo botânico
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Planta planta)
        {
            _context.Plantas.Add(planta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = planta.IdPlanta }, planta);
        }

        // READ ALL: Listar todas as plantas catalogadas (Gabaritos de cultivo)
        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var plantas = await _context.Plantas.ToListAsync();
            return Ok(plantas);
        }

        // READ BY ID: Buscar os limites ideais de uma planta específica
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var planta = await _context.Plantas.FindAsync(id);
            if (planta == null) return NotFound(new { mensagem = "Espécie botânica não catalogada." });
            return Ok(planta);
        }

        // UPDATE: Alterar as faixas térmicas ou de umidade ideais da planta
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Planta atualizada)
        {
            var plantaBanco = await _context.Plantas.FindAsync(id);
            if (plantaBanco == null) return NotFound();

            plantaBanco.NomePlanta = atualizada.NomePlanta;
            plantaBanco.TempMinIdeal = atualizada.TempMinIdeal;
            plantaBanco.TempMaxIdeal = atualizada.TempMaxIdeal;
            plantaBanco.UmiMinIdeal = atualizada.UmiMinIdeal;

            _context.Plantas.Update(plantaBanco);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: Remover uma planta do catálogo
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var planta = await _context.Plantas.FindAsync(id);
            if (planta == null) return NotFound();

            _context.Plantas.Remove(planta);
            await _context.SaveChangesAsync();
            return Ok(new { mensagem = "Espécie botânica removida do catálogo." });
        }
    }
}