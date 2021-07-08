using Microsoft.EntityFrameworkCore.Migrations;

namespace DestinyCustoms.Data.Migrations
{
    public partial class AddedMainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exotics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeaponIntrinsic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Catalyst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalystCompletionRequirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exotics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exotics_ItemClasses_ItemClassId",
                        column: x => x.ItemClassId,
                        principalTable: "ItemClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    ExoticId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Exotics_ExoticId",
                        column: x => x.ExoticId,
                        principalTable: "Exotics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ExoticId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_Exotics_ExoticId",
                        column: x => x.ExoticId,
                        principalTable: "Exotics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExoticId",
                table: "Comments",
                column: "ExoticId");

            migrationBuilder.CreateIndex(
                name: "IX_Exotics_ItemClassId",
                table: "Exotics",
                column: "ItemClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_ExoticId",
                table: "Suggestions",
                column: "ExoticId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Exotics");

            migrationBuilder.DropTable(
                name: "ItemClasses");
        }
    }
}
