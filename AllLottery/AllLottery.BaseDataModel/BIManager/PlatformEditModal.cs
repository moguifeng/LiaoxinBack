using AllLottery.Model;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.BIManager
{
    public class PlatformEditModal : BaseServiceModal
    {
        public PlatformEditModal()
        {
        }

        public PlatformEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑BI";

        [HiddenTextField]
        public int PlatformId { get; set; }

        [TextField("平台代码", IsReadOnly = true)]
        public string Value { get; set; }

        [TextField("名称", IsRequired = true)]
        public string Name { get; set; }

        [TextField("描述", IsRequired = true)]
        public string Description { get; set; }

        [DropListField("平台类型")]
        public PlatformEnum PlatformType { get; set; }

        [NumberField("排序")]
        public int SortIndex { get; set; }

        [ImageField("游戏图片")]
        public int? AffixId { get; set; }

        [DropListField("是否启动")]
        public bool IsEnable { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public override void Init()
        {
            var platform = (from p in Context.Platforms where p.PlatformId == PlatformId select p).First();
            PlatformType = platform.Type;
        }

        public ServiceResult Save()
        {
            if (AffixId == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "请上传游戏图片");
            }
            var platform = (from p in Context.Platforms where p.PlatformId == PlatformId select p).First();
            platform.Name = Name;
            platform.Description = Description;
            platform.Type = PlatformType;
            platform.SortIndex = SortIndex;
            platform.AffixId = AffixId.Value;
            platform.IsEnable = IsEnable;
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}