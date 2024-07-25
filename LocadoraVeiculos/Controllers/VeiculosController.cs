using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly ContextDB _context;

        public VeiculosController(ContextDB context)
        {
            _context = context;
        }

		/// <summary>
		/// Lista todos os veículos cadastrados.
		/// </summary>
		/// <returns>Uma lista de todos os veículos cadastrados.</returns>
		/// <response code="200">Retorna a lista de veículos cadastrados.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Veiculo>), 200)]
		public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculo() {
			var veiculos = await _context.Veiculo
				.Include(v => v.manutencoes)
				.Include(v => v.reservas)
				.ToListAsync();

			if (veiculos == null || veiculos.Count == 0) {
				return NotFound("Nenhum veículo cadastrado.");
			}

			return Ok(new { Message = "Veículos cadastrados:", Data = veiculos });
		}

		/// <summary>
		/// Lista os veículos disponiveis.
		/// </summary>
		/// <returns>Uma lista de veículos disponiveis.</returns>
		/// <response code="200">Retorna a lista de veículos disponiveis.</response>
		[HttpGet("VeiculosDisponiveis")]
		[ProducesResponseType(typeof(IEnumerable<Veiculo>), 200)]
		public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculosDisponiveis() {
			var veiculos = await _context.Veiculo
				.Where(x => x.status == StatusVeiculo.Disponivel).ToListAsync();

			if (veiculos == null || veiculos.Count == 0) {
				return NotFound("Nenhum veículo cadastrado.");
			}

			return Ok(new { Message = "Veículos cadastrados:", Data = veiculos });
		}

		/// <summary>
		/// Obtém informações de um veículo específico.
		/// </summary>
		/// <param name="id">O ID do veículo.</param>
		/// <returns>As informações do veículo especificado.</returns>
		/// <response code="200">Retorna as informações do veículo especificado.</response>
		/// <response code="404">Se o veículo não for encontrado.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Veiculo), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<Veiculo>> GetVeiculo(int id) {
			var veiculo = await _context.Veiculo.FindAsync(id);

			if (veiculo == null) {
				return NotFound();
			}

			return Ok(new { Message = "Veículo encontrado:", Data = veiculo });
		}

		/// <summary>
		/// Atualiza as informações de um veículo existente.
		/// </summary>
		/// <param name="id">O ID do veículo a ser atualizado.</param>
		/// <param name="veiculo">Os dados atualizados do veículo.</param>
		/// <returns>O veículo atualizado.</returns>
		/// <response code="200">Retorna o veículo atualizado.</response>
		/// <response code="400">Se o ID do veículo informado for inválido.</response>
		/// <response code="404">Se o veículo não for encontrado.</response>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(Veiculo), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> PutVeiculo(int id, Veiculo veiculo) {
			if (id != veiculo.Id) {
				return BadRequest("Os IDs do veículo informados devem ser iguais");
			}

			_context.Entry(veiculo).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!VeiculoExists(id)) {
					return NotFound("Veículo não encontrado.");
				} else {
					throw;
				}
			}

			return Ok(new { Message = "Os dados do veículo foram atualizados com sucesso", Data = veiculo });
		}

		/// <summary>
		/// Atualiza o status do veiculo para Reservado
		/// </summary>
		/// <param name="veiculo">Id do veiculo.</param>
		/// <returns>O veículo atualizado.</returns>
		/// <response code="200">Retorna o veículo atualizado.</response>
		[HttpPut("atualizaStatus/{id}")]
		public async Task<ActionResult<IEnumerable<Veiculo>>> reservaVeiculo(int id) {

			var veiculo = await _context.Veiculo.FindAsync(id);

			if (veiculo == null) {
				return NotFound("Veículo não encontrado.");
			}

			veiculo.status = StatusVeiculo.Reservado;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!VeiculoExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}
			return Ok(new { Message = "Status do veiculo atualizado com sucesso", Data = veiculo });
		}

		/// <summary>
		/// Atualiza o status do veiculo para Disponivel
		/// </summary>
		/// <param name="veiculo">Id do veiculo.</param>
		/// <returns>O veículo atualizado.</returns>
		/// <response code="200">Retorna o veículo atualizado.</response>
		[HttpPut("atualizaStatusDisponivel/{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<IEnumerable<Veiculo>>> atualizaStatusVeiculo(int id) {

			var veiculo = await _context.Veiculo.FindAsync(id);

			if (veiculo == null) {
				return NotFound();
			}

			veiculo.status = StatusVeiculo.Disponivel;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!VeiculoExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}
			return await this.GetVeiculo(); //vai retornar os veiculos já atualizados
		}

		/// <summary>
		/// Cria um novo veículo.
		/// </summary>
		/// <param name="veiculo">Os dados do novo veículo.</param>
		/// <returns>O veículo criado.</returns>
		/// <response code="200">Retorna o veículo criado.</response>
		[HttpPost]
		[ProducesResponseType(typeof(Veiculo), 200)]
		public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo) {
			_context.Veiculo.Add(veiculo);
			await _context.SaveChangesAsync();

			return Ok(new { Message = "Veículo cadastrado com sucesso!", Data = veiculo });
		}

		/// <summary>
		/// Remove um veículo existente.
		/// </summary>
		/// <param name="id">O ID do veículo a ser removido.</param>
		/// <returns>Uma mensagem indicando que o veículo foi removido com sucesso.</returns>
		/// <response code="200">Retorna uma mensagem indicando que o veículo foi removido com sucesso.</response>
		/// <response code="404">Se o veículo não for encontrado.</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteVeiculo(int id) {
			var veiculo = await _context.Veiculo.FindAsync(id);
			if (veiculo == null) {
				return NotFound();
			}

			_context.Veiculo.Remove(veiculo);
			await _context.SaveChangesAsync();

			return Ok("Veículo deletado com sucesso!");
		}

		private bool VeiculoExists(int id)
        {
            return (_context.Veiculo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
