using CCC.Data.Monitoring.Concrete.Interfaces;

namespace CCC.Data.Monitoring.Concrete.Entities
{
    public class QueueGroup : ITable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SLAPercent { get; set; }
        public int SLATime { get; set; }
    }
}
