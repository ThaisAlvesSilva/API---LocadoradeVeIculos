using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ManutencoesController : ControllerBase {
		private readonly ContextDB _context;

		public ManutencoesController(ContextDB context) {
			_context = context;
		}

		/// <summary>
		/// Lista todas as manutenções cadastradas.
		/// </summary>
		/// <returns>Uma lista de todas as manutenções cadastradas.</returns>
		/// <response code="200">Retorna a lista de manutenções cadastradas.</response>
		/// <response code="404">Se não houver manutenções cadastradas.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Manutencao>), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencao() {
			var manutencoes = await _context.Manutencao.Include(m => m.veiculo).ToListAsync();


			if (manutencoes == null || manutencoes.Count == 0) {
				return NotFound("Nenhuma manutenção cadastrada.");
			}

			return Ok(new { Message = "Manutenções cadastradas:", Data = manutencoes });
		}

		/// <summary>
		/// Obtém informações de uma manutenção específica.
		/// </summary>
		/// <param name="id">O ID da manutenção.</param>
		/// <returns>As informações da manutenção especificada.</returns>
		/// <response code="200">Retorna as informações da manutenção especificada.</response>
		/// <response code="404">Se a manutenção não for encontrada.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Manutencao), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<Manutencao>> GetManutencao(int id) {
			var manutencao = await _context.Manutencao
				.Include(m => m.veiculo)
				.FirstOrDefaultAsync(m => m.Id == id);
				
			if (manutencao == null) {
				return NotFound("Manutenção não encontrada com o id informado.");
			}

			return Ok(new { Message = "Manutenção encontrada:", Data = manutencao });
		}

		/// <summary>
		/// Atualiza as informações de uma manutenção existente.
		/// </summary>
		/// <param name="id">O ID da manutenção a ser atualizada.</param>
		/// <param name="manutencao">Os dados atualizados da manutenção.</param>
		/// <returns>A manutenção atualizada.</returns>
		/// <response code="200">Retorna a manutenção atualizada.</response>
		/// <response code="400">Se o ID da manutenção informado for inválido.</response>
		/// <response code="404">Se a manutenção não for encontrada.</response>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(Manutencao), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> PutManutencao(int id, Manutencao manutencao) {
			if (id != manutencao.Id) {
				return BadRequest("Os IDs da manutenção informados devem ser iguais.");
			}

			_context.Entry(manutencao).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!ManutencaoExists(id)) {
					return NotFound("Manutenção não encontrada.");
				} else {
					throw;
				}
			}

			return Ok(new { Message = "Manutenção atualizada com sucesso.", Data = manutencao });
		}

		/// <summary>
		/// Cria uma nova manutenção.
		/// </summary>
		/// <param name="manutencao">Os dados da nova manutenção.</param>
		/// <returns>A manutenção criada.</returns>
		/// <response code="201">Retorna a manutenção criada.</response>
		/// <response code="400">Se a criação da manutenção falhar.</response>
		[HttpPost]
		[ProducesResponseType(typeof(Manutencao), 201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<Manutencao>> PostManutencao(Manutencao manutencao) {
			_context.Manutencao.Add(manutencao);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetManutencao", new { id = manutencao.Id }, new { Message = "Manutenção criada com sucesso.", Data = manutencao });
		}

		/// <summary>
		/// Remove uma manutenção existente.
		/// </summary>
		/// <param name="id">O ID da manutenção a ser removida.</param>
		/// <returns>Uma mensagem indicando que a manutenção foi removida com sucesso.</returns>
		/// <response code="200">Retorna uma mensagem indicando que a manutenção foi removida com sucesso.</response>
		/// <response code="404">Se a manutenção não for encontrada.</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteManutencao(int id) {
			var manutencao = await _context.Manutencao.FindAsync(id);
			if (manutencao == null) {
				return NotFound("Manutenção não encontrada com o id informado.");
			}

			_context.Manutencao.Remove(manutencao);
			await _context.SaveChangesAsync();

			return Ok("Manutenção removida com sucesso.");
		}

		private bool ManutencaoExists(int id) {
			return _context.Manutencao.Any(e => e.Id == id);
		}

		/// <summary>
		/// Lista todas as manutenções de um veículo específico.
		/// </summary>
		/// <param name="VeiculoID">O ID do veículo.</param>
		/// <returns>As manutenções do veículo especificado.</returns>
		/// <response code="200">Retorna as manutenções do veículo especificado.</response>
		/// <response code="404">Se nenhuma manutenção for encontrada para o veículo especificado.</response>
		[HttpGet("{VeiculoID}/manutencoes")]
		[ProducesResponseType(typeof(IEnumerable<Manutencao>), 200)]
		[ProducesResponseType(404)]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<IEnumerable<Manutencao>>> GetManutencoesVeiculo(int VeiculoID) {
			var manutencoes = await _context.Manutencao.Where(m => m.VeiculoID == VeiculoID).ToListAsync();

			if (manutencoes == null || manutencoes.Count == 0) {
				return NotFound("Nenhuma manutenção encontrada");
			}

			return Ok(new { Message = "Manutenções encontradas:", Data = manutencoes });
		}
	}
}
