using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchedItApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class voteCountAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "voteCount",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "voteCount",
                table: "Movies");
        }
    }
}
