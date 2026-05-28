using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSpaceAPI.Data;
using NutriSpaceAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NutriSpaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AstronautaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AstronautaController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE: Cadastrar um novo astronauta/operador no sistema
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Astronauta astronauta)
        {
            _context.Astronautas.Add(astronauta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = astronauta.IdAstronauta }, astronauta);
        }

        // READ ALL: Listar todos os astronautas da base espacial
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var astronautas = await _context.Astronautas.ToListAsync();
            return Ok(astronautas);
        }

        // READ BY ID: Buscar um astronauta específico e ver quais estufas ele monitora
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var astronauta = await _context.Astronautas
                .Include(a => a.Estufas) // JOIN automático com as Estufas sob sua responsabilidade
                .FirstOrDefaultAsync(a => a.IdAstronauta == id);

            if (astronauta == null) return NotFound(new { mensagem = "Astronauta não cadastrado." });
            return Ok(astronauta);
        }

        // UPDATE: Atualizar dados de contato ou cargo do astronauta
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Astronauta atualizado)
        {
            var astronautaBanco = await _context.Astronautas.FindAsync(id);
            if (astronautaBanco == null) return NotFound();

            astronautaBanco.Nome = atualizado.Nome;
            astronautaBanco.Cargo = atualizado.Cargo;
            astronautaBanco.Email = atualizado.Email; 
            astronautaBanco.Senha = atualizado.Senha; 

            _context.Astronautas.Update(astronautaBanco);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: Remover um astronauta do sistema
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var astronauta = await _context.Astronautas.FindAsync(id);
            if (astronauta == null) return NotFound();

            _context.Astronautas.Remove(astronauta);
            await _context.SaveChangesAsync();
            return Ok(new { mensagem = "Astronauta removido com sucesso." });
        }
    }
}