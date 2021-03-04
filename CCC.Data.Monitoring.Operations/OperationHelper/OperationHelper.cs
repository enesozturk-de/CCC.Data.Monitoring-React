using CCC.Data.Monitoring.Concrete.Constants;
using CCC.Data.Monitoring.Concrete.Entities;
using CCC.Data.Monitoring.Concrete.Interfaces;
using CCC.Data.Monitoring.Operations.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CCC.Data.Monitoring.Operations.OperationHelper
{
    public static class OperationHelper
    { 
        public static string CalculateAverageHandlingTime(MonitorData monitorData)
        {  
            return FormatValue(((monitorData.TalkTime + monitorData.AfterCallWorkTime) / monitorData.Handled));
        }

        public static string CalculateAverageTalkTime(MonitorData monitorData)
        { 
            return FormatValue(monitorData.TalkTime / monitorData.Handled);
        }

        public static string DecideColumnColour(QueueGroup queueGroup, MonitorData monitorData)
        {
            return (monitorData.HandledWithinSL / monitorData.Offered) >= queueGroup.SLA_Percent ? Constants.Green : Constants.Red;
        }

        public static string CalculateServiceLevel(MonitorData monitorData)
        {
            double result = (Convert.ToDouble(monitorData.HandledWithinSL) * Convert.ToDouble(monitorData.Offered)) / 100;
            return Math.Round(result,1).ToString();
        }

        public static string FormatValue(double value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            return $"{timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
        }
         
        public static bool VerifyPassword(string password, Account account)
        { 
            string decryptedPass = Decrypt(account.PasswordHash);

            if (password == decryptedPass)
            {
                return true;
            }
            return false;
        }
         
        public static string Encrypt(string strData)
        { 
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));  
        } 
         
        public static string Decrypt(string strData)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strData))); 

        }
         
        public static byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Constants.strPermutation,
            new byte[] { Constants.bytePermutation1,
                         Constants.bytePermutation2,
                         Constants.bytePermutation3,
                         Constants.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
         
        public static byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Constants.strPermutation,
            new byte[] { Constants.bytePermutation1,
                         Constants.bytePermutation2,
                         Constants.bytePermutation3,
                         Constants.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    } 
}
