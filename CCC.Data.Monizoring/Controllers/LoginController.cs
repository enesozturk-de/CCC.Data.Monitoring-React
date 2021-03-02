using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Operations.Extensions;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MonitoringDbContext _monitoringDbContext;
        private readonly IConfiguration _configuration;
        public LoginController(MonitoringDbContext monitoringDbContext, IConfiguration configuration)
        {
            _monitoringDbContext = monitoringDbContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public bool IsLoginEnable(LoginEntity loginEntity)
        {
            if (!ModelState.IsValid) return false; 

            Guard.Against.NullOrEmpty(loginEntity.Username, nameof(loginEntity.Username));
            Guard.Against.NullOrWhiteSpace(loginEntity.Username, nameof(loginEntity.Username));

            var currentUser = _monitoringDbContext.Account.SingleOrDefault(x => x.Username == loginEntity.Username);
            if (currentUser == null)
            {
                return false;
            }

            byte[] passwordHash;
            OperationHelper.CreatePasswordHash(loginEntity.Password, out passwordHash);

            if (currentUser.PasswordHash == passwordHash.ToString())
            {
                return true;
            }

            return false;
        }
    }
}
