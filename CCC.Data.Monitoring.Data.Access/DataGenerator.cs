using CCC.Data.Monitoring.Concrete.Constants;
using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Data.Access.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCC.Data.Monitoring.Data.Access
{
    public class DataGenerator : IDataGenerator, IDisposable
    {
        private readonly IMonitorDataRepository _monitorDataRepository;
        private readonly IQueueGroupRepository _queueGroupRepository;
        private readonly IAccountRepository _accountRepository;
        public DataGenerator(IAccountRepository accountRepository, IMonitorDataRepository monitorDataRepository, IQueueGroupRepository queueGroupRepository)
        {
            _monitorDataRepository = monitorDataRepository;
            _queueGroupRepository = queueGroupRepository;
            _accountRepository = accountRepository;
        }
        public DataGenerator()
        {
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
                _accountRepository.Add(account);
            }

            foreach (var monitorData in monitorDataList)
            {
                _monitorDataRepository.Add(monitorData);
            }

            foreach (var queueGroup in queueGroupList)
            {
                _queueGroupRepository.Add(queueGroup);
            }
        }

        public void UpdateTableWithRandomData()
        {
            var opsions = new DbContextOptionsBuilder<MonitoringDbContext>().UseInMemoryDatabase(databaseName: "CCCMonitoring").Options;
            MonitoringDbContext monitoringDbContext = new MonitoringDbContext(opsions);

            var queueGroups = monitoringDbContext.QueueGroup.ToList();

            foreach (var queueGroup in queueGroups)
            {
                Random random = new Random();
                queueGroup.SLA_Percent = random.Next(Constants.MinValueSLA_Percent, Constants.MaxValueSLA_Percent);
            }

            monitoringDbContext.QueueGroup.UpdateRange(queueGroups);

            var monitorDatas = monitoringDbContext.MonitorData.ToList();

            foreach (var monitorData in monitorDatas)
            {
                Random random = new Random();
                monitorData.HandledWithinSL = random.Next(Constants.MinValueSHandledWithinSL, Constants.MaxValueHandledWithinSL);
                monitorData.Offered = random.Next(Constants.MinValueOffered, Constants.MaxValueOffered);
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

        public void Dispose()
        { 
            GC.SuppressFinalize(this);
        }
    }
}
