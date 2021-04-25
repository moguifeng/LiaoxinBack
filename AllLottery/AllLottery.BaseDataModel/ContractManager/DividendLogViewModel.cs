using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;

namespace AllLottery.BaseDataModel.ContractManager
{
    //public class DividendLogViewModel : BaseServiceNav
    //{
    //    public override string NavName => "分红发放记录";

    //    public override string FolderName => "工资分红管理";

    //    [NavField("主键", IsKey = true, IsDisplay = false)]
    //    public int DividendLogId { get; set; }

    //    /// <summary>
    //    /// 日期
    //    /// </summary>
    //    [NavField("开始日期", 150)]
    //    public DateTime CalBeginDate { get; set; }

    //    [NavField("结束日期", 150)]
    //    public DateTime EndTime { get; set; }

    //    [NavField("发放人")]
    //    public string ParentName { get; set; }

    //    [NavField("收款人")]
    //    public string PlayerName { get; set; }

    //    [NavField("分红金额")]
    //    public decimal DividendMoney { get; set; }

    //    [NavField("分红比例")]
    //    public string RateString { get; set; }

    //    [NavField("投注总额")]
    //    public decimal BetMoney { get; set; }

    //    [NavField("亏损金额")]
    //    public decimal LostMoney { get; set; }


    //    /// <summary>
    //    /// 总投注人数
    //    /// </summary>
    //    [NavField("总投注人数")]
    //    public int BetMen { get; set; }

    //    protected override object[] DoGetNavDatas()
    //    {
    //        return CreateEfDatas<DividendLog, DividendLogViewModel>(
    //            from d in Context.DividendLogs where d.IsEnable orderby d.CreateTime descending select d,
    //            (k, t) =>
    //            {
    //                t.EndTime = k.DividendDate.SettleTime;
    //                t.PlayerName = k.Player.Name;
    //                t.ParentName = k.Player.ParentPlayer?.Name;
    //                t.RateString = (k.Rate * 100).ToString("0.#") + "%";
    //            });
    //    }
    //}
}