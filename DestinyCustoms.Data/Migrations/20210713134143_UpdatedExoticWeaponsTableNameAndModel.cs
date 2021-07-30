using Microsoft.EntityFrameworkCore.Migrations;

namespace DestinyCustoms.Data.Migrations
{
    public partial class UpdatedExoticWeaponsTableNameAndModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Exotics_ExoticId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exotics_ItemClasses_WeaponClassId",
                table: "Exotics");

            migrationBuilder.DropForeignKey(
                name: "FK_Suggestions_Exotics_ExoticId",
                table: "Suggestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exotics",
                table: "Exotics");

            migrationBuilder.RenameTable(
                name: "Exotics",
                newName: "Weapons");

            migrationBuilder.RenameIndex(
                name: "IX_Exotics_WeaponClassId",
                table: "Weapons",
                newName: "IX_Weapons_WeaponClassId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "CatalystEffect",
                table: "Weapons",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments",
                column: "ExoticId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suggestions_Weapons_ExoticId",
                table: "Suggestions",
                column: "ExoticId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_ItemClasses_WeaponClassId",
                table: "Weapons",
                column: "WeaponClassId",
                principalTable: "ItemClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Suggestions_Weapons_ExoticId",
                table: "Suggestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_ItemClasses_WeaponClassId",
                table: "Weapons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "CatalystEffect",
                table: "Weapons");

            migrationBuilder.RenameTable(
                name: "Weapons",
                newName: "Exotics");

            migrationBuilder.RenameIndex(
                name: "IX_Weapons_WeaponClassId",
                table: "Exotics",
                newName: "IX_Exotics_WeaponClassId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exotics",
                table: "Exotics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Exotics_ExoticId",
                table: "Comments",
                column: "ExoticId",
                principalTable: "Exotics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exotics_ItemClasses_WeaponClassId",
                table: "Exotics",
                column: "WeaponClassId",
                principalTable: "ItemClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suggestions_Exotics_ExoticId",
                table: "Suggestions",
                column: "ExoticId",
                principalTable: "Exotics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
