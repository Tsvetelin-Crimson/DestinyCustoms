using Microsoft.EntityFrameworkCore.Migrations;

namespace DestinyCustoms.Data.Migrations
{
    public partial class RenamedCollumsAndStartOfChangingWeaponIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_ExoticId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ExoticId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "ExoticId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "WeaponIntrinsicName",
                table: "Weapons",
                newName: "IntrinsicName");

            migrationBuilder.RenameColumn(
                name: "WeaponIntrinsicDescription",
                table: "Weapons",
                newName: "IntrinsicDescription");

            migrationBuilder.AddColumn<string>(
                name: "ExoticWeaponId",
                table: "Weapons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "WeaponId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ExoticWeaponId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons",
                column: "ExoticWeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExoticWeaponId",
                table: "Comments",
                column: "ExoticWeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Weapons_ExoticWeaponId",
                table: "Comments",
                column: "ExoticWeaponId",
                principalTable: "Weapons",
                principalColumn: "ExoticWeaponId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Weapons_ExoticWeaponId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ExoticWeaponId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ExoticWeaponId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "ExoticWeaponId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "IntrinsicName",
                table: "Weapons",
                newName: "WeaponIntrinsicName");

            migrationBuilder.RenameColumn(
                name: "IntrinsicDescription",
                table: "Weapons",
                newName: "WeaponIntrinsicDescription");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Weapons",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "WeaponId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExoticId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons",
                column: "Id");

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
