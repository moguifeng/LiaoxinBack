using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class REDPACKET : Migration
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

            migrationBuilder.CreateTable(
                name: "RedPackets",
                columns: table => new
                {
                    RedPacketId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Greeting = table.Column<string>(nullable: true),
                    SendTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Over = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedPackets", x => x.RedPacketId);
                    table.ForeignKey(
                        name: "FK_RedPackets_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RedPackets_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedPacketReceives",
                columns: table => new
                {
                    RedPacketReceiveId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    RedPacketId = table.Column<Guid>(nullable: false),
                    SnatchMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    IsWin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedPacketReceives", x => x.RedPacketReceiveId);
                    table.ForeignKey(
                        name: "FK_RedPacketReceives_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RedPacketReceives_RedPackets_RedPacketId",
                        column: x => x.RedPacketId,
                        principalTable: "RedPackets",
                        principalColumn: "RedPacketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketReceives_ClientId",
                table: "RedPacketReceives",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketReceives_CreateTime",
                table: "RedPacketReceives",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketReceives_IsEnable",
                table: "RedPacketReceives",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketReceives_RedPacketId",
                table: "RedPacketReceives",
                column: "RedPacketId");

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketReceives_UpdateTime",
                table: "RedPacketReceives",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RedPackets_ClientId",
                table: "RedPackets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RedPackets_CreateTime",
                table: "RedPackets",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RedPackets_GroupId",
                table: "RedPackets",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RedPackets_IsEnable",
                table: "RedPackets",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_RedPackets_UpdateTime",
                table: "RedPackets",
                column: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RedPacketReceives");

            migrationBuilder.DropTable(
                name: "RedPackets");

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
