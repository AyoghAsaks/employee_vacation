using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeVacation.Migrations
{
    public partial class addedSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "27a676bb-3d08-4d02-9603-70ee9ecbdc54", "2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "824f100d-a55d-479d-9fb5-fe4aaaddf6ff", "1", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27a676bb-3d08-4d02-9603-70ee9ecbdc54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "824f100d-a55d-479d-9fb5-fe4aaaddf6ff");
        }
    }
}
