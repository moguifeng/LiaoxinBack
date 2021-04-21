using System.Collections.Generic;

namespace AllLottery.Business.Report.Team
{
    public abstract class BaseTeamReport : BaseReport
    {
        protected BaseTeamReport(int playerId) : base(playerId)
        {
        }

        protected override List<int> GetTeamPlayerIds(int id)
        {
            var list = base.GetTeamPlayerIds(id);
            list.Add(id);
            return list;
        }
    }
}