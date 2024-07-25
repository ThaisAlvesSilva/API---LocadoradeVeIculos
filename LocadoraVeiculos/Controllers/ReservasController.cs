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
	public class ReservasController : ControllerBase {
		private readonly ContextDB _context;

		public ReservasController(ContextDB context) {
			_context = context;
		}

		/// <summary>
		/// Lista todas as reservas cadastradas.
		/// </summary>
		/// <returns>Uma lista de todas as reservas cadastradas.</returns>
		/// <response code="200">Retorna a lista de reservas cadastradas.</response>
		/// <response code="404">Se não houver reservas cadastradas.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Reserva>), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<IEnumerable<Reserva>>> GetReserva() {
			var reservas = await _context.Reserva.ToListAsync();

			if (reservas == null || reservas.Count == 0) {
				return NotFound("Nenhuma reserva cadastrada.");
			}

			return Ok(new { Message = "Reservas cadastradas:", Data = reservas });
		}

		/// <summary>
		/// Obtém informações de uma reserva específica.
		/// </summary>
		/// <param name="id">O ID da reserva.</param>
		/// <returns>As informações da reserva especificada.</returns>
		/// <response code="200">Retorna as informações da reserva especificada.</response>
		/// <response code="404">Se a reserva não for encontrada.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Reserva), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<Reserva>> GetReserva(int id) {
			var reserva = await _context.Reserva.FindAsync(id);

			if (reserva == null) {
				return NotFound("Reserva não encontrada com o id informado.");
			}

			return Ok(new { Message = "Reserva encontrada:", Data = reserva });
		}

		/// <summary>
		/// Atualiza as informações de uma reserva existente.
		/// </summary>
		/// <param name="id">O ID da reserva a ser atualizada.</param>
		/// <param name="reserva">Os dados atualizados da reserva.</param>
		/// <returns>A reserva atualizada.</returns>
		/// <response code="200">Retorna a reserva atualizada.</response>
		/// <response code="400">Se o ID da reserva informado for inválido.</response>
		/// <response code="404">Se a reserva não for encontrada.</response>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(Reserva), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> PutReserva(int id, Reserva reserva) {
			if (id != reserva.Id) {
				return BadRequest("Os IDs da reserva informados devem ser iguais.");
			}

			_context.Entry(reserva).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!ReservaExists(id)) {
					return NotFound("Reserva não encontrada.");
				} else {
					throw;
				}
			}

			return Ok(new { Message = "Reserva atualizada com sucesso.", Data = reserva });
		}

		/// <summary>
		/// Atualiza o status da reserva para Cancelado
		/// </summary>
		/// <param name="veiculo">Id do veiculo.</param>
		/// <returns>O reserva atualizado.</returns>
		/// <response code="200">Retorna o reserva atualizado.</response>
		[HttpPut("cancelaReserva/{id}")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<IEnumerable<Reserva>>> atualizaStatusReserva(int id) {

			var reserva = await _context.Reserva.FindAsync(id);

			if (reserva == null) {
				return NotFound();
			}

			reserva.status = StatusReserva.Cancelado;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!ReservaExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}
			return await this.GetReservaCliente(reserva.ClienteID); //vai retornar os reservas já atualizados
		}

		/// <summary>
		/// Lista todas as reservas de um cliente específico.
		/// </summary>
		/// <param name="ClienteID">O ID do cliente.</param>
		/// <returns>As reservas do cliente especificado.</returns>
		/// <response code="200">Retorna as reservas do cliente especificado.</response>
		/// <response code="404">Se nenhuma reserva for encontrada para o cliente especificado.</response>
		[HttpGet("{ClienteID}/reservas")]
		[ProducesResponseType(typeof(IEnumerable<Reserva>), 200)]
		[ProducesResponseType(404)]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<IEnumerable<Reserva>>> GetReservaCliente(int ClienteID) {
			var reservas = await _context.Reserva.Where(r => r.ClienteID == ClienteID)
			.Include(r => r.veiculo).ToListAsync();

			if (reservas == null || reservas.Count == 0) {
				return NotFound("Nenhuma reserva encontrada");
			}

			var reservasComStatus = reservas.Select(reserva => new {
				reserva.Id,
				reserva.dtInicio,
				reserva.dtFim,
				reserva.valor,
				reserva.ClienteID,
				reserva.cliente,
				reserva.VeiculoID,
				reserva.veiculo,
				status = reserva.status.HasValue ? reserva.status.Value.ToString() : ""
			});

			return Ok(new { Message = "Reservas encontradas:", Data = reservasComStatus });
		}

		/// <summary>
		/// Cria uma nova reserva.
		/// </summary>
		/// <param name="reserva">Os dados da nova reserva.</param>
		/// <returns>A reserva criada.</returns>
		/// <response code="201">Retorna a reserva criada.</response>
		/// <response code="400">Se a criação da reserva falhar.</response>
		[HttpPost]
		[ProducesResponseType(typeof(Reserva), 201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva) {
			_context.Reserva.Add(reserva);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetReserva", new { id = reserva.Id }, new { Message = "Reserva criada com sucesso.", Data = reserva });
		}

		/// <summary>
		/// Remove uma reserva existente.
		/// </summary>
		/// <param name="id">O ID da reserva a ser removida.</param>
		/// <returns>Uma mensagem indicando que a reserva foi removida com sucesso.</returns>
		/// <response code="200">Retorna uma mensagem indicando que a reserva foi removida com sucesso.</response>
		/// <response code="404">Se a reserva não for encontrada.</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteReserva(int id) {
			var reserva = await _context.Reserva.FindAsync(id);
			if (reserva == null) {
				return NotFound("Reserva não encontrada com o id informado.");
			}

			_context.Reserva.Remove(reserva);
			await _context.SaveChangesAsync();

			return Ok("Reserva removida com sucesso.");
		}

		private bool ReservaExists(int id) {
			return _context.Reserva.Any(e => e.Id == id);
		}

		/// <summary>
		/// Lista todas as reservas de um veículo específico.
		/// </summary>
		/// <param name="VeiculoID">O ID do veículo.</param>
		/// <returns>As reservas do veículo especificado.</returns>
		/// <response code="200">Retorna as reservas do veículo especificado.</response>
		/// <response code="404">Se nenhuma reserva for encontrada para o veículo especificado.</response>
		[HttpGet("{VeiculoID}/reservasVeiculo")]
		[ProducesResponseType(typeof(IEnumerable<Reserva>), 200)]
		[ProducesResponseType(404)]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult<IEnumerable<Reserva>>> GetReservaByVeiculo(int VeiculoID) {
			var reservas = await _context.Reserva.Where(r => r.VeiculoID == VeiculoID).ToListAsync();

			if (reservas == null || reservas.Count == 0) {
				return NotFound("Nenhuma reserva encontrada");
			}

			return Ok(new { Message = "Reservas encontradas:", Data = reservas });
		}
	}
}
