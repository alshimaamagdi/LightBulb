using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskLightlamp.Migrations
{
    public partial class mm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_AspNetUsers_ApplicationUserId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_ApplicationUserId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_ApplicationUserId",
                table: "products",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_AspNetUsers_ApplicationUserId",
                table: "products",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
