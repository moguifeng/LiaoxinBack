using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class REMOVECLIENTADD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientAddDetails_ClientAdds_ClientAddId",
                table: "ClientAddDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientAddDetails_Clients_ClientId",
                table: "ClientAddDetails");

            migrationBuilder.DropTable(
                name: "ClientAdds");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "ClientAddDetails",
                newName: "ToClientId");

            migrationBuilder.RenameColumn(
                name: "ClientAddId",
                table: "ClientAddDetails",
                newName: "FromClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientAddDetails_ClientId",
                table: "ClientAddDetails",
                newName: "IX_ClientAddDetails_ToClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientAddDetails_ClientAddId",
                table: "ClientAddDetails",
                newName: "IX_ClientAddDetails_FromClientId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAddDetails_Clients_FromClientId",
                table: "ClientAddDetails",
                column: "FromClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAddDetails_Clients_ToClientId",
                table: "ClientAddDetails",
                column: "ToClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientAddDetails_Clients_FromClientId",
                table: "ClientAddDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientAddDetails_Clients_ToClientId",
                table: "ClientAddDetails");

            migrationBuilder.RenameColumn(
                name: "ToClientId",
                table: "ClientAddDetails",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "FromClientId",
                table: "ClientAddDetails",
                newName: "ClientAddId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientAddDetails_ToClientId",
                table: "ClientAddDetails",
                newName: "IX_ClientAddDetails_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientAddDetails_FromClientId",
                table: "ClientAddDetails",
                newName: "IX_ClientAddDetails_ClientAddId");

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

            migrationBuilder.CreateTable(
                name: "ClientAdds",
                columns: table => new
                {
                    ClientAddId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAdds", x => x.ClientAddId);
                    table.ForeignKey(
                        name: "FK_ClientAdds_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientAdds_ClientId",
                table: "ClientAdds",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAdds_CreateTime",
                table: "ClientAdds",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAdds_IsEnable",
                table: "ClientAdds",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAdds_UpdateTime",
                table: "ClientAdds",
                column: "UpdateTime");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAddDetails_ClientAdds_ClientAddId",
                table: "ClientAddDetails",
                column: "ClientAddId",
                principalTable: "ClientAdds",
                principalColumn: "ClientAddId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAddDetails_Clients_ClientId",
                table: "ClientAddDetails",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
