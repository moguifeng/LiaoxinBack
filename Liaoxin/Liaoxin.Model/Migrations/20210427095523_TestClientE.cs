using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class TestClientE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Clients",
                newName: "Telephone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Withdraws",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Recharges",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AppOpenWhileSound",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FontSize",
                table: "Clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HandFree",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NewMessageNotication",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OpenWhileShake",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowMessageNotication",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VideoMessageNotication",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WifiVideoPlay",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ClientEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    LastLoginDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEquipments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientEquipments_CreateTime",
                table: "ClientEquipments",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEquipments_IsEnable",
                table: "ClientEquipments",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEquipments_UpdateTime",
                table: "ClientEquipments",
                column: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientEquipments");

            migrationBuilder.DropColumn(
                name: "AppOpenWhileSound",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FontSize",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "HandFree",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NewMessageNotication",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "OpenWhileShake",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ShowMessageNotication",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "VideoMessageNotication",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "WifiVideoPlay",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                table: "Clients",
                newName: "Phone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Withdraws",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Recharges",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}
