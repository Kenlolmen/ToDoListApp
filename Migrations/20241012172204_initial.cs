using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoListApp.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    IDKategoria = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.IDKategoria);
                });

            migrationBuilder.CreateTable(
                name: "Statusy",
                columns: table => new
                {
                    IDStatus = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statusy", x => x.IDStatus);
                });

            migrationBuilder.CreateTable(
                name: "Zadania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IDStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusIDStatus = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDKategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategoriaIDKategoria = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadania", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zadania_Kategorie_KategoriaIDKategoria",
                        column: x => x.KategoriaIDKategoria,
                        principalTable: "Kategorie",
                        principalColumn: "IDKategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zadania_Statusy_StatusIDStatus",
                        column: x => x.StatusIDStatus,
                        principalTable: "Statusy",
                        principalColumn: "IDStatus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kategorie",
                columns: new[] { "IDKategoria", "Nazwa" },
                values: new object[,]
                {
                    { "dom", "Dom" },
                    { "pra", "Praca" },
                    { "pro", "Projekt" },
                    { "spo", "Sport" }
                });

            migrationBuilder.InsertData(
                table: "Statusy",
                columns: new[] { "IDStatus", "Nazwa" },
                values: new object[,]
                {
                    { "otw", "Otwarte" },
                    { "zam", "Zamkniete" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zadania_KategoriaIDKategoria",
                table: "Zadania",
                column: "KategoriaIDKategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Zadania_StatusIDStatus",
                table: "Zadania",
                column: "StatusIDStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zadania");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.DropTable(
                name: "Statusy");
        }
    }
}
