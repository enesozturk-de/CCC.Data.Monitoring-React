using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCC.Data.Monitoring.Data.Access.EFCore.Repositories
{
    public class MonitorDataRepository : Repository<MonitorData>, IMonitorDataRepository
    {
        public MonitorDataRepository(MonitoringDbContext context) : base(context)
        {
        }
    }
}
