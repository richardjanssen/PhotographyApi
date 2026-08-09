using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UsersPart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"RefreshTokens\"");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "RefreshTokens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "RefreshTokens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
