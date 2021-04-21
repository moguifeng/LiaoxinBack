using Microsoft.EntityFrameworkCore;

namespace AllLottery.Tool
{
    public class MsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=AllLottery;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Player> Players { get; set; }
    }
}