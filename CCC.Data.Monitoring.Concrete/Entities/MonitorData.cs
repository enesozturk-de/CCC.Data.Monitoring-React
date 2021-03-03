using CCC.Data.Monitoring.Concrete.Interfaces;

namespace CCC.Data.Monitoring.Concrete.Entities
{
    public class MonitorData : ITable
    {
        public int ID { get; set; }
        public int TalkTime { get; set; }
        public int AfterCallWorkTime { get; set; }
        public int Handled { get; set; }
        public int Offered { get; set; }
        public int HandledWithinSL { get; set; }
        public int QueueGroupID { get; set; }

    }
}
