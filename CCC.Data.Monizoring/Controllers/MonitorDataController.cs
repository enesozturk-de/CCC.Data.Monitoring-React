using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Operations.OperationHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CCC.Data.Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class MonitorDataController : ControllerBase, IMonitorDataController
    {
        private readonly IMonitorDataRepository _monitorDataRepository;
        private readonly IQueueGroupRepository _queueGroupRepository;
        public MonitorDataController(IMonitorDataRepository monitorDataRepository, IQueueGroupRepository queueGroupRepository)
        {
            _monitorDataRepository = monitorDataRepository;
            _queueGroupRepository = queueGroupRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public List<ScreenData> Get()
        {
            List<ScreenData> screenDatas = new List<ScreenData>();
            var monitorDatas = _monitorDataRepository.GetAll();
            var queueGroups = _queueGroupRepository.GetAll();

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
