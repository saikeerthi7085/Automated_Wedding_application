using Microsoft.EntityFrameworkCore.Migrations;

namespace Automated_Wedding_Application.Data.Migrations
{
    public partial class updateplanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Planner_services_servicesTempId",
            //    table: "Planner");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_plannerservices_Planner_PlannerModelPlannerId",
            //    table: "plannerservices");

            //migrationBuilder.DropTable(
            //    name: "services");

            //migrationBuilder.DropColumn(
            //    name: "servicesTempId",
            //    table: "Planner");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Planner",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Planner",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Planner",
                nullable: true);

          

            migrationBuilder.CreateIndex(
                name: "IX_Planner_ApplicationUserId",
                table: "Planner",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planner_AspNetUsers_ApplicationUserId",
                table: "Planner",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_plannerservices_Planner_PlannerModelPlannerId",
            //    table: "plannerservices",
            //    column: "PlannerModelPlannerId",
            //    principalTable: "Planner",
            //    principalColumn: "PlannerId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planner_AspNetUsers_ApplicationUserId",
                table: "Planner");

            migrationBuilder.DropForeignKey(
                name: "FK_plannerservices_Planner_PlannerModelPlannerId",
                table: "plannerservices");

            migrationBuilder.DropIndex(
                name: "IX_Planner_ApplicationUserId",
                table: "Planner");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Planner");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Planner");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Planner",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Planner",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "servicesTempId",
                table: "Planner",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    TempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.UniqueConstraint("AK_services_TempId", x => x.TempId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Planner_services_servicesTempId",
                table: "Planner",
                column: "servicesTempId",
                principalTable: "services",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_plannerservices_Planner_PlannerModelPlannerId",
                table: "plannerservices",
                column: "PlannerModelPlannerId",
                principalTable: "Planner",
                principalColumn: "PlannerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
