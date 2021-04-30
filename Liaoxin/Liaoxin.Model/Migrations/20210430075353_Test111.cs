using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Liaoxin.Model.Migrations
{
    public partial class Test111 : Migration
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
                name: "Areas",
                columns: table => new
                {
                    AreaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    LongCode = table.Column<string>(nullable: true),
                    ParentCode = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
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
                    Telephone = table.Column<string>(nullable: true),
                    CharacterSignature = table.Column<string>(nullable: true),
                    AreaCode = table.Column<string>(nullable: true),
                    CurrentDeviceId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AddMeNeedChecked = table.Column<bool>(nullable: false),
                    ShowFriendCircle = table.Column<int>(nullable: false),
                    UpadteMind = table.Column<bool>(nullable: false),
                    IsFreeze = table.Column<bool>(nullable: false),
                    RealName = table.Column<string>(nullable: true),
                    UniqueNo = table.Column<string>(nullable: true),
                    UniqueFrontImg = table.Column<string>(nullable: true),
                    UniqueBackImg = table.Column<string>(nullable: true),
                    CanWithdraw = table.Column<bool>(nullable: false),
                    FontSize = table.Column<int>(nullable: false),
                    HandFree = table.Column<bool>(nullable: false),
                    WifiVideoPlay = table.Column<bool>(nullable: false),
                    NewMessageNotication = table.Column<bool>(nullable: false),
                    VideoMessageNotication = table.Column<bool>(nullable: false),
                    ShowMessageNotication = table.Column<bool>(nullable: false),
                    AppOpenWhileSound = table.Column<bool>(nullable: false),
                    OpenWhileShake = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "ClientTags",
                columns: table => new
                {
                    ClientTagId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTags", x => x.ClientTagId);
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
                    ReportDate = table.Column<DateTime>(nullable: false)
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
                name: "ClientAddDetails",
                columns: table => new
                {
                    ClientAddDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    AddRemark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAddDetails", x => x.ClientAddDetailId);
                    table.ForeignKey(
                        name: "FK_ClientAddDetails_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientAdds",
                columns: table => new
                {
                    ClientAddId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ClientEquipments",
                columns: table => new
                {
                    ClientEquipmentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    LastLoginDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEquipments", x => x.ClientEquipmentId);
                    table.ForeignKey(
                        name: "FK_ClientEquipments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientLoginLogs",
                columns: table => new
                {
                    ClientLoginLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    IP = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientLoginLogs", x => x.ClientLoginLogId);
                    table.ForeignKey(
                        name: "FK_ClientLoginLogs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientOperateLogs",
                columns: table => new
                {
                    ClientOperateLogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOperateLogs", x => x.ClientOperateLogId);
                    table.ForeignKey(
                        name: "FK_ClientOperateLogs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRelations",
                columns: table => new
                {
                    ClientRelationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    RelationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRelations", x => x.ClientRelationId);
                    table.ForeignKey(
                        name: "FK_ClientRelations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
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
                    ClientId = table.Column<int>(nullable: false),
                    FlowCoin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Coin = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Type = table.Column<int>(nullable: false),
                    AboutId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinLogs", x => x.CoinLogId);
                    table.ForeignKey(
                        name: "FK_CoinLogs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    HuanxinGroupId = table.Column<string>(nullable: true),
                    Notice = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    AllBlock = table.Column<bool>(nullable: false),
                    SureConfirmInvite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
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
                name: "ClientBanks",
                columns: table => new
                {
                    ClientBankId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    SystemBankId = table.Column<int>(nullable: false),
                    CardNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBanks", x => x.ClientBankId);
                    table.ForeignKey(
                        name: "FK_ClientBanks_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientBanks_SystemBanks_SystemBankId",
                        column: x => x.SystemBankId,
                        principalTable: "SystemBanks",
                        principalColumn: "SystemBankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRelationDetails",
                columns: table => new
                {
                    ClientRelationDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    ClientRelationId = table.Column<int>(nullable: false),
                    Telephone = table.Column<string>(nullable: true),
                    ClientTagId = table.Column<int>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    NotSee = table.Column<bool>(nullable: false),
                    NotLetSee = table.Column<bool>(nullable: false),
                    SpecialAttention = table.Column<bool>(nullable: false),
                    ClientRemark = table.Column<string>(nullable: true),
                    AddSource = table.Column<int>(nullable: false),
                    MutipleGroup = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRelationDetails", x => x.ClientRelationDetailId);
                    table.ForeignKey(
                        name: "FK_ClientRelationDetails_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRelationDetails_ClientRelations_ClientRelationId",
                        column: x => x.ClientRelationId,
                        principalTable: "ClientRelations",
                        principalColumn: "ClientRelationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRelationDetails_ClientTags_ClientTagId",
                        column: x => x.ClientTagId,
                        principalTable: "ClientTags",
                        principalColumn: "ClientTagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupClients",
                columns: table => new
                {
                    GroupClientId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    MyNickName = table.Column<string>(nullable: true),
                    ShowOtherNickName = table.Column<bool>(nullable: false),
                    SaveMyGroup = table.Column<bool>(nullable: false),
                    SetTop = table.Column<bool>(nullable: false),
                    SetNoDisturb = table.Column<bool>(nullable: false),
                    BackgroundImg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupClients", x => x.GroupClientId);
                    table.ForeignKey(
                        name: "FK_GroupClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupClients_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupManagers",
                columns: table => new
                {
                    GroupManagerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupManagers", x => x.GroupManagerId);
                    table.ForeignKey(
                        name: "FK_GroupManagers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupManagers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
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
                    ClientBankId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Version = table.Column<DateTime>(rowVersion: true, nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraws", x => x.WithdrawId);
                    table.ForeignKey(
                        name: "FK_Withdraws_ClientBanks_ClientBankId",
                        column: x => x.ClientBankId,
                        principalTable: "ClientBanks",
                        principalColumn: "ClientBankId",
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
                name: "IX_Areas_Code",
                table: "Areas",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Areas_CreateTime",
                table: "Areas",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_IsEnable",
                table: "Areas",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Level",
                table: "Areas",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_UpdateTime",
                table: "Areas",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddDetails_ClientId",
                table: "ClientAddDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddDetails_CreateTime",
                table: "ClientAddDetails",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddDetails_IsEnable",
                table: "ClientAddDetails",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddDetails_UpdateTime",
                table: "ClientAddDetails",
                column: "UpdateTime");

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

            migrationBuilder.CreateIndex(
                name: "IX_ClientBanks_ClientId",
                table: "ClientBanks",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBanks_CreateTime",
                table: "ClientBanks",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBanks_IsEnable",
                table: "ClientBanks",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBanks_SystemBankId",
                table: "ClientBanks",
                column: "SystemBankId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBanks_UpdateTime",
                table: "ClientBanks",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBanks_CardNumber_ClientId",
                table: "ClientBanks",
                columns: new[] { "CardNumber", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientEquipments_ClientId",
                table: "ClientEquipments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEquipments_CreateTime",
                table: "ClientEquipments",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEquipments_IsEnable",
                table: "ClientEquipments",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEquipments_UpdateTime",
                table: "ClientEquipments",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLoginLogs_ClientId",
                table: "ClientLoginLogs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLoginLogs_CreateTime",
                table: "ClientLoginLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLoginLogs_IsEnable",
                table: "ClientLoginLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLoginLogs_UpdateTime",
                table: "ClientLoginLogs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOperateLogs_ClientId",
                table: "ClientOperateLogs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOperateLogs_CreateTime",
                table: "ClientOperateLogs",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOperateLogs_IsEnable",
                table: "ClientOperateLogs",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOperateLogs_UpdateTime",
                table: "ClientOperateLogs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelationDetails_ClientId",
                table: "ClientRelationDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelationDetails_ClientRelationId",
                table: "ClientRelationDetails",
                column: "ClientRelationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelationDetails_ClientTagId",
                table: "ClientRelationDetails",
                column: "ClientTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelationDetails_CreateTime",
                table: "ClientRelationDetails",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelationDetails_IsEnable",
                table: "ClientRelationDetails",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelationDetails_UpdateTime",
                table: "ClientRelationDetails",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelations_ClientId",
                table: "ClientRelations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelations_CreateTime",
                table: "ClientRelations",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelations_IsEnable",
                table: "ClientRelations",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelations_UpdateTime",
                table: "ClientRelations",
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
                name: "IX_Clients_Telephone",
                table: "Clients",
                column: "Telephone");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UpdateTime",
                table: "Clients",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTags_CreateTime",
                table: "ClientTags",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTags_IsEnable",
                table: "ClientTags",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTags_UpdateTime",
                table: "ClientTags",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_AboutId",
                table: "CoinLogs",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_ClientId",
                table: "CoinLogs",
                column: "ClientId");

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
                name: "IX_CoinLogs_Type",
                table: "CoinLogs",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_CoinLogs_UpdateTime",
                table: "CoinLogs",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GroupClients_ClientId",
                table: "GroupClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupClients_CreateTime",
                table: "GroupClients",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GroupClients_GroupId",
                table: "GroupClients",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupClients_IsEnable",
                table: "GroupClients",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_GroupClients_UpdateTime",
                table: "GroupClients",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GroupManagers_ClientId",
                table: "GroupManagers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupManagers_CreateTime",
                table: "GroupManagers",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_GroupManagers_GroupId",
                table: "GroupManagers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupManagers_IsEnable",
                table: "GroupManagers",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_GroupManagers_UpdateTime",
                table: "GroupManagers",
                column: "UpdateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ClientId",
                table: "Groups",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreateTime",
                table: "Groups",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_IsEnable",
                table: "Groups",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UpdateTime",
                table: "Groups",
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
                name: "IX_Withdraws_ClientBankId",
                table: "Withdraws",
                column: "ClientBankId");

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
                name: "Areas");

            migrationBuilder.DropTable(
                name: "ClientAddDetails");

            migrationBuilder.DropTable(
                name: "ClientAdds");

            migrationBuilder.DropTable(
                name: "ClientEquipments");

            migrationBuilder.DropTable(
                name: "ClientLoginLogs");

            migrationBuilder.DropTable(
                name: "ClientOperateLogs");

            migrationBuilder.DropTable(
                name: "ClientRelationDetails");

            migrationBuilder.DropTable(
                name: "CoinLogs");

            migrationBuilder.DropTable(
                name: "GroupClients");

            migrationBuilder.DropTable(
                name: "GroupManagers");

            migrationBuilder.DropTable(
                name: "NotReportPlayers");

            migrationBuilder.DropTable(
                name: "PictureNewses");

            migrationBuilder.DropTable(
                name: "Recharges");

            migrationBuilder.DropTable(
                name: "ReportCaches");

            migrationBuilder.DropTable(
                name: "RolePermissions");

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
                name: "ClientRelations");

            migrationBuilder.DropTable(
                name: "ClientTags");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "MerchantsBanks");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "ClientBanks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "SystemBanks");

            migrationBuilder.DropTable(
                name: "Affixs");
        }
    }
}
