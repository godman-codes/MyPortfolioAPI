using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolioAPI.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "573ce4be-2d82-47ba-a4e1-53ca5aa8fadc", "6707e42c-8068-48eb-8d87-c594d1f62205", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d4d9e614-b59b-4821-81da-186664aa37d9", "a31eb02c-681e-44bb-a4ea-710e2848d668", "Annonymous", "ANNONYMOUS" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f88a1c0e-eec9-4079-9da5-75008953ecc8", "6e7a9163-0f24-4d31-9e52-729a5618b071", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "573ce4be-2d82-47ba-a4e1-53ca5aa8fadc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4d9e614-b59b-4821-81da-186664aa37d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f88a1c0e-eec9-4079-9da5-75008953ecc8");
        }
    }
}
