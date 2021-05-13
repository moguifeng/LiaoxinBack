using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class TEST11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CoinLogs_AboutId",
                table: "CoinLogs");

            migrationBuilder.DropIndex(
                name: "IX_CoinLogs_CoinLogId",
                table: "CoinLogs");

            migrationBuilder.DropIndex(
                name: "IX_CoinLogs_Type",
                table: "CoinLogs");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_AboutId",
                table: "CoinLogs",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_CoinLogId",
                table: "CoinLogs",
                column: "CoinLogId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_Type",
                table: "CoinLogs",
                column: "Type");
        }
    }
}
