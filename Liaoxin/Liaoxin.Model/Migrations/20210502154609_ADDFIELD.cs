using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class ADDFIELD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "ClientAddId",
                table: "ClientAddDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddDetails_ClientAddId",
                table: "ClientAddDetails",
                column: "ClientAddId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAddDetails_ClientAdds_ClientAddId",
                table: "ClientAddDetails",
                column: "ClientAddId",
                principalTable: "ClientAdds",
                principalColumn: "ClientAddId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientAddDetails_ClientAdds_ClientAddId",
                table: "ClientAddDetails");

            migrationBuilder.DropIndex(
                name: "IX_ClientAddDetails_ClientAddId",
                table: "ClientAddDetails");

            migrationBuilder.DropColumn(
                name: "ClientAddId",
                table: "ClientAddDetails");

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
