using Liaoxin.Business.Socket;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zzb;
using Zzb.EF;
using Zzb.ZzbLog;

namespace Liaoxin.Business
{
    //public class MessageService : BaseService, IMessageService
    //{
    //    public void AddAllUserMessage(string message, MessageTypeEnum type)
    //    {
    //        foreach (UserInfo userInfo in from u in Context.UserInfos where u.IsEnable select u)
    //        {
    //            Context.Messages.Add(new Message(userInfo.UserInfoId, MessageInfoTypeEnum.User, type, message));
    //        }
    //        Context.SaveChanges();
    //    }

    //    public Message[] GetPlayerMessages(int playerId, int index, int size, out int total)
    //    {
    //        var sql = from m in Context.Messages
    //                  where m.IsEnable && m.InfoType == MessageInfoTypeEnum.Player && m.InfoId == playerId
    //                  select m;
    //        total = sql.Count();
    //        var messages = sql.OrderByDescending(t => t.CreateTime).Skip((index - 1) * size).Take(size).ToArray();
    //        foreach (Message message in messages)
    //        {
    //            message.IsLook = true;
    //        }
    //        Context.SaveChanges();
    //        return messages;
    //    }

    //    public Message[] GetUserMessage(int userId, int index, int size, out int total)
    //    {
    //        var sql = from m in Context.Messages
    //                  where m.IsEnable && m.InfoType == MessageInfoTypeEnum.User && m.InfoId == userId && !m.IsLook
    //                  select m;
    //        total = sql.Count();
    //        var messages = sql.OrderByDescending(t => t.CreateTime).Skip((index - 1) * size).Take(size).ToArray();
    //        return messages;
    //    }

    //    public void ReadMessage(int id)
    //    {
    //        var exist = (from m in Context.Messages where m.MessageId == id select m).FirstOrDefault();
    //        if (exist == null)
    //        {
    //            throw new ZzbException("未找到该消息");
    //        }

    //        exist.IsLook = true;
    //        exist.Update();
    //        Context.SaveChanges();
    //    }

    //    public void ClearUserMessage(int userId)
    //    {
    //        var sql = from m in Context.Messages
    //                  where m.IsEnable && m.InfoType == MessageInfoTypeEnum.User && m.InfoId == userId && !m.IsLook
    //                  select m;
    //        if (sql.Any())
    //        {
    //            foreach (Message message in sql)
    //            {
    //                message.IsLook = true;
    //            }
    //            Context.SaveChanges();
    //        }

    //    }

    //    public static void Start()
    //    {
    //        new Task(() =>
    //        {
    //            while (true)
    //            {
    //                try
    //                {
    //                    using (var context = LotteryContext.CreateContext())
    //                    {
    //                        var sql = from m in context.Messages where m.IsEnable && !m.IsSend select m;
    //                        if (sql.Any())
    //                        {
    //                            foreach (Message message in sql)
    //                            {
    //                                if (message.InfoType == MessageInfoTypeEnum.Player)
    //                                {
    //                                    BaseServiceSocketMiddleware.SendMessage<PlayerSocketMiddleware>(
    //                                        message.InfoId.ToString(), message.Description, message.Type, message.Money);
    //                                }
    //                                if (message.InfoType == MessageInfoTypeEnum.User)
    //                                {
    //                                    BaseServiceSocketMiddleware.SendMessage<UserSocketMiddleware>(message.InfoId.ToString(),
    //                                        message.Description, message.Type);
    //                                }
    //                                message.IsSend = true;
    //                            }
    //                            context.SaveChanges();
    //                        }
    //                    }
    //                }
    //                catch (Exception e)
    //                {
    //                    LogHelper.Error("发送socket信息失败", e);
    //                }
    //                finally
    //                {
    //                    Thread.Sleep(1000);
    //                }
    //            }
    //        }).Start();
    //    }
    //}
}