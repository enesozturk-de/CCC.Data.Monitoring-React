using CCC.Data.Monitoring.Concrete.Constants;
using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Data.Access.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CCC.Data.Monitoring.Data.Access
{
    public class DataGenerator : IDataGenerator
    {
        private readonly MonitoringDbContext _monitoringDbContext;
        private readonly IConfiguration _configuartion;
        public DataGenerator(MonitoringDbContext dbContext, IConfiguration configuration)
        {
            _monitoringDbContext = dbContext;
            _configuartion = configuration;
        }
        public void AddData()
        {
            var accountJsonString = DataGenerationHelper.GetJsonDataString(Constants.Account);
            var monitorDataJsonString = DataGenerationHelper.GetJsonDataString(Constants.MonitorData);
            var queueGroupJsonString = DataGenerationHelper.GetJsonDataString(Constants.QueueGroup);

            List<Account> accountList = DataGenerationHelper.GetData<List<Account>>(accountJsonString);
            List<MonitorData> monitorDataList = DataGenerationHelper.GetData<List<MonitorData>>(monitorDataJsonString);
            List<QueueGroup> queueGroupList = DataGenerationHelper.GetData<List<QueueGroup>>(queueGroupJsonString);

            foreach (var account in accountList)
            {
                _monitoringDbContext.AddAsync(account);
            }

            foreach (var monitorData in monitorDataList)
            {
                _monitoringDbContext.AddAsync(monitorData);
            }

            foreach (var queueGroup in queueGroupList)
            {
                _monitoringDbContext.AddAsync(queueGroup);
            }

            _monitoringDbContext.SaveChanges();
        } 

        public void RemoveData()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateData()
        {
            throw new System.NotImplementedException();
        }
    }
}
