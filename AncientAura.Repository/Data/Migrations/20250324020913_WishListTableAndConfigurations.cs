using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AncientAura.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class WishListTableAndConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishLists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishListId = table.Column<int>(type: "int", nullable: true),
                    AncientSitesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_AncientSites_AncientSitesId",
                        column: x => x.AncientSitesId,
                        principalTable: "AncientSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_WishLists_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_AncientSitesId",
                table: "Items",
                column: "AncientSitesId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_WishListId",
                table: "Items",
                column: "WishListId");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_AppUserId",
                table: "WishLists",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "WishLists");
        }
    }
}
