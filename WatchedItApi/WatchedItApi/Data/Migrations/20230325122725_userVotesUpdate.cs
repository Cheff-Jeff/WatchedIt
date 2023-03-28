using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchedItApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class userVotesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVotes_MovieLists_MovieListId",
                table: "UserVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVotes_Users_userId",
                table: "UserVotes");

            migrationBuilder.DropIndex(
                name: "IX_UserVotes_MovieListId",
                table: "UserVotes");

            migrationBuilder.DropIndex(
                name: "IX_UserVotes_userId",
                table: "UserVotes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserVotes_MovieListId",
                table: "UserVotes",
                column: "MovieListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVotes_userId",
                table: "UserVotes",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVotes_MovieLists_MovieListId",
                table: "UserVotes",
                column: "MovieListId",
                principalTable: "MovieLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVotes_Users_userId",
                table: "UserVotes",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
