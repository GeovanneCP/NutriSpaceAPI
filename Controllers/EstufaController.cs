using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSpaceAPI.Data;
using NutriSpaceAPI.Models;

namespace NutriSpaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstufaController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injeção de Dependência: Recebe o contexto do banco de dados configurado no Program.cs
        public EstufaController(AppDbContext context)
        {
            _context = context;
        }

        // 1. CREATE (POST): api/Estufa
        // Para o aplicativo mobile cadastrar uma nova estufa automatizada
        [HttpPost]
        public async Task<IActionResult> CriarEstufa([FromBody] Estufa estufa)
        {
            // Validação simples: Garante que o nome da estufa foi preenchido
            if (string.IsNullOrEmpty(estufa.NomeEstufa))
            {
                return BadRequest(new { mensagem = "O nome da estufa é obrigatório." });
            }

            _context.Estufas.Add(estufa);
            await _context.SaveChangesAsync(); // Grava fisicamente no Oracle

            // Retorna o status HTTP 201 (Created) e a URL para acessar este novo registro
            return CreatedAtAction(nameof(ObterPorId), new { id = estufa.IdEstufa }, estufa);
        }

        // 2. READ ALL (GET): api/Estufa
        // Usado na tela de "Visão Geral" do App Mobile para listar todas as estufas da base
        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var estufas = await _context.Estufas.ToListAsync();
            return Ok(estufas); // Retorna HTTP 200 com a lista em JSON
        }

        // 3. READ BY ID (GET): api/Estufa/{id}
        // Busca os detalhes específicos de uma estufa, incluindo suas leituras de sensores
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            // O "Include" faz um JOIN no banco, trazendo a Estufa junto com o histórico de leituras (1:N)
            var estufa = await _context.Estufas
                .Include(e => e.Leituras)
                .FirstOrDefaultAsync(e => e.IdEstufa == id);

            if (estufa == null)
            {
                return NotFound(new { mensagem = "Estufa espacial não encontrada." }); // HTTP 404
            }

            return Ok(estufa);
        }

        // 4. UPDATE (PUT): api/Estufa/{id}
        // Permite ao agrônomo/astronauta alterar o status ou nome da estufa (ex: desligar a bomba manualmente)
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarEstufa(int id, [FromBody] Estufa estufaAtualizada)
        {
            var estufaBanco = await _context.Estufas.FindAsync(id);
            if (estufaBanco == null) return NotFound();

            // Atualiza apenas os campos permitidos
            estufaBanco.NomeEstufa = estufaAtualizada.NomeEstufa;
            estufaBanco.StatusBomba = estufaAtualizada.StatusBomba;
            estufaBanco.IdAstronauta = estufaAtualizada.IdAstronauta;
            estufaBanco.IdPlanta = estufaAtualizada.IdPlanta;

            _context.Estufas.Update(estufaBanco);
            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204 (Sucesso, mas sem conteúdo no corpo da resposta)
        }

        // 5. DELETE: api/Estufa/{id}
        // Remove uma estufa do sistema
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarEstufa(int id)
        {
            var estufa = await _context.Estufas.FindAsync(id);
            if (estufa == null) return NotFound();

            _context.Estufas.Remove(estufa);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = $"Estufa {id} foi desativada e removida com sucesso." });
        }
    }
}