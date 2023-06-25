using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
