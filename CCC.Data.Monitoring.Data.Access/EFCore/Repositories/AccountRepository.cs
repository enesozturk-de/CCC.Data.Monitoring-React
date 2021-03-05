using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;

namespace CCC.Data.Monitoring.Data.Access.EFCore.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(MonitoringDbContext context) : base(context)
        {
        }
    }
}
