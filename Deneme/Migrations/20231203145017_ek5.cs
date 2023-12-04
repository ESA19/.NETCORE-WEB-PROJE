using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deneme.Migrations
{
    public partial class ek5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_applicationUserId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_applicationUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DepartmentId",
                table: "Appointments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DepartmentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_applicationUserId",
                table: "Appointments",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_applicationUserId",
                table: "Appointments",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
