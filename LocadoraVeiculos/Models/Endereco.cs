namespace LocadoraVeiculos.Models {
	public class Endereco {
		public int Id { get; set; }
		public string cep { get; set; }
		public string rua { get; set; }
		public int numero { get; set; }
		public string bairro { get; set; }
		public string cidade { get; set; }
		public string UF { get; set; }

		public int ClienteID { get; set; }
		public Cliente? cliente { get; set; }
	}
}
