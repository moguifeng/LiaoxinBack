using Liaoxin.IBusiness;
using Liaoxin.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.ContentManager
{
    public class PictureNewsViewModel : BaseServiceNav
    {
        public override string NavName => "滚动图管理";

        public override string FolderName => "内容管理";

        [NavField("主键", IsDisplay = false, IsKey = true)]
        public int PictureNewsId { get; set; }

        [NavField("图片编号")]
        public int AffixId { get; set; }

        [NavField("跳转链接")]
        public string Url { get; set; }

        [NavField("排序")]
        public int SortIndex { get; set; }

        public IUserOperateLogService UserOperateLogService { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<PictureNews, PictureNewsViewModel>(from p in Context.PictureNewses
                                                                    where p.IsEnable
                                                                    orderby p.SortIndex
                                                                    select p);
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new PictureNewsEditModal("Edit", "编辑"), new ConfirmActionButton("Delete", "删除", "是否确定删除？"), };
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new PictureNewsAddModal("Add", "新增"), };
        }

        public void Delete()
        {
            var exist = (from p in Context.PictureNewses where p.PictureNewsId == PictureNewsId select p).First();
            exist.IsEnable = false;
            exist.Update();
            UserOperateLogService.Log($"删除滚动图，主键为[{exist.PictureNewsId}]", Context);
            Context.SaveChanges();

        }
    }
}