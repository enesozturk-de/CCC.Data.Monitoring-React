using CCC.Data.Monitoring.Concrete.Interfaces;

namespace CCC.Data.Monitoring.Concrete.Entities
{
    public class MonitorData : ITable
    {
        public int ID { get; set; }
        public double TalkTime { get; set; }
        public double AfterCallWorkTime { get; set; }
        public double Handled { get; set; }
        public double Offered { get; set; }
        public double HandledWithinSL { get; set; }
        public double QueueGroupID { get; set; }

    }
}
