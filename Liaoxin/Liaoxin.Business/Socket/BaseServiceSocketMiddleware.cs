using Liaoxin.Model;
using Microsoft.AspNetCore.Http;
using Zzb.Mvc;

namespace Liaoxin.Business.Socket
{
    public abstract class BaseServiceSocketMiddleware : BaseSocketMiddleware
    {
        protected BaseServiceSocketMiddleware(RequestDelegate next) : base(next)
        {
        }

        public static void SendMessage<T>(string id, string message, MessageTypeEnum type, decimal money = 0) where T : BaseServiceSocketMiddleware
        {
            SendMessage<T>(id, Newtonsoft.Json.JsonConvert.SerializeObject(new { Message = message, Type = type, Money = money }));
        }

        public static void SendAllMessage<T>(string message, MessageTypeEnum type, decimal money = 0) where T : BaseServiceSocketMiddleware
        {
            SendAllMessage<T>(Newtonsoft.Json.JsonConvert.SerializeObject(new { Message = message, Type = type, Money = money }));
        }
    }
}