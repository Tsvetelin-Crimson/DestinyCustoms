using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DestinyCustoms.Data.Migrations
{
    public partial class AddedDateCreatedAndModifiedCollumsAndRenamedWhereNeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_ItemClasses_WeaponClassId",
                table: "Weapons");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemClasses",
                table: "ItemClasses");

            migrationBuilder.RenameTable(
                name: "ItemClasses",
                newName: "WeaponClasses");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ExoticId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeaponId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeaponClasses",
                table: "WeaponClasses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments",
                column: "ExoticId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_WeaponClasses_WeaponClassId",
                table: "Weapons",
                column: "WeaponClassId",
                principalTable: "WeaponClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_WeaponClasses_WeaponClassId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeaponClasses",
                table: "WeaponClasses");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "WeaponClasses",
                newName: "ItemClasses");

            migrationBuilder.AlterColumn<int>(
                name: "ExoticId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemClasses",
                table: "ItemClasses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExoticId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_Weapons_ExoticId",
                        column: x => x.ExoticId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_ExoticId",
                table: "Suggestions",
                column: "ExoticId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments",
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
    }
}
