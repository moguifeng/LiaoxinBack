using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.ContentManager
{
    public class ActivityAnnouncementEditModal : ActivityAnnouncementAddModal
    {
        public ActivityAnnouncementEditModal()
        {
        }

        public ActivityAnnouncementEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "修改";

        public override void Init()
        {
            var exist = (from a in Context.ActivityAnnouncements
                         where a.ActivityAnnouncementId == ActivityAnnouncementId
                         select a).First();
            AffixId = exist.AffixId;
            Content = exist.Content;
            base.Init();
        }

        [HiddenTextField]
        public int ActivityAnnouncementId { get; set; }

        public override ServiceResult Save()
        {
            if (AffixId == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "请上传活动图片");
            }

            var exist = (from a in Context.ActivityAnnouncements
                         where a.ActivityAnnouncementId == ActivityAnnouncementId
                         select a).First();
            exist.AffixId = AffixId.Value;
            exist.Title = Title;
            exist.BeginTime = BeginTime;
            exist.EndTime = EndTime;
            exist.Content = Content;
            exist.SortIndex = SortIndex;

            UserOperateLogService.Log($"编辑公告[{Title}]", Context);
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}