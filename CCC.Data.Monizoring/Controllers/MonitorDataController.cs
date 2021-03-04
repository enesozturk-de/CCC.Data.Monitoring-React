using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Operations.OperationHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCC.Data.Monitoring.Controllers
{  
    [Route("api/[controller]")]
    [ApiController] 
    public class MonitorDataController : ControllerBase
    {
        private readonly MonitoringDbContext _monitoringDbContext;
        private readonly IConfiguration _configuration;
        public MonitorDataController(MonitoringDbContext monitoringDbContext, IConfiguration configuration)
        {
            _monitoringDbContext = monitoringDbContext;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public List<ScreenData> Get()
        {
            List<ScreenData> screenDatas = new List<ScreenData>();
            var monitorDatas = _monitoringDbContext.MonitorData.ToList();
            var queueGroups = _monitoringDbContext.QueueGroup.ToList();

            foreach (var monitorData in monitorDatas)
            {
                var currentQueueGroup = queueGroups.SingleOrDefault(x => x.ID == monitorData.QueueGroupID);
                screenDatas.Add(new ScreenData
                {
                    Handled = monitorData.Handled,
                    Offered = monitorData.Offered,
                    QueueGroupName = currentQueueGroup.Name,
                    AverageHandlingTime = OperationHelper.CalculateAverageHandlingTime(monitorData),
                    AverageTalkTime = OperationHelper.CalculateAverageTalkTime(monitorData),
                    ServiceLevel = OperationHelper.CalculateServiceLevel(monitorData),
                    ColumnColour = OperationHelper.DecideColumnColour(currentQueueGroup, monitorData)

                });
            }

            return screenDatas;
        }
    }
}
