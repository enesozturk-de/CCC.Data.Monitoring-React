using System;
using System.Collections.Generic;
using System.Text;

namespace CCC.Data.Monitoring.Concrete.Entities
{
    public class ScreenData
    {
        public string QueueGroupName { get; set; }
        public int Offered { get; set; }
        public int Handled { get; set; }
        public string AverageTalkTime { get; set; }
        public string AverageHandlingTime { get; set; }
        public string ServiceLevel { get; set; }
        public string ColumnColour { get; set; }
    }
}
