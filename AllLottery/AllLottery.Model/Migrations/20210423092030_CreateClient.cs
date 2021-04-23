using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllLottery.Model.Migrations
{
    public partial class CreateClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tests");

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

            migrationBuilder.AddColumn<string>(
                name: "TestRow",
                table: "GiftEvents",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "DividendDates",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "ChasingOrders",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "ChasingOrderDetails",
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
                name: "ClientBlacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    FromClientId = table.Column<int>(nullable: false),
                    ToClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBlacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Cover = table.Column<string>(nullable: true),
                    HuanXinId = table.Column<string>(nullable: true),
                    LiaoxinNumber = table.Column<string>(maxLength: 20, nullable: true),
                    NickName = table.Column<string>(maxLength: 20, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CoinPassword = table.Column<string>(nullable: true),
                    Coin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    CharacterSignature = table.Column<string>(nullable: true),
                    Country = table.Column<int>(nullable: false),
                    Province = table.Column<int>(nullable: false),
                    City = table.Column<int>(nullable: false),
                    CurrentDeviceId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    AddMeNeedChecked = table.Column<bool>(nullable: false),
                    ShowFriendCircle = table.Column<int>(nullable: false),
                    UpadteMind = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientBlacks_CreateTime",
                table: "ClientBlacks",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBlacks_IsEnable",
                table: "ClientBlacks",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBlacks_UpdateTime",
                table: "ClientBlacks",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CreateTime",
                table: "Clients",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IsEnable",
                table: "Clients",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_LiaoxinNumber",
                table: "Clients",
                column: "LiaoxinNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UpdateTime",
                table: "Clients",
                column: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientBlacks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropColumn(
                name: "TestRow",
                table: "GiftEvents");

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
                table: "ChasingOrders",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Version",
                table: "ChasingOrderDetails",
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

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestId);
                });
        }
    }
}
