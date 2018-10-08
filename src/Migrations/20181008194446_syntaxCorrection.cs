using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingNet.Migrations
{
    public partial class syntaxCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Movies_movieId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "movieId",
                table: "Comments",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Comments",
                newName: "Content");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_movieId",
                table: "Comments",
                newName: "IX_Comments_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Movies_MovieId",
                table: "Comments",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Movies_MovieId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Comments",
                newName: "movieId");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "content");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_MovieId",
                table: "Comments",
                newName: "IX_Comments_movieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Movies_movieId",
                table: "Comments",
                column: "movieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
