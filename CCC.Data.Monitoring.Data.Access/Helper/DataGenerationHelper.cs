using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CCC.Data.Monitoring.Data.Access.Helper
{
    public static class DataGenerationHelper
    { 
        public static T GetData<T>(string jsonString, T defaultValue = default(T))
        {
            T result = JsonConvert.DeserializeObject<T>(jsonString);
            return result;
        }
        public static string GetJsonDataString(string jsonFileName)
        {  
            return File.ReadAllText(jsonFileName);
        }
    }
}
