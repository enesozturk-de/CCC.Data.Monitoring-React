using CCC.Data.Monitoring.Concrete.Constants;
using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Data.Access.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CCC.Data.Monitoring.Data.Access
{
    public class DataGenerator : IDataGenerator
    {
        private readonly MonitoringDbContext _monitoringDbContext;
        private readonly IConfiguration _configuartion;
        List<QueueGroup> _queueGroupList;
        public DataGenerator(MonitoringDbContext dbContext, IConfiguration configuration)
        {
            _monitoringDbContext = dbContext;
            _configuartion = configuration;
        }
        public void GenerateData()
        {
            var accountJsonString = DataGenerationHelper.GetJsonDataString(Constants.Account);
            var monitorDataJsonString = DataGenerationHelper.GetJsonDataString(Constants.MonitorData);
            var queueGroupJsonString = DataGenerationHelper.GetJsonDataString(Constants.QueueGroup);

            List<Account> accountList = DataGenerationHelper.GetData<List<Account>>(accountJsonString);
            List<MonitorData> monitorDataList = DataGenerationHelper.GetData<List<MonitorData>>(monitorDataJsonString);
            _queueGroupList = DataGenerationHelper.GetData<List<QueueGroup>>(queueGroupJsonString);

            foreach (var account in accountList)
            {
                _monitoringDbContext.Account.AddAsync(account);
            }

            foreach (var monitorData in monitorDataList)
            {
                _monitoringDbContext.MonitorData.AddAsync(monitorData);
            }

            foreach (var queueGroup in _queueGroupList)
            {
                _monitoringDbContext.QueueGroup.AddAsync(queueGroup);
            }

            _monitoringDbContext.SaveChanges();
        } 

        public void RemoveData()
        {
            //ENES: Ne need to fill for this project
        }

        public void UpdateData()
        {
            //ENES: Ne need to fill for this project
        }

        public void UpdateTableWithRandomData()
        {
            foreach (var queueGroup in _queueGroupList)
            {
                Random random = new Random();
                queueGroup.SLA_Percent = random.Next(Constants.Minute, Constants.MaxValue);
            }

            _monitoringDbContext.QueueGroup.UpdateRange(_queueGroupList);
            _monitoringDbContext.SaveChanges();
        }
    }
}
