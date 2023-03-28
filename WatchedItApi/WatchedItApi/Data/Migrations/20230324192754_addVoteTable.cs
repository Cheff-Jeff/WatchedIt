using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchedItApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class addVoteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "voteCount",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "UserVoteMovies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieListId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    voteCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVoteMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVoteMovies_MovieLists_MovieListId",
                        column: x => x.MovieListId,
                        principalTable: "MovieLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVoteMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVoteMovies_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVoteMovies_MovieId",
                table: "UserVoteMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVoteMovies_MovieListId",
                table: "UserVoteMovies",
                column: "MovieListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVoteMovies_userId",
                table: "UserVoteMovies",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVoteMovies");

            migrationBuilder.AddColumn<int>(
                name: "voteCount",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
