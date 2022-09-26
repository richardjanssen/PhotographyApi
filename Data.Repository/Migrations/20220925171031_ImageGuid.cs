using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Repository.Migrations
{
    public partial class ImageGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Images",
                newName: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Images",
                newName: "Path");
        }
    }
}
