using System.Text.Json.Serialization;

namespace LocadoraVeiculos.Models {
	public class Veiculo {

		public int Id { get; set; }
		public string placa { get; set; }
		public string modelo { get; set; }
		public string marca { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public StatusVeiculo? status { get; set; }  //RESERVADO, DISPONIVEL
		public double valorAluguelDia { get; set; }
		public int quilometragem { get; set; }

		public List<Manutencao>? manutencoes { get; set; }
		public List<Reserva>? reservas { get; set; }

		public Veiculo() {
			status = StatusVeiculo.Disponivel;
		}

	}
}
