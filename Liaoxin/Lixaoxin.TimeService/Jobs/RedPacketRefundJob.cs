using Lixaoxin.TimeService.Utility;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Lixaoxin.TimeService.Jobs
{
    /// <summary>
    /// 红包退款:特性已控制不并发执行
    /// </summary>
    [DisallowConcurrentExecution]
    public class RedPacketRefundJob : IJob
    {
  

        public async Task Execute(IJobExecutionContext context)
        {
            StringBuilder sbsql = new StringBuilder();
            DateTime yesterdayTime = DateTime.Now.AddDays(-1);
            DataTable dt = MySqlHelper.ExecuteDataTable($"SELECT RedPacketId,ClientId,Money,Over FROM redpackets WHERE STATUS=0 AND Over>0 AND IsEnable=TRUE AND SendTime<='{yesterdayTime.ToString("yyyy-MM-dd HH:mm:ss")}'");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    decimal overMoney = 0,money=0;
                    decimal.TryParse(dr["Over"]+"", out overMoney);
                    // decimal.TryParse(dr["Money"] + "", out money);
                   
                    int refundStatus = 3;
                    //2:已退款;3:未领完退款
                    //refundStatus = overMoney == money ? 2 : 3;

                    string clientId = dr["ClientId"]+"";
                    string redPacketId= dr["RedPacketId"] + "";
                    sbsql.AppendFormat("UPDATE Clients SET Coin=Coin+{0},UpdateTime=NOW() WHERE ClientId='{1}' ; ",overMoney,clientId);//发红包账户汇款
                    sbsql.AppendFormat("UPDATE redpackets SET STATUS={0},UpdateTime=NOW() WHERE RedPacketId='{1}' ; ", refundStatus, redPacketId);//让红包失效
                    sbsql.AppendFormat(@"INSERT INTO coinlogs (CoinLogId, CreateTime, UpdateTime, IsEnable, ClientId, FlowCoin, Coin, TYPE, AboutId, Remark)VALUES(UUID(), NOW(), NOW(), TRUE, '{0}', {1},( SELECT IFNULL(MAX(Coin),0) FROM clients WHERE ClientId='{0}'), 9, '{2}', '红包退款');", clientId, overMoney, redPacketId);//流水
                }

            }
            DataTable dtPerson = MySqlHelper.ExecuteDataTable($"SELECT RedPacketPersonalId,FromClientId,Money FROM redpacketpersonals WHERE IsReceive=FALSE AND IsEnable=TRUE AND SendTime<='{yesterdayTime.ToString("yyyy-MM-dd HH:mm:ss")}'");
            if (dtPerson != null && dtPerson.Rows.Count > 0)
            {
   
                foreach (DataRow dr in dtPerson.Rows)
                {
                    decimal money = 0;
                    decimal.TryParse(dr["Money"] + "", out money);
        
                    int refundStatus = 3;
                    //2:已退款;3:未领完退款
                    //refundStatus = overMoney == money ? 2 : 3;

                    string clientId = dr["FromClientId"] + "";
                    string redPacketId = dr["RedPacketPersonalId"] + "";
                    sbsql.AppendFormat("UPDATE Clients SET Coin=Coin+{0},UpdateTime=NOW() WHERE ClientId='{1}' ; ", money, clientId); //发红包账户汇款
                    sbsql.AppendFormat("UPDATE redpacketpersonals SET IsEnable=False,UpdateTime=NOW() WHERE RedPacketPersonalId='{0}' ; ", redPacketId);//让红包失效
                    sbsql.AppendFormat(@"INSERT INTO coinlogs (CoinLogId, CreateTime, UpdateTime, IsEnable, ClientId, FlowCoin, Coin, TYPE, AboutId, Remark)VALUES(UUID(), NOW(), NOW(), TRUE, '{0}', {1},( SELECT IFNULL(MAX(Coin),0) FROM clients WHERE ClientId='{0}'), 11, '{2}', '个人红包转账退款');  ", clientId, money, redPacketId);//流水
                }

            }

            if (sbsql.Length > 0)
            {
               int exeRow= MySqlHelper.ExecuteTransaction(new List<string>() { sbsql.ToString() });
            }

        }
    }
}
