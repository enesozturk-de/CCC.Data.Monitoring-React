using Microsoft.EntityFrameworkCore;

namespace CCC.Data.Monitoring.Concrete.Interfaces
{
    public interface IDataGenerator
    {
        void GenerateData();
        void RemoveData();
        void UpdateData();
    }
}
