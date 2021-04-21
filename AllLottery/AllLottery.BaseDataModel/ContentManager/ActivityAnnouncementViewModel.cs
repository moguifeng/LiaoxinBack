using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ContentManager
{
    public class ActivityAnnouncementViewModel : BaseServiceNav
    {
        public override string NavName => "活动公告配置";

        public override string FolderName => "内容管理";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [NavField("Id", IsKey = true, IsDisplay = false)]
        public int ActivityAnnouncementId { get; set; }

        [NavField("标题")]
        public string Title { get; set; }

        [NavField("开始时间", 150)]
        public DateTime BeginTime { get; set; }

        [NavField("结束时间", 150)]
        public DateTime EndTime { get; set; }

        [NavField("排序")]
        public int SortIndex { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<ActivityAnnouncement, ActivityAnnouncementViewModel>(
                from a in Context.ActivityAnnouncements where a.IsEnable orderby a.SortIndex select a);
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new BaseButton[] { new ActivityAnnouncementAddModal("Add", "新增") };
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new ConfirmActionButton("Delete", "删除", "是否确定删除"), new ActivityAnnouncementEditModal("Edit", "编辑"), };
        }

        public void Delete()
        {
            var exist = (from a in Context.ActivityAnnouncements
                         where a.ActivityAnnouncementId == ActivityAnnouncementId
                         select a).First();
            exist.IsEnable = false;
            exist.Update();
            UserOperateLogService.Log($"删除活动公告[{Title}]", Context);
            Context.SaveChanges();
        }
    }
}