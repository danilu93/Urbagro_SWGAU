using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWGAU.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planta",
                columns: table => new
                {
                    PlantaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPlanta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombrePlanta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreCientifico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaSiembra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoSiembra = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planta", x => x.PlantaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContrasenaHash = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Abono",
                columns: table => new
                {
                    AbonoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantaId = table.Column<int>(type: "int", nullable: false),
                    FechaAbono = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrecuenciaAbono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoAbono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreAbono = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CantidadAbono = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abono", x => x.AbonoId);
                    table.ForeignKey(
                        name: "FK_Abono_Planta_PlantaId",
                        column: x => x.PlantaId,
                        principalTable: "Planta",
                        principalColumn: "PlantaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Irrigacion",
                columns: table => new
                {
                    IrrigacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantaId = table.Column<int>(type: "int", nullable: false),
                    FechaRiego = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoRiego = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrecuenciaRiego = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuracionRiego = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    CantidadAgua = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Irrigacion", x => x.IrrigacionId);
                    table.ForeignKey(
                        name: "FK_Irrigacion_Planta_PlantaId",
                        column: x => x.PlantaId,
                        principalTable: "Planta",
                        principalColumn: "PlantaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abono_PlantaId",
                table: "Abono",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_Irrigacion_PlantaId",
                table: "Irrigacion",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_NombreUsuario",
                table: "Usuario",
                column: "NombreUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abono");

            migrationBuilder.DropTable(
                name: "Irrigacion");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Planta");
        }
    }
}
