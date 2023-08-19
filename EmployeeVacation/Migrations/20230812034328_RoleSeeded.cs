using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeVacation.Migrations
{
    public partial class RoleSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a442d2b6-0d2d-4baf-96d4-f341d1672efd", "2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c94670ca-2c30-47ea-840c-670f5b88d890", "1", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a442d2b6-0d2d-4baf-96d4-f341d1672efd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c94670ca-2c30-47ea-840c-670f5b88d890");
        }
    }
}
