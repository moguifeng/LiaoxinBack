using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BackReportController : LiaoxinBaseController
    {


        /// <summary>
        /// 后台报表
        /// </summary>        
        /// <returns></returns>
        [HttpPost("GetAllReport")]
        public ServiceResult GetAllReport(BackReportRequest request)
        {
            return Json(() =>
            {
                BackReportResponse response = new BackReportResponse();
                response.ClientCount = Context.Clients.Where(c => c.IsEnable).Count();
                response.GroupCount = Context.Groups.Where(g => g.IsEnable).Count();

                var query = Context.RedPacketReceives.Where(c => c.IsLuck);
                var withQuery = Context.Withdraws.Where(c => c.IsEnable);
                var reacharQuery = Context.Recharges.Where(c => c.IsEnable);
                if (request.Begin.HasValue)
                {
                    query = query.Where(q => q.CreateTime >= request.Begin.Value.Date);
                    reacharQuery = reacharQuery.Where(q => q.CreateTime >= request.Begin.Value.Date);
                    withQuery = withQuery.Where(q => q.CreateTime >= request.Begin.Value.Date);
                }

                if (request.End.HasValue)
                {
                    query = query.Where(q => q.CreateTime < request.End.Value.Date.AddDays(1));
                    reacharQuery = reacharQuery.Where(q => q.CreateTime < request.End.Value.Date.AddDays(1));
                    withQuery = withQuery.Where(q => q.CreateTime < request.End.Value.Date.AddDays(1));

                }

                response.Wins = query.Sum(w => w.SnatchMoney);
                response.Recharges = reacharQuery.Sum(r => r.Money);
                response.Withdraws = withQuery.Sum(r => r.Money);

                if (!string.IsNullOrEmpty(request.RealName))
                {
                    query = query.Where(q => q.Client.RealName.Contains(request.RealName));
                }

                if (!string.IsNullOrEmpty(request.LiaoxinNumber))
                {
                    query = query.Where(q => q.Client.LiaoxinNumber.Contains(request.LiaoxinNumber));
                }


                List<BackReports> reports = new List<BackReports>();
                var lis = query.Select(c => new { c.Client.LiaoxinNumber, c.Client.RealName, c.SnatchMoney }).ToList();
                var lisGroup = lis.GroupBy(c => new { c.RealName, c.LiaoxinNumber });
                lisGroup.ToList().ForEach(ls =>
                {
                    BackReports entity = new BackReports();

                    entity.Wins = ls.ToList().Sum(c => c.SnatchMoney);
                    entity.LiaoxinNumber = ls.Key.LiaoxinNumber;
                    entity.RealName = ls.Key.RealName;

                    reports.Add(entity);

                });
                response.BackReports = reports;
   

                return ObjectGenericityResult(response);
            }, "获取失败");

        }

    }
}