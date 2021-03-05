using CCC.Data.Monitoring.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CCC.Data.Monitoring.Concrete.Interfaces
{
    public interface IUserController
    {
        IActionResult Login(LoginEntity loginEntity);
    }
}
