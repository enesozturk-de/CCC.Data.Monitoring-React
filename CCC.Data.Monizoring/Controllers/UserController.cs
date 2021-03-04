using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Data.Access;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Data.Access.Helper;
using CCC.Data.Monitoring.Operations.Extensions;
using CCC.Data.Monitoring.Operations.OperationHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC.Data.Monitoring.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly MonitoringDbContext _monitoringDbContext;
        private readonly IConfiguration _configuration;
        private bool updateControlFlag;
        public UserController(MonitoringDbContext monitoringDbContext, IConfiguration configuration)
        {
            _monitoringDbContext = monitoringDbContext;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginEntity loginEntity)
        {
            Guard.Against.NullOrEmpty(loginEntity.Username, nameof(loginEntity.Username));
            Guard.Against.NullOrWhiteSpace(loginEntity.Username, nameof(loginEntity.Username));

            var currentUser = _monitoringDbContext.Account.SingleOrDefault(x => x.Username == loginEntity.Username);
            if (currentUser == null)
            {
                return this.BadRequest(false);
            }
            if (OperationHelper.VerifyPassword(loginEntity.Password, currentUser))
            {

                var startTimeSpan = TimeSpan.Zero;
                var periodTimeSpan = TimeSpan.FromSeconds(8);

                var timer = new System.Threading.Timer((e) =>
                {
                    updateControlFlag = true;
                    DataGenerator dataGenerator = new DataGenerator(_monitoringDbContext, _configuration);
                    dataGenerator.UpdateTableWithRandomData();
                }, null, startTimeSpan, periodTimeSpan);

                return this.Ok(true);
            }

            return this.BadRequest(false);
        }
    }
}
