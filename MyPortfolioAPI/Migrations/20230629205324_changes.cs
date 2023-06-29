using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolioAPI.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiesnces_AspNetUsers_OwnerId",
                table: "WorkExperiesnces");


            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "WorkExperiesnces",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiesnces_AspNetUsers_OwnerId",
                table: "WorkExperiesnces",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiesnces_AspNetUsers_OwnerId",
                table: "WorkExperiesnces");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "WorkExperiesnces",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

           
            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiesnces_AspNetUsers_OwnerId",
                table: "WorkExperiesnces",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
