using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Data.Access.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CCC.Data.Monitoring.Data.Access.EFCore
{
    public class MonitoringDbContext : DbContext
    {
        public MonitoringDbContext(DbContextOptions<MonitoringDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new MonitorDataMapping());
            modelBuilder.ApplyConfiguration(new QueueGroupMapping()); 
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<MonitorData> MonitorData { get; set; }
        public DbSet<QueueGroup> QueueGroup { get; set; }

    }
}
