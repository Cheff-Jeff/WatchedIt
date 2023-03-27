using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchedItApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class addmovieDeadline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "addMovieDeadLine",
                table: "MovieLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "addMovieDeadLine",
                table: "MovieLists");
        }
    }
}
