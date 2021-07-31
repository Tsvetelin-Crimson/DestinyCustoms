using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DestinyCustoms.Data.Migrations
{
    public partial class AddedExoticArmorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArmorId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Armors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    IntrinsicName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IntrinsicDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterClass = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArmorId",
                table: "Comments",
                column: "ArmorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Armors_ArmorId",
                table: "Comments",
                column: "ArmorId",
                principalTable: "Armors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Armors_ArmorId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Armors");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ArmorId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ArmorId",
                table: "Comments");
        }
    }
}
