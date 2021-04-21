using AllLottery.IBusiness;
using AllLottery.Model;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ContentManager
{
    public class PictureNewsAddModal : BaseServiceModal
    {
        public PictureNewsAddModal()
        {
        }

        public PictureNewsAddModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "新增";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [ImageField("滚动图")]
        public int? AffixId { get; set; }

        [TextField("跳转链接")]
        public string Url { get; set; }

        [NumberField("排序")]
        public int SortIndex { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public virtual ServiceResult Save()
        {
            if (AffixId == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "请上传滚动图");
            }
            Context.PictureNewses.Add(new PictureNews(AffixId.Value, Url, SortIndex));
            UserOperateLogService.Log($"新增滚动图，图片编号是[{AffixId}]", Context);
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}