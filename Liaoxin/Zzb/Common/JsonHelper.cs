using Newtonsoft.Json;

namespace Zzb.Common
{
    public static class JsonHelper
    {
        public static T Json<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject(json, typeof(T)) as T;
        }

        public static string ToJson(object json)
        {
            return JsonConvert.SerializeObject(json);
        }
    }
}