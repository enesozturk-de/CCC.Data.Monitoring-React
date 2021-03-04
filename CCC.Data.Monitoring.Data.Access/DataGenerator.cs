using CCC.Data.Monitoring.Concrete.Constants;
using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Data.Access.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCC.Data.Monitoring.Data.Access
{
    public class DataGenerator : IDataGenerator
    {
        private readonly MonitoringDbContext _monitoringDbContext;
        private readonly IConfiguration _configuartion; 
        public DataGenerator(MonitoringDbContext dbContext, IConfiguration configuration )
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
            List<QueueGroup> queueGroupList = DataGenerationHelper.GetData<List<QueueGroup>>(queueGroupJsonString);

            foreach (var account in accountList)
            {
                _monitoringDbContext.Account.AddAsync(account);
            }

            foreach (var monitorData in monitorDataList)
            {
                _monitoringDbContext.MonitorData.AddAsync(monitorData);
            }

            foreach (var queueGroup in queueGroupList)
            {
                _monitoringDbContext.QueueGroup.AddAsync(queueGroup);
            }

            _monitoringDbContext.SaveChanges();
        }  

        public void UpdateTableWithRandomData()
        {
            var opsions = new DbContextOptionsBuilder<MonitoringDbContext>().UseInMemoryDatabase(databaseName: "CCCMonitoring").Options;
            MonitoringDbContext monitoringDbContext = new MonitoringDbContext(opsions);

            var queueGroups = monitoringDbContext.QueueGroup.ToList();

            foreach (var queueGroup in queueGroups)
            {
                Random random = new Random();
                queueGroup.SLA_Percent = random.Next(Constants.MinValue, Constants.MaxValue);
            }

            monitoringDbContext.QueueGroup.UpdateRange(queueGroups);

            var monitorDatas = monitoringDbContext.MonitorData.ToList();

            foreach (var monitorData in monitorDatas)
            {
                Random random = new Random();
                monitorData.HandledWithinSL = random.Next(150,200);
                monitorData.Offered = random.Next(180,250);
            }
            monitoringDbContext.MonitorData.UpdateRange(monitorDatas);  
            monitoringDbContext.SaveChanges();
        }
        public void RemoveData()
        {
            //ENES: Ne need to fill for this project
        }

        public void UpdateData()
        {
            //ENES: Ne need to fill for this project
        }
    }
}
