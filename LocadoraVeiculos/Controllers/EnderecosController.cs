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
	public class EnderecosController : ControllerBase {
		private readonly ContextDB _context;

		public EnderecosController(ContextDB context) {
			_context = context;
		}

		/// <summary>
		/// Lista todos os endereços cadastrados.
		/// </summary>
		/// <returns>Uma lista de todos os endereços cadastrados.</returns>
		/// <response code="200">Retorna a lista de endereços cadastrados.</response>
		/// <response code="404">Se não houver endereços cadastrados.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Endereco>), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<IEnumerable<Endereco>>> GetEndereco() {
			if (_context.Endereco == null || !_context.Endereco.Any()) {
				return NotFound("Nenhum endereço cadastrado.");
			}
			var enderecos = await _context.Endereco.
				Include(e => e.cliente).ToListAsync();


			return Ok(new { Message = "Endereços cadastrados:", Data = enderecos });
		}

		/// <summary>
		/// Obtém informações de um endereço específico.
		/// </summary>
		/// <param name="id">O ID do endereço.</param>
		/// <returns>As informações do endereço especificado.</returns>
		/// <response code="200">Retorna as informações do endereço especificado.</response>
		/// <response code="404">Se o endereço não for encontrado.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Endereco), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<Endereco>> GetEndereco(int id) {
			if (_context.Endereco == null) {
				return NotFound("Nenhum endereço cadastrado.");
			}
			var endereco = await _context.Endereco
								.Include(e => e.cliente)
								.FirstOrDefaultAsync(e => e.Id == id);

			if (endereco == null) {
				return NotFound("Endereço não encontrado com o id informado.");
			}

			return Ok(new { Message = "Endereço encontrado:", Data = endereco });
		}

		/// <summary>
		/// Atualiza as informações de um endereço existente.
		/// </summary>
		/// <param name="id">O ID do endereço a ser atualizado.</param>
		/// <param name="endereco">Os dados atualizados do endereço.</param>
		/// <returns>O endereço atualizado.</returns>
		/// <response code="200">Endereço atualizado com sucesso.</response>
		/// <response code="400">Se o ID do endereço informado for inválido.</response>
		/// <response code="404">Se o endereço não for encontrado.</response>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> PutEndereco(int id, Endereco endereco) {
			if (id != endereco.Id) {
				return BadRequest("Os IDs do endereço informados devem ser iguais.");
			}

			_context.Entry(endereco).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!EnderecoExists(id)) {
					return NotFound("Endereço não encontrado.");
				} else {
					throw;
				}
			}

			return Ok(new { Message = "Endereço atualizado com sucesso.", Data = endereco });
		}

		/// <summary>
		/// Cria um novo endereço.
		/// </summary>
		/// <param name="endereco">Os dados do novo endereço.</param>
		/// <returns>O endereço criado.</returns>
		/// <response code="201">Retorna o endereço criado.</response>
		/// <response code="400">Se a criação do endereço falhar.</response>
		[HttpPost]
		[ProducesResponseType(typeof(Endereco), 201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<Endereco>> PostEndereco(Endereco endereco) {
			if (_context.Endereco == null) {
				return Problem("Entity set 'ContextDB.Endereco' is null.");
			}
			_context.Endereco.Add(endereco);
			await _context.SaveChangesAsync();

			return Ok(new { Message = "Endereço cadastrado com sucesso.", Data = endereco });
		}

		/// <summary>
		/// Remove um endereço existente.
		/// </summary>
		/// <param name="id">O ID do endereço a ser removido.</param>
		/// <returns>Uma mensagem indicando que o endereço foi removido com sucesso.</returns>
		/// <response code="204">Endereço removido com sucesso.</response>
		/// <response code="404">Se o endereço não for encontrado.</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteEndereco(int id) {
			if (_context.Endereco == null) {
				return NotFound("Nenhum endereço cadastrado.");
			}
			var endereco = await _context.Endereco.FindAsync(id);
			if (endereco == null) {
				return NotFound("Endereço não encontrado.");
			}

			_context.Endereco.Remove(endereco);
			await _context.SaveChangesAsync();

			return Ok("Endereço removido com sucesso.");
		}

		private bool EnderecoExists(int id) {
			return (_context.Endereco?.Any(e => e.Id == id)).GetValueOrDefault();
		}

		/// <summary>
		/// Lista todos os endereços de um cliente específico.
		/// </summary>
		/// <param name="ClienteID">O ID do cliente.</param>
		/// <returns>As endereços do cliente especificado.</returns>
		/// <response code="200">Retorna as endereços do cliente especificado.</response>
		/// <response code="404">Se nenhum endereço for encontrada para o cliente especificado.</response>
		[HttpGet("{ClienteID}/enderecos")]
		[ProducesResponseType(typeof(IEnumerable<Reserva>), 200)]
		[ProducesResponseType(404)]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos(int ClienteID) {
			var enderecos = await _context.Endereco.Where(r => r.ClienteID == ClienteID).ToListAsync();

			if (enderecos == null || enderecos.Count == 0) {
				return NotFound("Nenhum endereco encontrado");
			}

			return Ok(new { Message = "Enderecos cadastrados:", Data = enderecos });
		}
	}
}
