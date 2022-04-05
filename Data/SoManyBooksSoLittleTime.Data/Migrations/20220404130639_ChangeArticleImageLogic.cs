using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoManyBooksSoLittleTime.Data.Migrations
{
    public partial class ChangeArticleImageLogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Articles_ArticleId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ArticleId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArticleId",
                table: "Images",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Articles_ArticleId",
                table: "Images",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }
    }
}
