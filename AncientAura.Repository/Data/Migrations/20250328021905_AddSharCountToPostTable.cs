using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AncientAura.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSharCountToPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShareCount",
                table: "Posts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShareCount",
                table: "Posts");
        }
    }
}
