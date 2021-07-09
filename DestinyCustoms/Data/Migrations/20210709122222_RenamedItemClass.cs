using Microsoft.EntityFrameworkCore.Migrations;

namespace DestinyCustoms.Data.Migrations
{
    public partial class RenamedItemClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exotics_ItemClasses_ItemClassId",
                table: "Exotics");

            migrationBuilder.RenameColumn(
                name: "ItemClassId",
                table: "Exotics",
                newName: "WeaponClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Exotics_ItemClassId",
                table: "Exotics",
                newName: "IX_Exotics_WeaponClassId");

            migrationBuilder.AlterColumn<string>(
                name: "CatalystName",
                table: "Exotics",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "CatalystCompletionRequirement",
                table: "Exotics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Exotics_ItemClasses_WeaponClassId",
                table: "Exotics",
                column: "WeaponClassId",
                principalTable: "ItemClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exotics_ItemClasses_WeaponClassId",
                table: "Exotics");

            migrationBuilder.RenameColumn(
                name: "WeaponClassId",
                table: "Exotics",
                newName: "ItemClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Exotics_WeaponClassId",
                table: "Exotics",
                newName: "IX_Exotics_ItemClassId");

            migrationBuilder.AlterColumn<string>(
                name: "CatalystName",
                table: "Exotics",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "CatalystCompletionRequirement",
                table: "Exotics",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddForeignKey(
                name: "FK_Exotics_ItemClasses_ItemClassId",
                table: "Exotics",
                column: "ItemClassId",
                principalTable: "ItemClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
