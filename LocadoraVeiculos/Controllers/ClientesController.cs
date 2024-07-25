using LocadoraVeiculos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/// <summary>
/// Controlador para lidar com operações relacionadas aos clientes.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase {
	private readonly ContextDB _context;

	public ClientesController(ContextDB context) {
		_context = context;
	}

	/// <summary>
	/// Lista todos os clientes cadastrados.
	/// </summary>
	/// <returns>Uma lista de todos os clientes cadastrados.</returns>
	/// <response code="200">Retorna a lista de clientes cadastrados.</response>
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<Cliente>), 200)]
	public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente() {
		var clientes = await _context.Cliente
			.Include(c => c.reservas)
			.Include(c => c.enderecos)
			.ToListAsync();

		if (clientes == null || clientes.Count == 0) {
			return NotFound("Nenhum cliente cadastrado.");
		}

		return Ok(new { Message = "Clientes cadastrados:", Data = clientes });
	}

	/// <summary>
	/// Obtém informações de um cliente específico.
	/// </summary>
	/// <param name="id">O ID do cliente.</param>
	/// <returns>As informações do cliente especificado.</returns>
	/// <response code="200">Retorna as informações do cliente especificado.</response>
	/// <response code="404">Se o cliente não for encontrado.</response>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(Cliente), 200)]
	[ProducesResponseType(404)]
	public async Task<ActionResult<Cliente>> GetCliente(int id) {
		var cliente = await _context.Cliente
			.Include(c => c.reservas) 
			.Include(c => c.enderecos) 
			.FirstOrDefaultAsync(c => c.Id == id);

		if (cliente == null) {
			return NotFound("Nenhum cliente encontrado com o id informado");
		}

		return Ok(new { Message = "Cliente cadastrado:", Data = cliente});
	}

	/// <summary>
	/// Realiza o login de um cliente.
	/// </summary>
	/// <param name="email">O e-mail do cliente.</param>
	/// <param name="senha">A senha do cliente.</param>
	/// <returns>As informações do cliente logado.</returns>
	/// <response code="200">Retorna as informações do cliente logado.</response>
	/// <response code="404">Se o e-mail ou a senha estiverem incorretos.</response>
	[HttpGet("login/{email}/{senha}")]
	[ProducesResponseType(typeof(Cliente), 200)]
	[ProducesResponseType(404)]
	public async Task<ActionResult<Cliente>> Login(string email, string senha) {
		var usuario = await _context.Cliente.FirstOrDefaultAsync(u => u.email == email && u.senha == senha);

		if (usuario == null) {
			return NotFound("Email ou senha incorretos");
		}

		return Ok(new { Message = "Login Realizado com sucesso!", Data = usuario });
	}

	/// <summary>
	/// Atualiza as informações de um cliente existente.
	/// </summary>
	/// <param name="id">O ID do cliente a ser atualizado.</param>
	/// <param name="cliente">Os dados atualizados do cliente.</param>
	/// <returns>O cliente atualizado.</returns>
	/// <response code="200">Retorna o cliente atualizado.</response>
	/// <response code="400">Se o ID do cliente informado for inválido.</response>
	/// <response code="404">Se o cliente não for encontrado.</response>
	[HttpPut("{id}")]
	[ProducesResponseType(typeof(Cliente), 200)]
	[ProducesResponseType(400)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> PutCliente(int id, Cliente cliente) {
		if (id != cliente.Id) {
			return BadRequest("Os IDs do cliente informados devem ser iguais");
		}

		_context.Entry(cliente).State = EntityState.Modified;

		try {
			await _context.SaveChangesAsync();
		} catch (DbUpdateConcurrencyException) {
			if (!ClienteExists(id)) {
				return NotFound("Cliente não encontrado.");
			} else {
				throw;
			}
		}

		return Ok(new { Message = "Dados do cliente atualizados com sucesso", Data = cliente });
	}

	/// <summary>
	/// Cria um novo cliente.
	/// </summary>
	/// <param name="cliente">Os dados do novo cliente.</param>
	/// <returns>O cliente criado.</returns>
	/// <response code="200">Retorna o cliente criado.</response>
	[HttpPost]
	[ProducesResponseType(typeof(Cliente), 200)]
	public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente) {
		_context.Cliente.Add(cliente);
		await _context.SaveChangesAsync();

		
		return Ok(new { Message = "Cliente cadastrado com sucesso!", Data = cliente });
	}

	/// <summary>
	/// Remove um cliente existente.
	/// </summary>
	/// <param name="id">O ID do cliente a ser removido.</param>
	/// <returns>Uma mensagem indicando que o cliente foi removido com sucesso.</returns>
	/// <response code="200">Retorna uma mensagem indicando que o cliente foi removido com sucesso.</response>
	/// <response code="404">Se o cliente não for encontrado.</response>
	[HttpDelete("{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> DeleteCliente(int id) {
		var cliente = await _context.Cliente.FindAsync(id);
		if (cliente == null) {
			return NotFound("Não existe nenhum cliente com esse Id");
		}

		_context.Cliente.Remove(cliente);
		await _context.SaveChangesAsync();

		return Ok("Cliente deletado com sucesso!");
	}

	private bool ClienteExists(int id) {
		return _context.Cliente.Any(e => e.Id == id);
	}
}