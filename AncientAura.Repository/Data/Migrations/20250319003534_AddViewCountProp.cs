using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AncientAura.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddViewCountProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Documentries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "AncientSites",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Documentries");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "AncientSites");
        }
    }
}
