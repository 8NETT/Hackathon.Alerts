using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegraDeAlerta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Alvo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Operador = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Limite = table.Column<double>(type: "float", nullable: false),
                    JanelasConsecutivas = table.Column<int>(type: "int", nullable: false),
                    ExigirJanelaCompleta = table.Column<bool>(type: "bit", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Severidade = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegraDeAlerta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talhao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProprietarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talhao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeituraAgregada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TalhaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Unidade = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Inicio = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Fim = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Minima = table.Column<double>(type: "float", nullable: false),
                    Maxima = table.Column<double>(type: "float", nullable: false),
                    Media = table.Column<double>(type: "float", nullable: false),
                    Soma = table.Column<double>(type: "float", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrimeiroTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimoTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UltimoValor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeituraAgregada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeituraAgregada_Talhao_TalhaoId",
                        column: x => x.TalhaoId,
                        principalTable: "Talhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeituraAgregada_TalhaoId",
                table: "LeituraAgregada",
                column: "TalhaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeituraAgregada");

            migrationBuilder.DropTable(
                name: "RegraDeAlerta");

            migrationBuilder.DropTable(
                name: "Talhao");
        }
    }
}
