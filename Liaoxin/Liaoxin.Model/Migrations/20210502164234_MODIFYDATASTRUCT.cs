using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class MODIFYDATASTRUCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Withdraws_ClientBanks_ClientBankId1",
                table: "Withdraws");

            migrationBuilder.DropIndex(
                name: "IX_Withdraws_ClientBankId1",
                table: "Withdraws");

            migrationBuilder.DropColumn(
                name: "ClientBankId1",
                table: "Withdraws");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Withdraws",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientBankId",
                table: "Withdraws",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "WithdrawId",
                table: "Withdraws",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Recharges",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_ClientBankId",
                table: "Withdraws",
                column: "ClientBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraws_ClientBanks_ClientBankId",
                table: "Withdraws",
                column: "ClientBankId",
                principalTable: "ClientBanks",
                principalColumn: "ClientBankId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Withdraws_ClientBanks_ClientBankId",
                table: "Withdraws");

            migrationBuilder.DropIndex(
                name: "IX_Withdraws_ClientBankId",
                table: "Withdraws");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Withdraws",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientBankId",
                table: "Withdraws",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "WithdrawId",
                table: "Withdraws",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientBankId1",
                table: "Withdraws",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Recharges",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_ClientBankId1",
                table: "Withdraws",
                column: "ClientBankId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraws_ClientBanks_ClientBankId1",
                table: "Withdraws",
                column: "ClientBankId1",
                principalTable: "ClientBanks",
                principalColumn: "ClientBankId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
