using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskLightlamp.Migrations
{
    public partial class asa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationId",
                table: "products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_ApplicationId",
                table: "products",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_AspNetUsers_ApplicationId",
                table: "products",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_AspNetUsers_ApplicationId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_ApplicationId",
                table: "products");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationId",
                table: "products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
