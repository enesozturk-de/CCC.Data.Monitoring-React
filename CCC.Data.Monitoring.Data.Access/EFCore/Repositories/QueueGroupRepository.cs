using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;

namespace CCC.Data.Monitoring.Data.Access.EFCore.Repositories
{
    public class QueueGroupRepository : Repository<QueueGroup>, IQueueGroupRepository
    {
        public QueueGroupRepository(MonitoringDbContext context) : base(context)
        {
        }
    }
}
