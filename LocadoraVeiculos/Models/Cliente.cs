namespace LocadoraVeiculos.Models {
	public class Cliente {
		public int Id { get; set; }
		public string nome { get; set; }
		public string telefone { get; set; }
		public string email { get; set;}
		public string senha { get; set; }
		public string cpf { get; set; }
		public List<Reserva>? reservas { get; set; }
		public List<Endereco>? enderecos { get; set; }
	}
}
