using CCC.Data.Monitoring.Concrete.Interfaces;

namespace CCC.Data.Monitoring.Concrete.Entities
{
    public class Account : ITable
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; } 
        public string Username { get; set; }
    }
}
