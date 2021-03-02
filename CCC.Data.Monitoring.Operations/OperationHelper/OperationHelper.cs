using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Operations.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCC.Data.Monitoring.Operations.OperationHelper
{
    public static class OperationHelper
    {
        private static int serviceLevel;
        public static string CalculateAverageHandlingTime(MonitorData monitorData)
        {
            return ((monitorData.TalkTime + monitorData.AfterCallWorkTime) / monitorData.Handled).ToString();
        }

        public static string CalculateAverageTalkTime(MonitorData monitorData)
        {
            return (monitorData.TalkTime / monitorData.Handled).ToString();
        }

        public static string DecideColumnColour(QueueGroup queueGroup)
        {
            return serviceLevel >= queueGroup.SLAPercent ? "green" : "red";
        }

        public static string CalculateServiceLevel(MonitorData monitorData)
        {
            serviceLevel = (monitorData.HandledWithinSL / monitorData.Offered);
            return serviceLevel.ToString();
        } 
        public static void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            Guard.Against.IsNullOrEmpty(password);
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            { 
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            Guard.Against.IsNullOrEmpty(password);
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
