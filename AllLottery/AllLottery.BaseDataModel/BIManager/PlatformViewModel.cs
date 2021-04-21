using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.BIManager
{
    public class PlatformViewModel : BaseServiceNav
    {
        public override string NavName => "BI接口管理";

        public override string FolderName => "BI管理";

        [NavField("主键", IsDisplay = false, IsKey = true)]
        public int PlatformId { get; set; }

        [NavField("名称")]
        public string Name { get; set; }

        [NavField("平台代码")]
        public string Value { get; set; }

        [NavField("描述",200)]
        public string Description { get; set; }

        [NavField("类型")]
        public PlatformEnum Type { get; set; }

        [NavField("排序")]
        public int SortIndex { get; set; }

        [NavField("图片", IsDisplay = false)]
        public int AffixId { get; set; }

        [NavField("是否启动")]
        public bool IsEnable { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<Platform, PlatformViewModel>(from p in Context.Platforms orderby p.SortIndex select p);
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new[] { new PlatformEditModal("Edit", "编辑"), };
        }
    }
}