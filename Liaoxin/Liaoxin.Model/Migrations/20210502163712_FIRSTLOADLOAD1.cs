using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class FIRSTLOADLOAD1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientRelationDetails_ClientTags_ClientTagId",
                table: "ClientRelationDetails");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientTagId",
                table: "ClientRelationDetails",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ClientRelationDetails_ClientTags_ClientTagId",
                table: "ClientRelationDetails",
                column: "ClientTagId",
                principalTable: "ClientTags",
                principalColumn: "ClientTagId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientRelationDetails_ClientTags_ClientTagId",
                table: "ClientRelationDetails");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientTagId",
                table: "ClientRelationDetails",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientRelationDetails_ClientTags_ClientTagId",
                table: "ClientRelationDetails",
                column: "ClientTagId",
                principalTable: "ClientTags",
                principalColumn: "ClientTagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
