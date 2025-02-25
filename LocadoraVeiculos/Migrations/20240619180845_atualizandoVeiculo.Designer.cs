﻿// <auto-generated />
using System;
using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LocadoraVeiculos.Migrations
{
    [DbContext(typeof(ContextDB))]
    [Migration("20240619180845_atualizandoVeiculo")]
    partial class atualizandoVeiculo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LocadoraVeiculos.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numero")
                        .HasColumnType("int");

                    b.Property<string>("rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteID");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Manutencao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VeiculoID")
                        .HasColumnType("int");

                    b.Property<double>("custo")
                        .HasColumnType("float");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("VeiculoID");

                    b.ToTable("Manutencao");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<int>("VeiculoID")
                        .HasColumnType("int");

                    b.Property<DateTime>("dtFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ClienteID");

                    b.HasIndex("VeiculoID");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quilometragem")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("valorAluguelDia")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Endereco", b =>
                {
                    b.HasOne("LocadoraVeiculos.Models.Cliente", "cliente")
                        .WithMany("enderecos")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cliente");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Manutencao", b =>
                {
                    b.HasOne("LocadoraVeiculos.Models.Veiculo", "veiculo")
                        .WithMany("manutencoes")
                        .HasForeignKey("VeiculoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("veiculo");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Reserva", b =>
                {
                    b.HasOne("LocadoraVeiculos.Models.Cliente", "cliente")
                        .WithMany("reservas")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LocadoraVeiculos.Models.Veiculo", "veiculo")
                        .WithMany("reservas")
                        .HasForeignKey("VeiculoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cliente");

                    b.Navigation("veiculo");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Cliente", b =>
                {
                    b.Navigation("enderecos");

                    b.Navigation("reservas");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Veiculo", b =>
                {
                    b.Navigation("manutencoes");

                    b.Navigation("reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
