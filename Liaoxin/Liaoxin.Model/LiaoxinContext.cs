using Microsoft.EntityFrameworkCore;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class LiaoxinContext : ZzbDbContext
    {
        protected LiaoxinContext()
        {

        }

        public LiaoxinContext(DbContextOptions options) : base(options)
        {
        }

        public static LiaoxinContext CreateContext()
        {
            return new LiaoxinContext();
        }

      //  public DbSet<Test> Tests { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Area> Areas { get; set; }
        public DbSet<ClientLoginLog> ClientLoginLogs { get; set; }



        public DbSet<CoinLog> CoinLogs { get; set; }


        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientRelation> ClientRelations { get; set; }

        public DbSet<ClientRelationDetail> ClientRelationDetails { get; set; }

        public DbSet<ClientEquipment> ClientEquipments { get; set; }

        public DbSet<ClientOperateLog> ClientOperateLogs { get; set; }

        


                   public DbSet<RedPacket> RedPackets { get; set; }
        public DbSet<RedPacketReceive> RedPacketReceives { get; set; }

        public DbSet<ClientAdd> ClientAdds { get; set; }


        public DbSet<ClientAddDetail> ClientAddDetails { get; set; }

        public DbSet<ClientTag> ClientTags { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupClient> GroupClients { get; set; }

        public DbSet<GroupManager> GroupManagers { get; set; }

        public DbSet<SystemBank> SystemBanks { get; set; }

        public DbSet<ClientBank> ClientBanks { get; set; }

        public DbSet<MerchantsBank> MerchantsBanks { get; set; }

        public DbSet<Recharge> Recharges { get; set; }

        public DbSet<SystemConfig> SystemConfigs { get; set; }

        public DbSet<Withdraw> Withdraws { get; set; }

     //   public DbSet<RebateLog> RebateLogs { get; set; }

      //  public DbSet<GiftEvent> GiftEvents { get; set; }

      //  public DbSet<GiftReceive> GiftReceives { get; set; }

       // public DbSet<DailyWage> DailyWages { get; set; }

        public DbSet<ReportCache> ReportCaches { get; set; }

    //    public DbSet<DailyWageLog> DailyWageLogs { get; set; }

     //   public DbSet<DividendDate> DividendDates { get; set; }

     //   public DbSet<DividendSetting> DividendSettings { get; set; }

      //  public DbSet<DividendLog> DividendLogs { get; set; }

        public DbSet<UserOperateLog> UserOperateLogs { get; set; }

     //   public DbSet<ProxyRegister> ProxyRegisters { get; set; }



        public DbSet<Announcement> Announcements { get; set; }

    //    public DbSet<Message> Messages { get; set; }

     //   public DbSet<PlayerLotteryType> PlayerLotteryTypes { get; set; }

      //  public DbSet<PlatformMoneyLog> PlatformMoneyLogs { get; set; }

     //   public DbSet<Platform> Platforms { get; set; }

        public DbSet<NotReportPlayer> NotReportPlayers { get; set; }

        public DbSet<ActivityAnnouncement> ActivityAnnouncements { get; set; }

        public DbSet<PictureNews> PictureNewses { get; set; }

     //   public DbSet<ChasingOrder> ChasingOrders { get; set; }

      //  public DbSet<ChasingOrderDetail> ChasingOrderDetails { get; set; }

      //  public DbSet<SoftwareExpired> SoftwareExpireds { get; set; }
    }
}