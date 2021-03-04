using CCC.Data.Monitoring.Concrete.Interfaces;

namespace CCC.Data.Monitoring.Concrete.Entities
{
    public class QueueGroup : ITable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SLA_Percent { get; set; }
        public int SLA_Time { get; set; }
    }
}
