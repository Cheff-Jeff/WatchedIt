using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchedItApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateUserFriendsNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Friends",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserId",
                table: "Friends",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_UserId",
                table: "Friends",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_UserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieLists_Users_UserId",
                table: "MovieLists");

            migrationBuilder.DropIndex(
                name: "IX_Friends_UserId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Friends");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MovieLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieLists_Users_UserId",
                table: "MovieLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
