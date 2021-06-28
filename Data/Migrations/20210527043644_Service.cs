using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Automated_Wedding_Application.Data.Migrations
{
    public partial class Service : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCart",
                columns: table => new
                {
                    CartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    PlannerId = table.Column<string>(nullable: true),
                    ServiceId = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ServiceCost = table.Column<double>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCart", x => x.CartId);
                    
                });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCart");
        }
    }
}
