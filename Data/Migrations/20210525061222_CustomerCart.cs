using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Automated_Wedding_Application.Data.Migrations
{
    public partial class CustomerCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceCost = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCart", x => x.CartId);
                    
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
