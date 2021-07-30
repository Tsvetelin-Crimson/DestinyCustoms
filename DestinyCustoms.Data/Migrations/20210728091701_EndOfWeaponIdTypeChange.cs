using Microsoft.EntityFrameworkCore.Migrations;

namespace DestinyCustoms.Data.Migrations
{
    public partial class EndOfWeaponIdTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_ExoticWeaponId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ExoticWeaponId",
                table: "Weapons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ExoticWeaponId",
                table: "Comments",
                newName: "ExoticId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ExoticWeaponId",
                table: "Comments",
                newName: "IX_Comments_ExoticId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments",
                column: "ExoticId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Weapons",
                newName: "ExoticWeaponId");

            migrationBuilder.RenameColumn(
                name: "ExoticId",
                table: "Comments",
                newName: "ExoticWeaponId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ExoticId",
                table: "Comments",
                newName: "IX_Comments_ExoticWeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Weapons_ExoticWeaponId",
                table: "Comments",
                column: "ExoticWeaponId",
                principalTable: "Weapons",
                principalColumn: "ExoticWeaponId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
