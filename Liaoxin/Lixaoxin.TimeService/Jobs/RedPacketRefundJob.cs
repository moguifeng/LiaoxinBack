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
            DateTime yesterdayTime = DateTime.Now.AddDays(-1);
            DataTable dt = MySqlHelper.ExecuteDataTable($"SELECT RedPacketId,ClientId,Over FROM redpackets WHERE STATUS=0 AND Over>0 AND IsEnable=TRUE AND SendTime<='{yesterdayTime.ToString("yyyy-MM-dd HH:mm:ss")}'");

  
        }
    }
}
