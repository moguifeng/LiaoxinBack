using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class ADDRATE : Migration
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
                name: "RateOfClients",
                columns: table => new
                {
                    RateOfClientId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    IsStop = table.Column<bool>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateOfClients", x => x.RateOfClientId);
                    table.ForeignKey(
                        name: "FK_RateOfClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateOfGroupClients",
                columns: table => new
                {
                    RateOfGroupClientId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    IsStop = table.Column<bool>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateOfGroupClients", x => x.RateOfGroupClientId);
                    table.ForeignKey(
                        name: "FK_RateOfGroupClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateOfGroupClients_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateOfGroups",
                columns: table => new
                {
                    RateOfGroupId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    IsStop = table.Column<bool>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateOfGroups", x => x.RateOfGroupId);
                    table.ForeignKey(
                        name: "FK_RateOfGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RateOfClients_ClientId",
                table: "RateOfClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfClients_CreateTime",
                table: "RateOfClients",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfClients_IsEnable",
                table: "RateOfClients",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfClients_UpdateTime",
                table: "RateOfClients",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroupClients_ClientId",
                table: "RateOfGroupClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroupClients_CreateTime",
                table: "RateOfGroupClients",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroupClients_GroupId",
                table: "RateOfGroupClients",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroupClients_IsEnable",
                table: "RateOfGroupClients",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroupClients_UpdateTime",
                table: "RateOfGroupClients",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroups_CreateTime",
                table: "RateOfGroups",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroups_GroupId",
                table: "RateOfGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroups_IsEnable",
                table: "RateOfGroups",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_RateOfGroups_UpdateTime",
                table: "RateOfGroups",
                column: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RateOfClients");

            migrationBuilder.DropTable(
                name: "RateOfGroupClients");

            migrationBuilder.DropTable(
                name: "RateOfGroups");

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
