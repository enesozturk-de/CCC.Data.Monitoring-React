using System;

namespace CCC.Data.Monitoring.Concrete.Constants
{
    public static class Constants
    {
        public const String strPermutation = "ouiveyxaqtd";
        public const Int32 bytePermutation1 = 0x19;
        public const Int32 bytePermutation2 = 0x59;
        public const Int32 bytePermutation3 = 0x17;
        public const Int32 bytePermutation4 = 0x41; 

        public static readonly int MaxValueSLA_Percent = 100;
        public static readonly int MinValueSLA_Percent = 40;

        public static readonly int MaxValueHandledWithinSL = 200;
        public static readonly int MinValueSHandledWithinSL = 150;

        public static readonly int MaxValueOffered = 250;
        public static readonly int MinValueOffered = 180;

        public static readonly int RetryTime = 8;

        public static readonly string Green = "#00FF00";
        public static readonly string Red = "#FF4500";

        public static readonly string Account = "..\\CCC.Data.Monitoring.Data.Access\\DataJson\\Account.json";
        public static readonly string MonitorData = "..\\CCC.Data.Monitoring.Data.Access\\DataJson\\MonitorData.json";
        public static readonly string QueueGroup =  "..\\CCC.Data.Monitoring.Data.Access\\DataJson\\QueueGroup.json";
    }
}
