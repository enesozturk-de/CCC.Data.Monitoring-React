using CCC.Data.Monitoring.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCC.Data.Monitoring.Concrete.Interfaces
{
    public interface IMonitorDataController
    {
        List<ScreenData> Get();
    }
}
