using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Automated_Wedding_Application.Data.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Serviceimage",
                table: "plannerservices",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Plannerimage",
                table: "Planner",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Serviceimage",
                table: "plannerservices");

            migrationBuilder.DropColumn(
                name: "Plannerimage",
                table: "Planner");
        }
    }
}
