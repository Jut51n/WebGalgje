using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGalgje.Migrations
{
    public partial class gameupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastGuess",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastGuess",
                table: "Games");
        }
    }
}
