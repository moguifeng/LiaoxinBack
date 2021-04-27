using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class DeleteData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlatformMoneyLogs_Platforms_PlatformId",
                table: "PlatformMoneyLogs");

            migrationBuilder.DropTable(
                name: "ChasingOrderDetails");

            migrationBuilder.DropTable(
                name: "DailyWageLogs");

            migrationBuilder.DropTable(
                name: "DailyWages");

            migrationBuilder.DropTable(
                name: "DividendLogs");

            migrationBuilder.DropTable(
                name: "DividendSettings");

            migrationBuilder.DropTable(
                name: "GiftEvents");

            migrationBuilder.DropTable(
                name: "GiftReceives");

            migrationBuilder.DropTable(
                name: "LotteryOpenTimes");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "PlayerLotteryTypes");

            migrationBuilder.DropTable(
                name: "ProxyRegisters");

            migrationBuilder.DropTable(
                name: "RebateLogs");

            migrationBuilder.DropTable(
                name: "SoftwareExpireds");

            migrationBuilder.DropTable(
                name: "ChasingOrders");

            migrationBuilder.DropTable(
                name: "DividendDates");

            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "BetModes");

            migrationBuilder.DropTable(
                name: "LotteryDatas");

            migrationBuilder.DropTable(
                name: "LotteryPlayDetails");

            migrationBuilder.DropTable(
                name: "LotteryPlayTypes");

            migrationBuilder.DropTable(
                name: "LotteryTypes");

            migrationBuilder.DropTable(
                name: "LotteryClassifies");

            migrationBuilder.DropIndex(
                name: "IX_PlatformMoneyLogs_PlatformId",
                table: "PlatformMoneyLogs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "PlatformMoneyLogs");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Version",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "PlatformMoneyLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BetModes",
                columns: table => new
                {
                    BetModeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsCredit = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetModes", x => x.BetModeId);
                });

            migrationBuilder.CreateTable(
                name: "DailyWageLogs",
                columns: table => new
                {
                    DailyWageLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BetMen = table.Column<int>(nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CalDate = table.Column<DateTime>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DailyMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyWageLogs", x => x.DailyWageLogId);
                    table.ForeignKey(
                        name: "FK_DailyWageLogs_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyWages",
                columns: table => new
                {
                    DailyWageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    MenCount = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyWages", x => x.DailyWageId);
                });

            migrationBuilder.CreateTable(
                name: "DividendDates",
                columns: table => new
                {
                    DividendDateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsCal = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    SettleTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DividendDates", x => x.DividendDateId);
                });

            migrationBuilder.CreateTable(
                name: "DividendSettings",
                columns: table => new
                {
                    DividendSettingId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LostMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    MenCount = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DividendSettings", x => x.DividendSettingId);
                });

            migrationBuilder.CreateTable(
                name: "GiftEvents",
                columns: table => new
                {
                    GiftEventId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    MaxMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    MinMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ReceivingType = table.Column<int>(nullable: false),
                    ReturnMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ReturnRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Rule = table.Column<int>(nullable: false),
                    TestRow = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftEvents", x => x.GiftEventId);
                });

            migrationBuilder.CreateTable(
                name: "GiftReceives",
                columns: table => new
                {
                    GiftReceiveId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    GiftMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftReceives", x => x.GiftReceiveId);
                    table.ForeignKey(
                        name: "FK_GiftReceives_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryClassifies",
                columns: table => new
                {
                    LotteryClassifyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryClassifies", x => x.LotteryClassifyId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InfoId = table.Column<int>(nullable: false),
                    InfoType = table.Column<int>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    IsLook = table.Column<bool>(nullable: false),
                    IsSend = table.Column<bool>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    PlatformId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AffixId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.PlatformId);
                    table.ForeignKey(
                        name: "FK_Platforms_Affixs_AffixId",
                        column: x => x.AffixId,
                        principalTable: "Affixs",
                        principalColumn: "AffixId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProxyRegisters",
                columns: table => new
                {
                    ProxyRegisterId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Number = table.Column<string>(maxLength: 20, nullable: true),
                    PlayerId = table.Column<int>(nullable: false),
                    Rebate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UseCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProxyRegisters", x => x.ProxyRegisterId);
                    table.ForeignKey(
                        name: "FK_ProxyRegisters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareExpireds",
                columns: table => new
                {
                    SoftwareExpiredId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Expired = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareExpireds", x => x.SoftwareExpiredId);
                    table.ForeignKey(
                        name: "FK_SoftwareExpireds_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DividendLogs",
                columns: table => new
                {
                    DividendLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BetMen = table.Column<int>(nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CalBeginDate = table.Column<DateTime>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DividendDateId = table.Column<int>(nullable: false),
                    DividendMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LostMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DividendLogs", x => x.DividendLogId);
                    table.ForeignKey(
                        name: "FK_DividendLogs_DividendDates_DividendDateId",
                        column: x => x.DividendDateId,
                        principalTable: "DividendDates",
                        principalColumn: "DividendDateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DividendLogs_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryTypes",
                columns: table => new
                {
                    LotteryTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CalType = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DateFormat = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IconId = table.Column<int>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    IsStop = table.Column<bool>(nullable: false),
                    LotteryClassifyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NumberLength = table.Column<int>(nullable: false),
                    ReportDate = table.Column<DateTime>(nullable: false),
                    RiskTime = table.Column<TimeSpan>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    SpiderName = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<DateTime>(nullable: true),
                    WinMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    WinRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryTypes", x => x.LotteryTypeId);
                    table.ForeignKey(
                        name: "FK_LotteryTypes_Affixs_IconId",
                        column: x => x.IconId,
                        principalTable: "Affixs",
                        principalColumn: "AffixId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotteryTypes_LotteryClassifies_LotteryClassifyId",
                        column: x => x.LotteryClassifyId,
                        principalTable: "LotteryClassifies",
                        principalColumn: "LotteryClassifyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryDatas",
                columns: table => new
                {
                    LotteryDataId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 200, nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    LotteryTypeId = table.Column<int>(nullable: false),
                    Number = table.Column<string>(maxLength: 200, nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryDatas", x => x.LotteryDataId);
                    table.ForeignKey(
                        name: "FK_LotteryDatas_LotteryTypes_LotteryTypeId",
                        column: x => x.LotteryTypeId,
                        principalTable: "LotteryTypes",
                        principalColumn: "LotteryTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryOpenTimes",
                columns: table => new
                {
                    LotteryOpenTimeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    IsTomorrow = table.Column<bool>(nullable: false),
                    LotteryTypeId = table.Column<int>(nullable: false),
                    OpenNumber = table.Column<int>(nullable: false),
                    OpenTime = table.Column<TimeSpan>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryOpenTimes", x => x.LotteryOpenTimeId);
                    table.ForeignKey(
                        name: "FK_LotteryOpenTimes_LotteryTypes_LotteryTypeId",
                        column: x => x.LotteryTypeId,
                        principalTable: "LotteryTypes",
                        principalColumn: "LotteryTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryPlayTypes",
                columns: table => new
                {
                    LotteryPlayTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LotteryTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryPlayTypes", x => x.LotteryPlayTypeId);
                    table.ForeignKey(
                        name: "FK_LotteryPlayTypes_LotteryTypes_LotteryTypeId",
                        column: x => x.LotteryTypeId,
                        principalTable: "LotteryTypes",
                        principalColumn: "LotteryTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerLotteryTypes",
                columns: table => new
                {
                    PlayerLotteryTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LotteryTypeId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLotteryTypes", x => x.PlayerLotteryTypeId);
                    table.ForeignKey(
                        name: "FK_PlayerLotteryTypes_LotteryTypes_LotteryTypeId",
                        column: x => x.LotteryTypeId,
                        principalTable: "LotteryTypes",
                        principalColumn: "LotteryTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerLotteryTypes_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryPlayDetails",
                columns: table => new
                {
                    LotteryPlayDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    LotteryPlayTypeId = table.Column<int>(nullable: false),
                    MaxBetCount = table.Column<int>(nullable: true),
                    MaxBetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    MaxOdds = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    MinOdds = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ReflectClass = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryPlayDetails", x => x.LotteryPlayDetailId);
                    table.ForeignKey(
                        name: "FK_LotteryPlayDetails_LotteryPlayTypes_LotteryPlayTypeId",
                        column: x => x.LotteryPlayTypeId,
                        principalTable: "LotteryPlayTypes",
                        principalColumn: "LotteryPlayTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    BetId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BetCount = table.Column<int>(nullable: false),
                    BetModeId = table.Column<int>(nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BetNo = table.Column<string>(nullable: true),
                    BetTime = table.Column<DateTime>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreditRemark = table.Column<string>(maxLength: 50, nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    LotteryDataId = table.Column<int>(nullable: true),
                    LotteryIssuseNo = table.Column<string>(maxLength: 20, nullable: true),
                    LotteryPlayDetailId = table.Column<int>(nullable: false),
                    Order = table.Column<string>(maxLength: 100, nullable: true),
                    PlayerId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Times = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<DateTime>(nullable: true),
                    WinBetCount = table.Column<long>(nullable: false),
                    WinMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.BetId);
                    table.ForeignKey(
                        name: "FK_Bets_BetModes_BetModeId",
                        column: x => x.BetModeId,
                        principalTable: "BetModes",
                        principalColumn: "BetModeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_LotteryDatas_LotteryDataId",
                        column: x => x.LotteryDataId,
                        principalTable: "LotteryDatas",
                        principalColumn: "LotteryDataId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bets_LotteryPlayDetails_LotteryPlayDetailId",
                        column: x => x.LotteryPlayDetailId,
                        principalTable: "LotteryPlayDetails",
                        principalColumn: "LotteryPlayDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChasingOrders",
                columns: table => new
                {
                    ChasingOrderId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BetModeId = table.Column<int>(nullable: false),
                    BetNo = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    IsWinStop = table.Column<bool>(nullable: false),
                    LotteryPlayDetailId = table.Column<int>(nullable: false),
                    Order = table.Column<string>(maxLength: 100, nullable: true),
                    PlayerId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<DateTime>(nullable: true)
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
                name: "RebateLogs",
                columns: table => new
                {
                    RebateLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BetId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DiffRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    RebateMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RebateLogs", x => x.RebateLogId);
                    table.ForeignKey(
                        name: "FK_RebateLogs_Bets_BetId",
                        column: x => x.BetId,
                        principalTable: "Bets",
                        principalColumn: "BetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RebateLogs_Players_PlayerId",
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
                    BetId = table.Column<int>(nullable: true),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ChasingOrderId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Times = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<DateTime>(nullable: true)
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
                name: "IX_PlatformMoneyLogs_PlatformId",
                table: "PlatformMoneyLogs",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_BetModes_CreateTime",
                table: "BetModes",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_BetModes_IsEnable",
                table: "BetModes",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_BetModes_UpdateTime",
                table: "BetModes",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_BetModeId",
                table: "Bets",
                column: "BetModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_BetTime",
                table: "Bets",
                column: "BetTime");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_CreateTime",
                table: "Bets",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_CreditRemark",
                table: "Bets",
                column: "CreditRemark");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_IsEnable",
                table: "Bets",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_LotteryDataId",
                table: "Bets",
                column: "LotteryDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_LotteryIssuseNo",
                table: "Bets",
                column: "LotteryIssuseNo");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_LotteryPlayDetailId",
                table: "Bets",
                column: "LotteryPlayDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_Order",
                table: "Bets",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_PlayerId",
                table: "Bets",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_Status",
                table: "Bets",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_UpdateTime",
                table: "Bets",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_WinMoney",
                table: "Bets",
                column: "WinMoney");

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

            migrationBuilder.CreateIndex(
                name: "IX_DailyWageLogs_CreateTime",
                table: "DailyWageLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWageLogs_IsEnable",
                table: "DailyWageLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWageLogs_PlayerId",
                table: "DailyWageLogs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWageLogs_UpdateTime",
                table: "DailyWageLogs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWages_CreateTime",
                table: "DailyWages",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWages_IsEnable",
                table: "DailyWages",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWages_UpdateTime",
                table: "DailyWages",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DividendDates_CreateTime",
                table: "DividendDates",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DividendDates_IsEnable",
                table: "DividendDates",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_DividendDates_SettleTime",
                table: "DividendDates",
                column: "SettleTime",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DividendDates_UpdateTime",
                table: "DividendDates",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DividendLogs_CreateTime",
                table: "DividendLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DividendLogs_DividendDateId",
                table: "DividendLogs",
                column: "DividendDateId");

            migrationBuilder.CreateIndex(
                name: "IX_DividendLogs_IsEnable",
                table: "DividendLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_DividendLogs_PlayerId",
                table: "DividendLogs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_DividendLogs_UpdateTime",
                table: "DividendLogs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DividendSettings_CreateTime",
                table: "DividendSettings",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_DividendSettings_IsEnable",
                table: "DividendSettings",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_DividendSettings_UpdateTime",
                table: "DividendSettings",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GiftEvents_CreateTime",
                table: "GiftEvents",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GiftEvents_IsEnable",
                table: "GiftEvents",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_GiftEvents_UpdateTime",
                table: "GiftEvents",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GiftReceives_CreateTime",
                table: "GiftReceives",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GiftReceives_IsEnable",
                table: "GiftReceives",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_GiftReceives_PlayerId",
                table: "GiftReceives",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftReceives_UpdateTime",
                table: "GiftReceives",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryClassifies_CreateTime",
                table: "LotteryClassifies",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryClassifies_IsEnable",
                table: "LotteryClassifies",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryClassifies_Type",
                table: "LotteryClassifies",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LotteryClassifies_UpdateTime",
                table: "LotteryClassifies",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryDatas_CreateTime",
                table: "LotteryDatas",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryDatas_Data",
                table: "LotteryDatas",
                column: "Data");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryDatas_IsEnable",
                table: "LotteryDatas",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryDatas_LotteryDataId",
                table: "LotteryDatas",
                column: "LotteryDataId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryDatas_Time",
                table: "LotteryDatas",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryDatas_UpdateTime",
                table: "LotteryDatas",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryDatas_LotteryTypeId_Number",
                table: "LotteryDatas",
                columns: new[] { "LotteryTypeId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LotteryOpenTimes_CreateTime",
                table: "LotteryOpenTimes",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryOpenTimes_IsEnable",
                table: "LotteryOpenTimes",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryOpenTimes_LotteryTypeId",
                table: "LotteryOpenTimes",
                column: "LotteryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryOpenTimes_UpdateTime",
                table: "LotteryOpenTimes",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPlayDetails_CreateTime",
                table: "LotteryPlayDetails",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPlayDetails_IsEnable",
                table: "LotteryPlayDetails",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPlayDetails_LotteryPlayTypeId",
                table: "LotteryPlayDetails",
                column: "LotteryPlayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPlayDetails_UpdateTime",
                table: "LotteryPlayDetails",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPlayTypes_CreateTime",
                table: "LotteryPlayTypes",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPlayTypes_IsEnable",
                table: "LotteryPlayTypes",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPlayTypes_LotteryTypeId",
                table: "LotteryPlayTypes",
                column: "LotteryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPlayTypes_UpdateTime",
                table: "LotteryPlayTypes",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryTypes_CreateTime",
                table: "LotteryTypes",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryTypes_IconId",
                table: "LotteryTypes",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryTypes_IsEnable",
                table: "LotteryTypes",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryTypes_LotteryClassifyId",
                table: "LotteryTypes",
                column: "LotteryClassifyId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryTypes_LotteryTypeId",
                table: "LotteryTypes",
                column: "LotteryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryTypes_ReportDate",
                table: "LotteryTypes",
                column: "ReportDate");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryTypes_UpdateTime",
                table: "LotteryTypes",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreateTime",
                table: "Messages",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IsEnable",
                table: "Messages",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IsLook",
                table: "Messages",
                column: "IsLook");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IsSend",
                table: "Messages",
                column: "IsSend");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UpdateTime",
                table: "Messages",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_AffixId",
                table: "Platforms",
                column: "AffixId");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_CreateTime",
                table: "Platforms",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_IsEnable",
                table: "Platforms",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_UpdateTime",
                table: "Platforms",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_Value",
                table: "Platforms",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLotteryTypes_CreateTime",
                table: "PlayerLotteryTypes",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLotteryTypes_IsEnable",
                table: "PlayerLotteryTypes",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLotteryTypes_PlayerId",
                table: "PlayerLotteryTypes",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLotteryTypes_UpdateTime",
                table: "PlayerLotteryTypes",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLotteryTypes_LotteryTypeId_PlayerId",
                table: "PlayerLotteryTypes",
                columns: new[] { "LotteryTypeId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProxyRegisters_CreateTime",
                table: "ProxyRegisters",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ProxyRegisters_IsEnable",
                table: "ProxyRegisters",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ProxyRegisters_Number",
                table: "ProxyRegisters",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProxyRegisters_PlayerId",
                table: "ProxyRegisters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProxyRegisters_UpdateTime",
                table: "ProxyRegisters",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RebateLogs_BetId",
                table: "RebateLogs",
                column: "BetId");

            migrationBuilder.CreateIndex(
                name: "IX_RebateLogs_CreateTime",
                table: "RebateLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_RebateLogs_IsEnable",
                table: "RebateLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_RebateLogs_PlayerId",
                table: "RebateLogs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RebateLogs_RebateLogId",
                table: "RebateLogs",
                column: "RebateLogId");

            migrationBuilder.CreateIndex(
                name: "IX_RebateLogs_UpdateTime",
                table: "RebateLogs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareExpireds_CreateTime",
                table: "SoftwareExpireds",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareExpireds_IsEnable",
                table: "SoftwareExpireds",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareExpireds_PlayerId",
                table: "SoftwareExpireds",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareExpireds_UpdateTime",
                table: "SoftwareExpireds",
                column: "UpdateTime");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformMoneyLogs_Platforms_PlatformId",
                table: "PlatformMoneyLogs",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "PlatformId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
