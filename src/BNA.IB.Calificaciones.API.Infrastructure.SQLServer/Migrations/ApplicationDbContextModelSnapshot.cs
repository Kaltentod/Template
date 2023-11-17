﻿// <auto-generated />
using System;
using BNA.IB.Calificaciones.API.Infrastructure.SQLServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BNA.IB.Calificaciones.API.Infrastructure.SQLServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.BCRACalificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Calificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BCRACalificaciones");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.Calificadora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Clave")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Calificadoras");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraPeriodo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CalificadoraId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaAltaBCRA")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaBaja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaBajaBCRA")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CalificadoraId");

                    b.ToTable("CalificadoraPeriodos");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.Equivalencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BcraCalificacionId")
                        .HasColumnType("int");

                    b.Property<string>("CalificacionCalificadora")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CalificadoraPeriodoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BcraCalificacionId");

                    b.HasIndex("CalificadoraPeriodoId");

                    b.ToTable("Equivalencias");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.TituloPersonaCalificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BCRACalificacionId")
                        .HasColumnType("int");

                    b.Property<int>("CalificadoraPeriodoId")
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaBaja")
                        .HasColumnType("datetime2");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BCRACalificacionId");

                    b.HasIndex("CalificadoraPeriodoId");

                    b.ToTable("TituloPersonaCalificaciones");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraPeriodo", b =>
                {
                    b.HasOne("BNA.IB.Calificaciones.API.Domain.Entities.Calificadora", "Calificadora")
                        .WithMany("Periodos")
                        .HasForeignKey("CalificadoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calificadora");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.Equivalencia", b =>
                {
                    b.HasOne("BNA.IB.Calificaciones.API.Domain.Entities.BCRACalificacion", "BcraCalificacion")
                        .WithMany()
                        .HasForeignKey("BcraCalificacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraPeriodo", null)
                        .WithMany("Equivalencias")
                        .HasForeignKey("CalificadoraPeriodoId");

                    b.Navigation("BcraCalificacion");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.TituloPersonaCalificacion", b =>
                {
                    b.HasOne("BNA.IB.Calificaciones.API.Domain.Entities.BCRACalificacion", "BcraCalificacion")
                        .WithMany()
                        .HasForeignKey("BCRACalificacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraPeriodo", "CalificadoraPeriodo")
                        .WithMany()
                        .HasForeignKey("CalificadoraPeriodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BcraCalificacion");

                    b.Navigation("CalificadoraPeriodo");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.Calificadora", b =>
                {
                    b.Navigation("Periodos");
                });

            modelBuilder.Entity("BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraPeriodo", b =>
                {
                    b.Navigation("Equivalencias");
                });
#pragma warning restore 612, 618
        }
    }
}
