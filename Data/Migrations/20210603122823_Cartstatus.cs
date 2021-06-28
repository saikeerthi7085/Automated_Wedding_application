using Microsoft.EntityFrameworkCore.Migrations;

namespace Automated_Wedding_Application.Data.Migrations
{
    public partial class Cartstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Deliverystatus",
                table: "CustomerCart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "CustomerCart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deliverystatus",
                table: "CustomerCart");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "CustomerCart");
        }
    }
}
