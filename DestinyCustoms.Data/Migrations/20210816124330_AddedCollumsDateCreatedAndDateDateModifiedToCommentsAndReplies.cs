using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DestinyCustoms.Data.Migrations
{
    public partial class AddedCollumsDateCreatedAndDateDateModifiedToCommentsAndReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ExoticId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ExoticId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Weapons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Replies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Replies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "WeaponId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Armors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_UserId",
                table: "Weapons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WeaponId",
                table: "Comments",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Armors_UserId",
                table: "Armors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Armors_AspNetUsers_UserId",
                table: "Armors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Weapons_WeaponId",
                table: "Comments",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_AspNetUsers_UserId",
                table: "Weapons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armors_AspNetUsers_UserId",
                table: "Armors");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_WeaponId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_AspNetUsers_UserId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_UserId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Comments_WeaponId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Armors_UserId",
                table: "Armors");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "WeaponId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExoticId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Armors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExoticId",
                table: "Comments",
                column: "ExoticId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments",
                column: "ExoticId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
