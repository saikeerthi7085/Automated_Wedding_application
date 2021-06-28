using Microsoft.EntityFrameworkCore.Migrations;

namespace Automated_Wedding_Application.Data.Migrations
{
    public partial class Chargeid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chargeid",
                table: "CustomerCart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chargeid",
                table: "CustomerCart");
        }
    }
}
