namespace LocadoraVeiculos.Models {
	public class Manutencao {

		public int Id { get; set; }
		public string Descricao { get; set; }
		public DateTime data { get; set; }
		public double custo { get; set; }
		public int VeiculoID { get; set; }
		public Veiculo? veiculo { get; set; }

	}
}
