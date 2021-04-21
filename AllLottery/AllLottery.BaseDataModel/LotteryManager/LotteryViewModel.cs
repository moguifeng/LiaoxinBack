using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.LotteryManager
{
    public class LotteryViewModel : BaseServiceNav
    {
        public override string NavName => "彩种管理";

        public override string FolderName => "彩种管理";

        [NavField("主键", IsDisplay = false, IsKey = true)]
        public int LotteryTypeId { get; set; }

        [NavField("彩种名称")]
        public string Name { get; set; }

        [NavField("123", IsDisplay = false)]
        public decimal WinRate { get; set; }

        [NavField("盈利比例")]
        public string WinRateString { get; set; }

        [NavField("是否维护")]
        public bool IsStop { get; set; }

        [NavField("是否热门")]
        public bool IsHot { get; set; }

        [NavField("排序")]
        public int SortIndex { get; set; }


        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<LotteryType, LotteryViewModel>(from l in Context.LotteryTypes orderby l.SortIndex select l,
                (k, t) =>
                {
                    t.WinRateString = k.CalType == LotteryCalNumberTypeEnum.Automatic ? k.WinRate.ToPercenString() : "-";

                });
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new[] { new LotteryEditViewModel("Edit", "编辑彩种"), };
        }

        public override bool ShowOperaColumn => true;
    }
}