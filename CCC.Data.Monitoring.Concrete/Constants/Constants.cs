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

        public static readonly int Hour = 60;
        public static readonly int Minute = 60;
        public static readonly int Second = 60;

        public static readonly int MaxValue = 100;
        public static readonly int MinValue = 0;

        public static readonly int RetryTime = 8;

        public static readonly string Green = "#00FF00";
        public static readonly string Red = "#FF4500";

        public static readonly string Account = "..\\CCC.Data.Monitoring.Data.Access\\DataJson\\Account.json";
        public static readonly string MonitorData = "..\\CCC.Data.Monitoring.Data.Access\\DataJson\\MonitorData.json";
        public static readonly string QueueGroup =  "..\\CCC.Data.Monitoring.Data.Access\\DataJson\\QueueGroup.json";
    }
}
