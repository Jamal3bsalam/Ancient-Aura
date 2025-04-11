using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AncientAura.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVideosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discription",
                table: "Videos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discription",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
