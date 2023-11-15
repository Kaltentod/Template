using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNA.IB.Calificaciones.API.Infrastructure.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BCRACalificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BCRACalificaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calificadoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaAltaBCRA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaBajaBCRA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificadoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalificadoraPeriodos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CalificadoraId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalificadoraPeriodos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalificadoraPeriodos_Calificadoras_CalificadoraId",
                        column: x => x.CalificadoraId,
                        principalTable: "Calificadoras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TituloPersonaCalificadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalificadoraId = table.Column<int>(type: "int", nullable: false),
                    BcraCalificacionId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TituloPersonaCalificadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TituloPersonaCalificadas_BCRACalificaciones_BcraCalificacionId",
                        column: x => x.BcraCalificacionId,
                        principalTable: "BCRACalificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TituloPersonaCalificadas_Calificadoras_CalificadoraId",
                        column: x => x.CalificadoraId,
                        principalTable: "Calificadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalificadoraPeriodoEquivalencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalificadoraPeriodoId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalificadoraPeriodoEquivalencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalificadoraPeriodoEquivalencias_CalificadoraPeriodos_CalificadoraPeriodoId",
                        column: x => x.CalificadoraPeriodoId,
                        principalTable: "CalificadoraPeriodos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Equivalencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BcraCalificacionId = table.Column<int>(type: "int", nullable: false),
                    CalificacionCalificadora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalificadoraPeriodoEquivalenciaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equivalencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equivalencias_BCRACalificaciones_BcraCalificacionId",
                        column: x => x.BcraCalificacionId,
                        principalTable: "BCRACalificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equivalencias_CalificadoraPeriodoEquivalencias_CalificadoraPeriodoEquivalenciaId",
                        column: x => x.CalificadoraPeriodoEquivalenciaId,
                        principalTable: "CalificadoraPeriodoEquivalencias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalificadoraPeriodoEquivalencias_CalificadoraPeriodoId",
                table: "CalificadoraPeriodoEquivalencias",
                column: "CalificadoraPeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_CalificadoraPeriodos_CalificadoraId",
                table: "CalificadoraPeriodos",
                column: "CalificadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Equivalencias_BcraCalificacionId",
                table: "Equivalencias",
                column: "BcraCalificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Equivalencias_CalificadoraPeriodoEquivalenciaId",
                table: "Equivalencias",
                column: "CalificadoraPeriodoEquivalenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_TituloPersonaCalificadas_BcraCalificacionId",
                table: "TituloPersonaCalificadas",
                column: "BcraCalificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_TituloPersonaCalificadas_CalificadoraId",
                table: "TituloPersonaCalificadas",
                column: "CalificadoraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equivalencias");

            migrationBuilder.DropTable(
                name: "TituloPersonaCalificadas");

            migrationBuilder.DropTable(
                name: "CalificadoraPeriodoEquivalencias");

            migrationBuilder.DropTable(
                name: "BCRACalificaciones");

            migrationBuilder.DropTable(
                name: "CalificadoraPeriodos");

            migrationBuilder.DropTable(
                name: "Calificadoras");
        }
    }
}
