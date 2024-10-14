using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListApp.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDKategoria",
                table: "Zadania");

            migrationBuilder.DropColumn(
                name: "IDStatus",
                table: "Zadania");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDKategoria",
                table: "Zadania",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IDStatus",
                table: "Zadania",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
