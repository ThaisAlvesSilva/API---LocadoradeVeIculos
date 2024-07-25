using System.Text.Json.Serialization;

namespace LocadoraVeiculos.Models {
	public class Reserva {

		public int Id { get; set; }
		public DateTime dtInicio { get; set; }
		public DateTime dtFim { get; set; }
		public double valor { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public StatusReserva? status { get; set; }
		public int ClienteID { get; set; }
		public Cliente? cliente { get; set; }
		public int VeiculoID { get; set; }
		public Veiculo? veiculo { get; set; }

		public Reserva() {
			status = StatusReserva.Andamento;
		}

	}
}
