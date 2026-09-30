using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Repository.Migrations
{
    /// <inheritdoc />
    public partial class IngredientAddToGroceries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddToGroceries",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddToGroceries",
                table: "Ingredients");
        }
    }
}
