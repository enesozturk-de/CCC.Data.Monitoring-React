using Microsoft.EntityFrameworkCore;

namespace CCC.Data.Monitoring.Concrete.Interfaces
{
    public interface IDataGenerator
    {
        void AddData();
        void RemoveData();
        void UpdateData();
    }
}
