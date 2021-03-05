using CCC.Data.Monitoring.Concrete.Constants;
using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Data.Access;
using CCC.Data.Monitoring.Data.Access.EFCore;
using CCC.Data.Monitoring.Operations.Extensions;
using CCC.Data.Monitoring.Operations.OperationHelper;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CCC.Data.Monitoring.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller , IUserController
    {
        private readonly IAccountRepository _accountRepository; 
        public UserController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository; 
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginEntity loginEntity)
        {
            Guard.Against.NullOrEmpty(loginEntity.Username, nameof(loginEntity.Username));
            Guard.Against.NullOrWhiteSpace(loginEntity.Username, nameof(loginEntity.Username));
             
            var currentUser = _accountRepository.SingleOrDefault(x => x.Username == loginEntity.Username);
            if (currentUser == null)
            {
                return this.BadRequest(false);
            }
            if (OperationHelper.VerifyPassword(loginEntity.Password, currentUser))
            {

                var startTimeSpan = TimeSpan.Zero;
                var periodTimeSpan = TimeSpan.FromSeconds(Constants.RetryTime);

                var timer = new System.Threading.Timer((e) =>
                {
                    DataGenerator dataGenerator = new DataGenerator();
                    dataGenerator.UpdateTableWithRandomData();
                }, null, startTimeSpan, periodTimeSpan);

                return this.Ok(true);
            }

            return this.BadRequest(false);
        }
    }
}
