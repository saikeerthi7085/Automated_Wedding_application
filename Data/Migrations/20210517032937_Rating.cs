using Microsoft.EntityFrameworkCore.Migrations;

namespace Automated_Wedding_Application.Data.Migrations
{
    public partial class Rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "UserID",
            //    table: "Planner");

            //migrationBuilder.AlterColumn<int>(
            //    name: "PlannerModelPlannerId",
            //    table: "plannerservices",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Planner",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ServicesCost",
                table: "Planner",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Planner");

            migrationBuilder.DropColumn(
                name: "ServicesCost",
                table: "Planner");

            migrationBuilder.AlterColumn<int>(
                name: "PlannerModelPlannerId",
                table: "plannerservices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Planner",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
