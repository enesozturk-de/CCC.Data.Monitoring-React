using Newtonsoft.Json;
using System.IO;

namespace CCC.Data.Monitoring.Data.Access.Helper
{
    public static class DataGenerationHelper
    { 
        public static T GetData<T>(string jsonString, T defaultValue = default(T))
        {
            if (jsonString == string.Empty || jsonString == null || jsonString == "")
            {
                return defaultValue;
            }

            T result = JsonConvert.DeserializeObject<T>(jsonString);
            return result;
        }
        public static string GetJsonDataString(string jsonFileName)
        {  
            return File.ReadAllText(jsonFileName);
        }
    }
}
