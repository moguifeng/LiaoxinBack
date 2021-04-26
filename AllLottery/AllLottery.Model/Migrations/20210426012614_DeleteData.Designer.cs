﻿// <auto-generated />
using System;
using AllLottery.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AllLottery.Model.Migrations
{
    [DbContext(typeof(LotteryContext))]
    [Migration("20210426012614_DeleteData")]
    partial class DeleteData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AllLottery.Model.ActivityAnnouncement", b =>
                {
                    b.Property<int>("ActivityAnnouncementId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AffixId");

                    b.Property<DateTime>("BeginTime");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("SortIndex");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("ActivityAnnouncementId");

                    b.HasIndex("AffixId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("UpdateTime");

                    b.ToTable("ActivityAnnouncements");
                });

            modelBuilder.Entity("AllLottery.Model.Announcement", b =>
                {
                    b.Property<int>("AnnouncementId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<DateTime>("ShowOpenTime");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("AnnouncementId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("UpdateTime");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("AllLottery.Model.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AddMeNeedChecked");

                    b.Property<string>("CharacterSignature");

                    b.Property<int>("City");

                    b.Property<decimal>("Coin")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("CoinPassword");

                    b.Property<int>("Country");

                    b.Property<string>("Cover");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("CurrentDeviceId");

                    b.Property<string>("Email");

                    b.Property<string>("HuanXinId");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("LiaoxinNumber")
                        .HasMaxLength(20);

                    b.Property<string>("NickName")
                        .HasMaxLength(20);

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<int>("Province");

                    b.Property<int>("ShowFriendCircle");

                    b.Property<bool>("UpadteMind");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("LiaoxinNumber")
                        .IsUnique();

                    b.HasIndex("UpdateTime");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("AllLottery.Model.ClientBlack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("FromClientId");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("ToClientId");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("UpdateTime");

                    b.ToTable("ClientBlacks");
                });

            modelBuilder.Entity("AllLottery.Model.CoinLog", b =>
                {
                    b.Property<int>("CoinLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AboutId");

                    b.Property<decimal>("Coin")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<DateTime>("CreateTime");

                    b.Property<decimal>("FCoin")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("FlowCoin")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("FlowFCoin")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("PlayerId");

                    b.Property<string>("Remark");

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("CoinLogId");

                    b.HasIndex("AboutId");

                    b.HasIndex("CoinLogId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("PlayerId");

                    b.HasIndex("Type");

                    b.HasIndex("UpdateTime");

                    b.ToTable("CoinLogs");
                });

            modelBuilder.Entity("AllLottery.Model.MerchantsBank", b =>
                {
                    b.Property<int>("MerchantsBankId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account");

                    b.Property<string>("Aisle");

                    b.Property<string>("BankUserName");

                    b.Property<int>("BannerAffixId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<int>("IndexSort");

                    b.Property<bool>("IsEnable");

                    b.Property<bool>("IsUseful");

                    b.Property<decimal>("Max")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<int>("MerchantsBankShowType");

                    b.Property<string>("MerchantsKey");

                    b.Property<string>("MerchantsNumber");

                    b.Property<decimal>("Min")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("Name");

                    b.Property<int?>("ScanAffixId");

                    b.Property<int>("ThirdPayMerchantsType");

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("MerchantsBankId");

                    b.HasIndex("BannerAffixId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("ScanAffixId");

                    b.HasIndex("UpdateTime");

                    b.ToTable("MerchantsBanks");
                });

            modelBuilder.Entity("AllLottery.Model.NotReportPlayer", b =>
                {
                    b.Property<int>("NotReportPlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("PlayerId");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("NotReportPlayerId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.HasIndex("UpdateTime");

                    b.ToTable("NotReportPlayers");
                });

            modelBuilder.Entity("AllLottery.Model.PictureNews", b =>
                {
                    b.Property<int>("PictureNewsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AffixId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("SortIndex");

                    b.Property<DateTime>("UpdateTime");

                    b.Property<string>("Url");

                    b.HasKey("PictureNewsId");

                    b.HasIndex("AffixId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("UpdateTime");

                    b.ToTable("PictureNewses");
                });

            modelBuilder.Entity("AllLottery.Model.PlatformMoneyLog", b =>
                {
                    b.Property<int>("PlatformMoneyLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<decimal>("FlowMoney")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("PlayerId");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("PlatformMoneyLogId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("PlayerId");

                    b.HasIndex("UpdateTime");

                    b.ToTable("PlatformMoneyLogs");
                });

            modelBuilder.Entity("AllLottery.Model.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BetMoney")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<DateTime?>("Birthday");

                    b.Property<bool>("CanWithdraw");

                    b.Property<decimal>("Coin")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("CoinPassword");

                    b.Property<DateTime>("CreateTime");

                    b.Property<decimal?>("DailyWageRate")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal?>("DividendRate")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("FCoin")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("GiftMoney")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<bool>("IsChangePassword");

                    b.Property<bool>("IsEnable");

                    b.Property<bool>("IsFreeze");

                    b.Property<decimal>("LastBetMoney")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<int?>("ParentPlayerId");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("QQ");

                    b.Property<decimal>("Rebate")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("RebateMoney")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("RechargeMoney")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<DateTime>("ReportDate");

                    b.Property<string>("Title")
                        .HasMaxLength(20);

                    b.Property<DateTime>("UpdateTime");

                    b.Property<string>("WeChat");

                    b.Property<decimal>("WinMoney")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<decimal>("WithdrawMoney")
                        .HasColumnType("decimal(18, 6)");

                    b.HasKey("PlayerId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("DailyWageRate");

                    b.HasIndex("DividendRate");

                    b.HasIndex("IsEnable");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentPlayerId");

                    b.HasIndex("Rebate");

                    b.HasIndex("ReportDate");

                    b.HasIndex("UpdateTime");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("AllLottery.Model.PlayerBank", b =>
                {
                    b.Property<int>("PlayerBankId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CardNumber")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("PayeeName");

                    b.Property<int>("PlayerId");

                    b.Property<int>("SystemBankId");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("PlayerBankId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SystemBankId");

                    b.HasIndex("UpdateTime");

                    b.HasIndex("CardNumber", "PlayerId")
                        .IsUnique();

                    b.ToTable("PlayerBanks");
                });

            modelBuilder.Entity("AllLottery.Model.PlayerLoginLog", b =>
                {
                    b.Property<int>("PlayerLoginLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("IP");

                    b.Property<bool>("IsApp");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("PlayerId");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("PlayerLoginLogId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("PlayerId");

                    b.HasIndex("UpdateTime");

                    b.ToTable("PlayerLoginLogs");
                });

            modelBuilder.Entity("AllLottery.Model.PlayerOperateLog", b =>
                {
                    b.Property<int>("PlayerOperateLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("Message");

                    b.Property<int>("PlayerId");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("PlayerOperateLogId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("PlayerId");

                    b.HasIndex("UpdateTime");

                    b.ToTable("PlayerOperateLogs");
                });

            modelBuilder.Entity("AllLottery.Model.Recharge", b =>
                {
                    b.Property<int>("RechargeId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<int?>("MerchantsBankId");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("OrderNo")
                        .HasMaxLength(20);

                    b.Property<int>("PlayerId");

                    b.Property<string>("Remark");

                    b.Property<int>("State");

                    b.Property<DateTime>("UpdateTime");

                    b.Property<DateTime?>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("RechargeId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("MerchantsBankId");

                    b.HasIndex("OrderNo")
                        .IsUnique();

                    b.HasIndex("PlayerId");

                    b.HasIndex("UpdateTime");

                    b.ToTable("Recharges");
                });

            modelBuilder.Entity("AllLottery.Model.ReportCache", b =>
                {
                    b.Property<int>("ReportCacheId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("PlayerId");

                    b.Property<DateTime>("QueryTime");

                    b.Property<string>("Type")
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdateTime");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18, 6)");

                    b.HasKey("ReportCacheId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("UpdateTime");

                    b.HasIndex("Value");

                    b.ToTable("ReportCaches");
                });

            modelBuilder.Entity("AllLottery.Model.SystemBank", b =>
                {
                    b.Property<int>("SystemBankId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AffixId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<int>("SortIndex");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("SystemBankId");

                    b.HasIndex("AffixId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UpdateTime");

                    b.ToTable("SystemBanks");
                });

            modelBuilder.Entity("AllLottery.Model.SystemConfig", b =>
                {
                    b.Property<int>("SystemConfigId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<int>("Type");

                    b.Property<DateTime>("UpdateTime");

                    b.Property<string>("Value");

                    b.HasKey("SystemConfigId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.HasIndex("UpdateTime");

                    b.ToTable("SystemConfigs");
                });

            modelBuilder.Entity("AllLottery.Model.UserOperateLog", b =>
                {
                    b.Property<int>("UserOperateLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("Message");

                    b.Property<DateTime>("UpdateTime");

                    b.Property<int>("UserInfoId");

                    b.HasKey("UserOperateLogId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("UpdateTime");

                    b.HasIndex("UserInfoId");

                    b.ToTable("UserOperateLogs");
                });

            modelBuilder.Entity("AllLottery.Model.Withdraw", b =>
                {
                    b.Property<int>("WithdrawId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<string>("OrderNo")
                        .HasMaxLength(20);

                    b.Property<int>("PlayerBankId");

                    b.Property<string>("Remark");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateTime");

                    b.Property<DateTime?>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("WithdrawId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("OrderNo")
                        .IsUnique();

                    b.HasIndex("PlayerBankId");

                    b.HasIndex("Status");

                    b.HasIndex("UpdateTime");

                    b.ToTable("Withdraws");
                });

            modelBuilder.Entity("Zzb.EF.Affix", b =>
                {
                    b.Property<int>("AffixId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("Path");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("AffixId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("UpdateTime");

                    b.ToTable("Affixs");
                });

            modelBuilder.Entity("Zzb.EF.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("NavId");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("PermissionId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("UpdateTime");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Zzb.EF.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("RoleId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UpdateTime");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Zzb.EF.RolePermission", b =>
                {
                    b.Property<int>("PermissionId");

                    b.Property<int>("RoleId");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Zzb.EF.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("UserInfoId");

                    b.HasIndex("CreateTime");

                    b.HasIndex("IsEnable");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UpdateTime");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Zzb.EF.UserInfoPermission", b =>
                {
                    b.Property<int>("PermissionId");

                    b.Property<int>("UserInfoId");

                    b.HasKey("PermissionId", "UserInfoId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("UserInfoPermissions");
                });

            modelBuilder.Entity("Zzb.EF.UserInfoRole", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("UserInfoId");

                    b.HasKey("RoleId", "UserInfoId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("UserInfoRoles");
                });

            modelBuilder.Entity("AllLottery.Model.ActivityAnnouncement", b =>
                {
                    b.HasOne("Zzb.EF.Affix", "Affix")
                        .WithMany()
                        .HasForeignKey("AffixId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.CoinLog", b =>
                {
                    b.HasOne("AllLottery.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.MerchantsBank", b =>
                {
                    b.HasOne("Zzb.EF.Affix", "BannerAffix")
                        .WithMany()
                        .HasForeignKey("BannerAffixId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zzb.EF.Affix", "ScanAffix")
                        .WithMany()
                        .HasForeignKey("ScanAffixId");
                });

            modelBuilder.Entity("AllLottery.Model.NotReportPlayer", b =>
                {
                    b.HasOne("AllLottery.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.PictureNews", b =>
                {
                    b.HasOne("Zzb.EF.Affix", "Affix")
                        .WithMany()
                        .HasForeignKey("AffixId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.PlatformMoneyLog", b =>
                {
                    b.HasOne("AllLottery.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.Player", b =>
                {
                    b.HasOne("AllLottery.Model.Player", "ParentPlayer")
                        .WithMany("Players")
                        .HasForeignKey("ParentPlayerId");
                });

            modelBuilder.Entity("AllLottery.Model.PlayerBank", b =>
                {
                    b.HasOne("AllLottery.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AllLottery.Model.SystemBank", "SystemBank")
                        .WithMany()
                        .HasForeignKey("SystemBankId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.PlayerLoginLog", b =>
                {
                    b.HasOne("AllLottery.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.PlayerOperateLog", b =>
                {
                    b.HasOne("AllLottery.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.Recharge", b =>
                {
                    b.HasOne("AllLottery.Model.MerchantsBank", "MerchantsBank")
                        .WithMany()
                        .HasForeignKey("MerchantsBankId");

                    b.HasOne("AllLottery.Model.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.SystemBank", b =>
                {
                    b.HasOne("Zzb.EF.Affix", "Affix")
                        .WithMany()
                        .HasForeignKey("AffixId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.UserOperateLog", b =>
                {
                    b.HasOne("Zzb.EF.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AllLottery.Model.Withdraw", b =>
                {
                    b.HasOne("AllLottery.Model.PlayerBank", "PlayerBank")
                        .WithMany()
                        .HasForeignKey("PlayerBankId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zzb.EF.RolePermission", b =>
                {
                    b.HasOne("Zzb.EF.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zzb.EF.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zzb.EF.UserInfoPermission", b =>
                {
                    b.HasOne("Zzb.EF.Permission", "Permission")
                        .WithMany("UserInfoPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zzb.EF.UserInfo", "UserInfo")
                        .WithMany("UserInfoPermissions")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zzb.EF.UserInfoRole", b =>
                {
                    b.HasOne("Zzb.EF.Role", "Role")
                        .WithMany("UserInfoRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zzb.EF.UserInfo", "UserInfo")
                        .WithMany("UserInfoRoles")
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
