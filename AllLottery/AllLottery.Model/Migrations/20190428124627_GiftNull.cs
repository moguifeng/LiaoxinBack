using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllLottery.Model.Migrations
{
    public partial class GiftNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftReceives_GiftEvents_GiftEventId",
                table: "GiftReceives");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftReceives_Recharges_RechargeId",
                table: "GiftReceives");

            migrationBuilder.RenameColumn(
                name: "GiftEventId",
                table: "GiftReceives",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_GiftReceives_GiftEventId",
                table: "GiftReceives",
                newName: "IX_GiftReceives_PlayerId");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Players",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "LotteryTypes",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "LotteryDatas",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RechargeId",
                table: "GiftReceives",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "DividendDates",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Bets",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftReceives_Players_PlayerId",
                table: "GiftReceives",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftReceives_Recharges_RechargeId",
                table: "GiftReceives",
                column: "RechargeId",
                principalTable: "Recharges",
                principalColumn: "RechargeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftReceives_Players_PlayerId",
                table: "GiftReceives");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftReceives_Recharges_RechargeId",
                table: "GiftReceives");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "GiftReceives",
                newName: "GiftEventId");

            migrationBuilder.RenameIndex(
                name: "IX_GiftReceives_PlayerId",
                table: "GiftReceives",
                newName: "IX_GiftReceives_GiftEventId");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Players",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "LotteryTypes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "LotteryDatas",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RechargeId",
                table: "GiftReceives",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "DividendDates",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "Bets",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftReceives_GiftEvents_GiftEventId",
                table: "GiftReceives",
                column: "GiftEventId",
                principalTable: "GiftEvents",
                principalColumn: "GiftEventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftReceives_Recharges_RechargeId",
                table: "GiftReceives",
                column: "RechargeId",
                principalTable: "Recharges",
                principalColumn: "RechargeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
