using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class CreateInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Affixs",
                columns: table => new
                {
                    AffixId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affixs", x => x.AffixId);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    AnnouncementId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ShowOpenTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.AnnouncementId);
                });

            migrationBuilder.CreateTable(
                name: "BetModes",
                columns: table => new
                {
                    BetModeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsCredit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetModes", x => x.BetModeId);
                });

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

            migrationBuilder.CreateTable(
                name: "DailyWages",
                columns: table => new
                {
                    DailyWageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    MenCount = table.Column<int>(nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
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
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    SettleTime = table.Column<DateTime>(nullable: false),
                    IsCal = table.Column<bool>(nullable: false),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true)
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
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    MenCount = table.Column<int>(nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    LostMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
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
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ReceivingType = table.Column<int>(nullable: false),
                    Rule = table.Column<int>(nullable: false),
                    ReturnMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    TestRow = table.Column<string>(nullable: true),
                    ReturnRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    MinMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    MaxMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftEvents", x => x.GiftEventId);
                });

            migrationBuilder.CreateTable(
                name: "LotteryClassifies",
                columns: table => new
                {
                    LotteryClassifyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false)
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
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InfoId = table.Column<int>(nullable: false),
                    InfoType = table.Column<int>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsLook = table.Column<bool>(nullable: false),
                    IsSend = table.Column<bool>(nullable: false),
                    Version = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    NavId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Title = table.Column<string>(maxLength: 20, nullable: true),
                    Rebate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Password = table.Column<string>(nullable: true),
                    CoinPassword = table.Column<string>(nullable: true),
                    Coin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    QQ = table.Column<string>(nullable: true),
                    WeChat = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    IsChangePassword = table.Column<bool>(nullable: false),
                    IsFreeze = table.Column<bool>(nullable: false),
                    CanWithdraw = table.Column<bool>(nullable: false),
                    LastBetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    FCoin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ParentPlayerId = table.Column<int>(nullable: true),
                    DailyWageRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    DividendRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    RechargeMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    WithdrawMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    WinMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    RebateMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    GiftMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ReportDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Players_ParentPlayerId",
                        column: x => x.ParentPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportCaches",
                columns: table => new
                {
                    ReportCacheId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    QueryTime = table.Column<DateTime>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCaches", x => x.ReportCacheId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    SystemConfigId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.SystemConfigId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserInfoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserInfoId);
                });

            migrationBuilder.CreateTable(
                name: "ActivityAnnouncements",
                columns: table => new
                {
                    ActivityAnnouncementId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    AffixId = table.Column<int>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityAnnouncements", x => x.ActivityAnnouncementId);
                    table.ForeignKey(
                        name: "FK_ActivityAnnouncements_Affixs_AffixId",
                        column: x => x.AffixId,
                        principalTable: "Affixs",
                        principalColumn: "AffixId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MerchantsBanks",
                columns: table => new
                {
                    MerchantsBankId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BannerAffixId = table.Column<int>(nullable: false),
                    BankUserName = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    ScanAffixId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IndexSort = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ThirdPayMerchantsType = table.Column<int>(nullable: false),
                    MerchantsNumber = table.Column<string>(nullable: true),
                    MerchantsKey = table.Column<string>(nullable: true),
                    Aisle = table.Column<string>(nullable: true),
                    Min = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Max = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsUseful = table.Column<bool>(nullable: false),
                    MerchantsBankShowType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantsBanks", x => x.MerchantsBankId);
                    table.ForeignKey(
                        name: "FK_MerchantsBanks_Affixs_BannerAffixId",
                        column: x => x.BannerAffixId,
                        principalTable: "Affixs",
                        principalColumn: "AffixId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MerchantsBanks_Affixs_ScanAffixId",
                        column: x => x.ScanAffixId,
                        principalTable: "Affixs",
                        principalColumn: "AffixId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PictureNewses",
                columns: table => new
                {
                    PictureNewsId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    AffixId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureNewses", x => x.PictureNewsId);
                    table.ForeignKey(
                        name: "FK_PictureNewses_Affixs_AffixId",
                        column: x => x.AffixId,
                        principalTable: "Affixs",
                        principalColumn: "AffixId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    PlatformId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    AffixId = table.Column<int>(nullable: false)
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
                name: "SystemBanks",
                columns: table => new
                {
                    SystemBankId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    AffixId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemBanks", x => x.SystemBankId);
                    table.ForeignKey(
                        name: "FK_SystemBanks_Affixs_AffixId",
                        column: x => x.AffixId,
                        principalTable: "Affixs",
                        principalColumn: "AffixId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryTypes",
                columns: table => new
                {
                    LotteryTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SpiderName = table.Column<string>(nullable: true),
                    LotteryClassifyId = table.Column<int>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    NumberLength = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsStop = table.Column<bool>(nullable: false),
                    RiskTime = table.Column<TimeSpan>(nullable: false),
                    DateFormat = table.Column<string>(nullable: true),
                    IsHot = table.Column<bool>(nullable: false),
                    CalType = table.Column<int>(nullable: false),
                    IconId = table.Column<int>(nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    WinMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ReportDate = table.Column<DateTime>(nullable: false),
                    WinRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true)
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
                name: "CoinLogs",
                columns: table => new
                {
                    CoinLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    FlowCoin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Coin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    FlowFCoin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    FCoin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Type = table.Column<int>(nullable: false),
                    AboutId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinLogs", x => x.CoinLogId);
                    table.ForeignKey(
                        name: "FK_CoinLogs_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyWageLogs",
                columns: table => new
                {
                    DailyWageLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BetMen = table.Column<int>(nullable: false),
                    DailyMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CalDate = table.Column<DateTime>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
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
                name: "DividendLogs",
                columns: table => new
                {
                    DividendLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    LostMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BetMen = table.Column<int>(nullable: false),
                    DividendMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CalBeginDate = table.Column<DateTime>(nullable: false),
                    DividendDateId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
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
                name: "GiftReceives",
                columns: table => new
                {
                    GiftReceiveId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    GiftMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
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
                name: "NotReportPlayers",
                columns: table => new
                {
                    NotReportPlayerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotReportPlayers", x => x.NotReportPlayerId);
                    table.ForeignKey(
                        name: "FK_NotReportPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerLoginLogs",
                columns: table => new
                {
                    PlayerLoginLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    IP = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    IsApp = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLoginLogs", x => x.PlayerLoginLogId);
                    table.ForeignKey(
                        name: "FK_PlayerLoginLogs_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerOperateLogs",
                columns: table => new
                {
                    PlayerOperateLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerOperateLogs", x => x.PlayerOperateLogId);
                    table.ForeignKey(
                        name: "FK_PlayerOperateLogs_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProxyRegisters",
                columns: table => new
                {
                    ProxyRegisterId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Number = table.Column<string>(maxLength: 20, nullable: true),
                    PlayerId = table.Column<int>(nullable: false),
                    UseCount = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Rebate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Remark = table.Column<string>(nullable: true)
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
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Expired = table.Column<DateTime>(nullable: false)
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
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfoPermissions",
                columns: table => new
                {
                    UserInfoId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoPermissions", x => new { x.PermissionId, x.UserInfoId });
                    table.ForeignKey(
                        name: "FK_UserInfoPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInfoPermissions_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfoRoles",
                columns: table => new
                {
                    UserInfoId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoRoles", x => new { x.RoleId, x.UserInfoId });
                    table.ForeignKey(
                        name: "FK_UserInfoRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInfoRoles_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperateLogs",
                columns: table => new
                {
                    UserOperateLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    UserInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperateLogs", x => x.UserOperateLogId);
                    table.ForeignKey(
                        name: "FK_UserOperateLogs_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recharges",
                columns: table => new
                {
                    RechargeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    OrderNo = table.Column<string>(maxLength: 20, nullable: true),
                    PlayerId = table.Column<int>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    MerchantsBankId = table.Column<int>(nullable: true),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recharges", x => x.RechargeId);
                    table.ForeignKey(
                        name: "FK_Recharges_MerchantsBanks_MerchantsBankId",
                        column: x => x.MerchantsBankId,
                        principalTable: "MerchantsBanks",
                        principalColumn: "MerchantsBankId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recharges_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformMoneyLogs",
                columns: table => new
                {
                    PlatformMoneyLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    PlatformId = table.Column<int>(nullable: false),
                    FlowMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformMoneyLogs", x => x.PlatformMoneyLogId);
                    table.ForeignKey(
                        name: "FK_PlatformMoneyLogs_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "PlatformId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformMoneyLogs_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerBanks",
                columns: table => new
                {
                    PlayerBankId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    SystemBankId = table.Column<int>(nullable: false),
                    CardNumber = table.Column<string>(maxLength: 20, nullable: true),
                    PayeeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBanks", x => x.PlayerBankId);
                    table.ForeignKey(
                        name: "FK_PlayerBanks_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerBanks_SystemBanks_SystemBankId",
                        column: x => x.SystemBankId,
                        principalTable: "SystemBanks",
                        principalColumn: "SystemBankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryDatas",
                columns: table => new
                {
                    LotteryDataId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LotteryTypeId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Number = table.Column<string>(maxLength: 200, nullable: true),
                    Data = table.Column<string>(maxLength: 200, nullable: true),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true)
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
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    OpenNumber = table.Column<int>(nullable: false),
                    OpenTime = table.Column<TimeSpan>(nullable: false),
                    IsTomorrow = table.Column<bool>(nullable: false),
                    LotteryTypeId = table.Column<int>(nullable: false)
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
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LotteryTypeId = table.Column<int>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false)
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
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    LotteryTypeId = table.Column<int>(nullable: false)
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
                name: "Withdraws",
                columns: table => new
                {
                    WithdrawId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    OrderNo = table.Column<string>(maxLength: 20, nullable: true),
                    PlayerBankId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraws", x => x.WithdrawId);
                    table.ForeignKey(
                        name: "FK_Withdraws_PlayerBanks_PlayerBankId",
                        column: x => x.PlayerBankId,
                        principalTable: "PlayerBanks",
                        principalColumn: "PlayerBankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryPlayDetails",
                columns: table => new
                {
                    LotteryPlayDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LotteryPlayTypeId = table.Column<int>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MaxBetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    MaxBetCount = table.Column<int>(nullable: true),
                    MaxOdds = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    MinOdds = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ReflectClass = table.Column<string>(nullable: true)
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
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Order = table.Column<string>(maxLength: 100, nullable: true),
                    PlayerId = table.Column<int>(nullable: false),
                    LotteryPlayDetailId = table.Column<int>(nullable: false),
                    LotteryIssuseNo = table.Column<string>(maxLength: 20, nullable: true),
                    BetNo = table.Column<string>(nullable: true),
                    BetTime = table.Column<DateTime>(nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BetCount = table.Column<int>(nullable: false),
                    BetModeId = table.Column<int>(nullable: false),
                    Times = table.Column<int>(nullable: false),
                    LotteryDataId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    WinMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    WinBetCount = table.Column<long>(nullable: false),
                    CreditRemark = table.Column<string>(maxLength: 50, nullable: true),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true)
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
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Order = table.Column<string>(maxLength: 100, nullable: true),
                    IsWinStop = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    BetModeId = table.Column<int>(nullable: false),
                    LotteryPlayDetailId = table.Column<int>(nullable: false),
                    BetNo = table.Column<string>(nullable: true),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true)
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
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    BetId = table.Column<int>(nullable: false),
                    RebateMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    DiffRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
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
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ChasingOrderId = table.Column<int>(nullable: false),
                    BetId = table.Column<int>(nullable: true),
                    Times = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true)
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
                name: "IX_ActivityAnnouncements_AffixId",
                table: "ActivityAnnouncements",
                column: "AffixId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAnnouncements_CreateTime",
                table: "ActivityAnnouncements",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAnnouncements_IsEnable",
                table: "ActivityAnnouncements",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAnnouncements_UpdateTime",
                table: "ActivityAnnouncements",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Affixs_CreateTime",
                table: "Affixs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Affixs_IsEnable",
                table: "Affixs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Affixs_UpdateTime",
                table: "Affixs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CreateTime",
                table: "Announcements",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_IsEnable",
                table: "Announcements",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_UpdateTime",
                table: "Announcements",
                column: "UpdateTime");

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

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_AboutId",
                table: "CoinLogs",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_CoinLogId",
                table: "CoinLogs",
                column: "CoinLogId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_CreateTime",
                table: "CoinLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_IsEnable",
                table: "CoinLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_PlayerId",
                table: "CoinLogs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_Type",
                table: "CoinLogs",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_UpdateTime",
                table: "CoinLogs",
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
                name: "IX_MerchantsBanks_BannerAffixId",
                table: "MerchantsBanks",
                column: "BannerAffixId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantsBanks_CreateTime",
                table: "MerchantsBanks",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantsBanks_IsEnable",
                table: "MerchantsBanks",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantsBanks_ScanAffixId",
                table: "MerchantsBanks",
                column: "ScanAffixId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantsBanks_UpdateTime",
                table: "MerchantsBanks",
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
                name: "IX_NotReportPlayers_CreateTime",
                table: "NotReportPlayers",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_NotReportPlayers_IsEnable",
                table: "NotReportPlayers",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_NotReportPlayers_PlayerId",
                table: "NotReportPlayers",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotReportPlayers_UpdateTime",
                table: "NotReportPlayers",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreateTime",
                table: "Permissions",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IsEnable",
                table: "Permissions",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UpdateTime",
                table: "Permissions",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PictureNewses_AffixId",
                table: "PictureNewses",
                column: "AffixId");

            migrationBuilder.CreateIndex(
                name: "IX_PictureNewses_CreateTime",
                table: "PictureNewses",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PictureNewses_IsEnable",
                table: "PictureNewses",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_PictureNewses_UpdateTime",
                table: "PictureNewses",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformMoneyLogs_CreateTime",
                table: "PlatformMoneyLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformMoneyLogs_IsEnable",
                table: "PlatformMoneyLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformMoneyLogs_PlatformId",
                table: "PlatformMoneyLogs",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformMoneyLogs_PlayerId",
                table: "PlatformMoneyLogs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformMoneyLogs_UpdateTime",
                table: "PlatformMoneyLogs",
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
                name: "IX_PlayerBanks_CreateTime",
                table: "PlayerBanks",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBanks_IsEnable",
                table: "PlayerBanks",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBanks_PlayerId",
                table: "PlayerBanks",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBanks_SystemBankId",
                table: "PlayerBanks",
                column: "SystemBankId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBanks_UpdateTime",
                table: "PlayerBanks",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBanks_CardNumber_PlayerId",
                table: "PlayerBanks",
                columns: new[] { "CardNumber", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLoginLogs_CreateTime",
                table: "PlayerLoginLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLoginLogs_IsEnable",
                table: "PlayerLoginLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLoginLogs_PlayerId",
                table: "PlayerLoginLogs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLoginLogs_UpdateTime",
                table: "PlayerLoginLogs",
                column: "UpdateTime");

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
                name: "IX_PlayerOperateLogs_CreateTime",
                table: "PlayerOperateLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOperateLogs_IsEnable",
                table: "PlayerOperateLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOperateLogs_PlayerId",
                table: "PlayerOperateLogs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOperateLogs_UpdateTime",
                table: "PlayerOperateLogs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CreateTime",
                table: "Players",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Players_DailyWageRate",
                table: "Players",
                column: "DailyWageRate");

            migrationBuilder.CreateIndex(
                name: "IX_Players_DividendRate",
                table: "Players",
                column: "DividendRate");

            migrationBuilder.CreateIndex(
                name: "IX_Players_IsEnable",
                table: "Players",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                table: "Players",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_ParentPlayerId",
                table: "Players",
                column: "ParentPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Rebate",
                table: "Players",
                column: "Rebate");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ReportDate",
                table: "Players",
                column: "ReportDate");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UpdateTime",
                table: "Players",
                column: "UpdateTime");

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
                name: "IX_Recharges_CreateTime",
                table: "Recharges",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Recharges_IsEnable",
                table: "Recharges",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Recharges_MerchantsBankId",
                table: "Recharges",
                column: "MerchantsBankId");

            migrationBuilder.CreateIndex(
                name: "IX_Recharges_OrderNo",
                table: "Recharges",
                column: "OrderNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recharges_PlayerId",
                table: "Recharges",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Recharges_UpdateTime",
                table: "Recharges",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCaches_CreateTime",
                table: "ReportCaches",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCaches_IsEnable",
                table: "ReportCaches",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCaches_UpdateTime",
                table: "ReportCaches",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCaches_Value",
                table: "ReportCaches",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreateTime",
                table: "Roles",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsEnable",
                table: "Roles",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UpdateTime",
                table: "Roles",
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

            migrationBuilder.CreateIndex(
                name: "IX_SystemBanks_AffixId",
                table: "SystemBanks",
                column: "AffixId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemBanks_CreateTime",
                table: "SystemBanks",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SystemBanks_IsEnable",
                table: "SystemBanks",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_SystemBanks_Name",
                table: "SystemBanks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemBanks_UpdateTime",
                table: "SystemBanks",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfigs_CreateTime",
                table: "SystemConfigs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfigs_IsEnable",
                table: "SystemConfigs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfigs_Type",
                table: "SystemConfigs",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfigs_UpdateTime",
                table: "SystemConfigs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoPermissions_UserInfoId",
                table: "UserInfoPermissions",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoRoles_UserInfoId",
                table: "UserInfoRoles",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_CreateTime",
                table: "UserInfos",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_IsEnable",
                table: "UserInfos",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_Name",
                table: "UserInfos",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UpdateTime",
                table: "UserInfos",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperateLogs_CreateTime",
                table: "UserOperateLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperateLogs_IsEnable",
                table: "UserOperateLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperateLogs_UpdateTime",
                table: "UserOperateLogs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperateLogs_UserInfoId",
                table: "UserOperateLogs",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_CreateTime",
                table: "Withdraws",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_IsEnable",
                table: "Withdraws",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_OrderNo",
                table: "Withdraws",
                column: "OrderNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_PlayerBankId",
                table: "Withdraws",
                column: "PlayerBankId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_Status",
                table: "Withdraws",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_UpdateTime",
                table: "Withdraws",
                column: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityAnnouncements");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "ChasingOrderDetails");

            migrationBuilder.DropTable(
                name: "ClientBlacks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "CoinLogs");

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
                name: "NotReportPlayers");

            migrationBuilder.DropTable(
                name: "PictureNewses");

            migrationBuilder.DropTable(
                name: "PlatformMoneyLogs");

            migrationBuilder.DropTable(
                name: "PlayerLoginLogs");

            migrationBuilder.DropTable(
                name: "PlayerLotteryTypes");

            migrationBuilder.DropTable(
                name: "PlayerOperateLogs");

            migrationBuilder.DropTable(
                name: "ProxyRegisters");

            migrationBuilder.DropTable(
                name: "RebateLogs");

            migrationBuilder.DropTable(
                name: "Recharges");

            migrationBuilder.DropTable(
                name: "ReportCaches");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SoftwareExpireds");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropTable(
                name: "UserInfoPermissions");

            migrationBuilder.DropTable(
                name: "UserInfoRoles");

            migrationBuilder.DropTable(
                name: "UserOperateLogs");

            migrationBuilder.DropTable(
                name: "Withdraws");

            migrationBuilder.DropTable(
                name: "ChasingOrders");

            migrationBuilder.DropTable(
                name: "DividendDates");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "MerchantsBanks");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "PlayerBanks");

            migrationBuilder.DropTable(
                name: "BetModes");

            migrationBuilder.DropTable(
                name: "LotteryDatas");

            migrationBuilder.DropTable(
                name: "LotteryPlayDetails");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "SystemBanks");

            migrationBuilder.DropTable(
                name: "LotteryPlayTypes");

            migrationBuilder.DropTable(
                name: "LotteryTypes");

            migrationBuilder.DropTable(
                name: "Affixs");

            migrationBuilder.DropTable(
                name: "LotteryClassifies");
        }
    }
}
