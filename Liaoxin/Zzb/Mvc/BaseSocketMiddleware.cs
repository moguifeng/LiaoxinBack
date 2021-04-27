using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Zzb.ZzbLog;

namespace Zzb.Mvc
{
    public abstract class BaseSocketMiddleware
    {
        private readonly RequestDelegate _next;

        protected BaseSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public abstract string Path { get; }

        public abstract string CookiesName { get; }

        public virtual string SchemeName => CookiesName;

        public bool IsConnect(int id)
        {
            var tureId = GetType().FullName + id;
            lock (ActionDictionary)
            {
                return ActionDictionary.ContainsKey(tureId) && ActionDictionary[tureId].Count > 0;
            }
        }

        public List<int> ConnectList()
        {
            List<int> list = new List<int>();
            lock (ActionDictionary)
            {
                foreach (var action in ActionDictionary)
                {
                    if (action.Key.Contains(GetType().FullName))
                    {
                        if (int.TryParse(action.Key.Replace(GetType().FullName, ""), out var i))
                        {
                            list.Add(i);
                        }
                    }
                }
            }
            return list;
        }

        public int Count
        {
            get
            {
                lock (ActionDictionary)
                {
                    return (from i in ActionDictionary where i.Key.Contains(GetType().FullName) && i.Value.Any() select i).Count();
                }
            }
        }

        //private HttpContext Context { get; set; }

        //private WebSocket Socket { get; set; }

        //private string ConnectId { get; set; }

        //private string TrueId { get; set; }

        //private IOptionsMonitor<CookieAuthenticationOptions> Opt { get; set; }

        /// <summary>
        /// 1级是类名+ID，二级是连接ID
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, WebSocket>> ActionDictionary = new Dictionary<string, Dictionary<string, WebSocket>>();

        private static Dictionary<string, bool> ConnectDic = new Dictionary<string, bool>();

        public async Task Invoke(HttpContext context)
        {
            //Context = context;
            if (context.WebSockets.IsWebSocketRequest)
            {
                if (context.Request.Path.Value == "/" + Path)
                {

                    //链接ID
                    var connectId = context.Connection.Id;
                    var trueId = "";
                    try
                    {

                        var opt = context.RequestServices
                              .GetRequiredService<IOptionsMonitor<CookieAuthenticationOptions>>();
                        var cookie = opt.CurrentValue.CookieManager.GetRequestCookie(context, CookiesName);

                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        //Socket = webSocket;
                        //ConnectId = connectId;

                        bool isAuthenticated = false;

                        // Decrypt if found
                        if (!string.IsNullOrEmpty(cookie))
                        {
                            var dataProtector = opt.CurrentValue.DataProtectionProvider.CreateProtector(
                                "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                                SchemeName, "v2");

                            var ticketDataFormat = new TicketDataFormat(dataProtector);
                            var ticket = ticketDataFormat.Unprotect(cookie);



                            if (ticket.Principal.Identity.IsAuthenticated)
                            {
                                isAuthenticated = true;
                                var id = int.Parse(ticket.Principal.Identity.Name);

                                //用于保存的ID
                                trueId = GetType().FullName + id;
                                lock (ActionDictionary)
                                {
                                    if (!ActionDictionary.ContainsKey(trueId))
                                    {
                                        ActionDictionary.Add(GetType().FullName + id,
                                            new Dictionary<string, WebSocket>());
                                    }
                                    if (!ActionDictionary[trueId].ContainsKey(connectId))
                                    {
                                        ConnectDic.Add(connectId, true);
                                        ActionDictionary[trueId].Add(connectId, webSocket);
                                    }
                                }

                            }

                        }

                        if (!isAuthenticated)
                        {
                            byte[] bb = System.Text.Encoding.UTF8.GetBytes("0");
                            await webSocket.SendAsync(
                                new ArraySegment<byte>(bb, 0, bb.Length),
                                WebSocketMessageType.Text, true, CancellationToken.None);
                        }

                        //连接
                        var buffer = new byte[1024 * 4];
                        WebSocketReceiveResult result =
                            await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
                                CancellationToken.None);
                        while (!result.CloseStatus.HasValue)
                        {
                            OnMessage(System.Text.Encoding.Default.GetString(buffer).Trim('\0'), webSocket, opt, context);
                            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
                                CancellationToken.None);
                        }
                        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription,
                            CancellationToken.None);

                    }
                    catch (Exception e)
                    {
                        LogHelper.Warning("Socket连接失败", e);
                    }
                    finally
                    {
                        lock (ActionDictionary)
                        {
                            if (ActionDictionary.ContainsKey(trueId) && ActionDictionary[trueId].ContainsKey(connectId))
                            {
                                ActionDictionary[trueId].Remove(connectId);
                                ConnectDic.Remove(connectId);
                            }
                        }
                    }
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }

        protected virtual void OnMessage(string message, WebSocket socket, IOptionsMonitor<CookieAuthenticationOptions> opt, HttpContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {

                    var dataProtector = opt.CurrentValue.DataProtectionProvider.CreateProtector(
                        "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                        SchemeName, "v2");

                    var ticketDataFormat = new TicketDataFormat(dataProtector);
                    var ticket = ticketDataFormat.Unprotect(message);
                    if (ticket.Principal.Identity.IsAuthenticated)
                    {
                        var id = int.Parse(ticket.Principal.Identity.Name);
                        var trueId = GetType().FullName + id;
                        lock (ActionDictionary)
                        {
                            if (!ActionDictionary.ContainsKey(trueId))
                            {
                                ActionDictionary.Add(GetType().FullName + id, new Dictionary<string, WebSocket>());
                            }
                            if (!ActionDictionary[trueId].ContainsKey(context.Connection.Id))
                            {
                                ConnectDic.Add(context.Connection.Id, true);
                                ActionDictionary[trueId].Add(context.Connection.Id, socket);
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {
                LogHelper.Warning("Socket接受信息处理失败", e);
            }
        }

        private static async void SendMessage(string message, WebSocket socket)
        {
            try
            {
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(
                    new ArraySegment<byte>(byteArray, 0, byteArray.Length),
                    WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void SendMessage<T>(string id, string message) where T : BaseSocketMiddleware
        {
            lock (ActionDictionary)
            {
                var tureId = typeof(T).FullName + id;
                if (ActionDictionary.ContainsKey(tureId))
                {
                    foreach (var action in ActionDictionary[tureId])
                    {
                        SendMessage(message, action.Value);
                    }
                }
            }
        }

        public static void SendAllMessage<T>(string message) where T : BaseSocketMiddleware
        {
            lock (ActionDictionary)
            {
                foreach (var dic in ActionDictionary)
                {
                    if (dic.Key.Contains(typeof(T).FullName))
                    {
                        foreach (var action in dic.Value)
                        {
                            SendMessage(message, action.Value);
                        }
                    }
                }
            }
        }
    }
}