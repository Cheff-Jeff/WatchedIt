using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchedItApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class movielistToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "MovieListid",
                table: "Movies",
                newName: "movieListId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_MovieListid",
                table: "Movies",
                newName: "IX_Movies_movieListId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MovieLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieLists_UserId",
                table: "MovieLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieLists_Users_UserId",
                table: "MovieLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieLists_Users_UserId",
                table: "MovieLists");

            migrationBuilder.DropIndex(
                name: "IX_MovieLists_UserId",
                table: "MovieLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MovieLists");

            migrationBuilder.RenameColumn(
                name: "movieListId",
                table: "Movies",
                newName: "MovieListid");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_movieListId",
                table: "Movies",
                newName: "IX_Movies_MovieListid");

        }
    }
}
