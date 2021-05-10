using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class DDD : Migration
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
                name: "RedPacketPersonals",
                columns: table => new
                {
                    RedPacketPersonalId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    FromClientId = table.Column<Guid>(nullable: false),
                    ToClientId = table.Column<Guid>(nullable: false),
                    SendTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IsReceive = table.Column<bool>(nullable: false),
                    Greeting = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedPacketPersonals", x => x.RedPacketPersonalId);
                    table.ForeignKey(
                        name: "FK_RedPacketPersonals_Clients_FromClientId",
                        column: x => x.FromClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RedPacketPersonals_Clients_ToClientId",
                        column: x => x.ToClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketPersonals_CreateTime",
                table: "RedPacketPersonals",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketPersonals_FromClientId",
                table: "RedPacketPersonals",
                column: "FromClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketPersonals_IsEnable",
                table: "RedPacketPersonals",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketPersonals_ToClientId",
                table: "RedPacketPersonals",
                column: "ToClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RedPacketPersonals_UpdateTime",
                table: "RedPacketPersonals",
                column: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RedPacketPersonals");

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
