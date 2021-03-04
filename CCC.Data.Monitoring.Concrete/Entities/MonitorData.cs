using CCC.Data.Monitoring.Concrete.Interfaces;

namespace CCC.Data.Monitoring.Concrete.Entities
{
    public class MonitorData : ITable
    {
        public int ID { get; set; }
        public double TalkTime { get; set; }
        public double AfterCallWorkTime { get; set; }
        public int Handled { get; set; }
        public int Offered { get; set; }
        public int HandledWithinSL { get; set; }
        public int QueueGroupID { get; set; }

    }
}
