using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.ContentManager
{
    public class PictureNewsEditModal : PictureNewsAddModal
    {
        public PictureNewsEditModal()
        {
        }

        public PictureNewsEditModal(string id, string name) : base(id, name)
        {
        }

        [HiddenTextField]
        public int PictureNewsId { get; set; }

        public override ServiceResult Save()
        {
            if (AffixId == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "请上传滚动图");
            }
            var exist =
                (from p in Context.PictureNewses where p.PictureNewsId == PictureNewsId select p).First();
            exist.SortIndex = SortIndex;
            exist.AffixId = AffixId.Value;
            exist.Url = Url;
            exist.Update();
            UserOperateLogService.Log($"编辑滚动图，主键为[{PictureNewsId}]", Context);
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}