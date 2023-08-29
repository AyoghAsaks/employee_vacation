using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeVacation.Migrations
{
    public partial class addIdentityRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "267dd724-3650-4f0a-840b-00967ed228aa", "2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "710e4193-921a-470b-9b29-e545caf52c24", "1", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "267dd724-3650-4f0a-840b-00967ed228aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "710e4193-921a-470b-9b29-e545caf52c24");
        }
    }
}
