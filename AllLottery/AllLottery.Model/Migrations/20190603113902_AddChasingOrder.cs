using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllLottery.Model.Migrations
{
    public partial class AddChasingOrder : Migration
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

            migrationBuilder.CreateTable(
                name: "ChasingOrders",
                columns: table => new
                {
                    ChasingOrderId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Order = table.Column<string>(maxLength: 100, nullable: true),
                    IsWinStop = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    BetModeId = table.Column<int>(nullable: false),
                    LotteryPlayDetailId = table.Column<int>(nullable: false),
                    BetNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChasingOrders", x => x.ChasingOrderId);
                    table.ForeignKey(
                        name: "FK_ChasingOrders_BetModes_BetModeId",
                        column: x => x.BetModeId,
                        principalTable: "BetModes",
                        principalColumn: "BetModeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChasingOrders_LotteryPlayDetails_LotteryPlayDetailId",
                        column: x => x.LotteryPlayDetailId,
                        principalTable: "LotteryPlayDetails",
                        principalColumn: "LotteryPlayDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChasingOrders_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChasingOrderDetails",
                columns: table => new
                {
                    ChasingOrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ChasingOrderId = table.Column<int>(nullable: false),
                    BetId = table.Column<int>(nullable: true),
                    Times = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChasingOrderDetails", x => x.ChasingOrderDetailId);
                    table.ForeignKey(
                        name: "FK_ChasingOrderDetails_Bets_BetId",
                        column: x => x.BetId,
                        principalTable: "Bets",
                        principalColumn: "BetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChasingOrderDetails_ChasingOrders_ChasingOrderId",
                        column: x => x.ChasingOrderId,
                        principalTable: "ChasingOrders",
                        principalColumn: "ChasingOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrderDetails_BetId",
                table: "ChasingOrderDetails",
                column: "BetId");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrderDetails_ChasingOrderId",
                table: "ChasingOrderDetails",
                column: "ChasingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrderDetails_CreateTime",
                table: "ChasingOrderDetails",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrderDetails_IsEnable",
                table: "ChasingOrderDetails",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrderDetails_UpdateTime",
                table: "ChasingOrderDetails",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrders_BetModeId",
                table: "ChasingOrders",
                column: "BetModeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrders_CreateTime",
                table: "ChasingOrders",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrders_IsEnable",
                table: "ChasingOrders",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrders_LotteryPlayDetailId",
                table: "ChasingOrders",
                column: "LotteryPlayDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrders_Order",
                table: "ChasingOrders",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrders_PlayerId",
                table: "ChasingOrders",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChasingOrders_UpdateTime",
                table: "ChasingOrders",
                column: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChasingOrderDetails");

            migrationBuilder.DropTable(
                name: "ChasingOrders");

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
        }
    }
}
