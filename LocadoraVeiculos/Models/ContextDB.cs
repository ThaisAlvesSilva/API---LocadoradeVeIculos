using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Models {
	public class ContextDB : DbContext {

		public DbSet<Cliente> Cliente { get; set; }
		public DbSet<Manutencao> Manutencao { get; set; }
		public DbSet<Reserva> Reserva { get; set; }
		public DbSet<Veiculo> Veiculo { get; set; }
		public DbSet<Endereco> Endereco { get; set; }

		public ContextDB(DbContextOptions<ContextDB> options) : base(options) {

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Veiculo>()
				.Property(e => e.status)
				.HasConversion(
					v => v.ToString(),
					v => (StatusVeiculo)Enum.Parse(typeof(StatusVeiculo), v)
				);
			modelBuilder.Entity<Reserva>()
				.Property(e => e.status)
				.HasConversion(
					v => v.ToString(),
					v => (StatusReserva)Enum.Parse(typeof(StatusReserva), v)
				);
		}
	}
}
