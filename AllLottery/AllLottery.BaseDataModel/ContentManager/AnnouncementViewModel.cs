using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ContentManager
{
    public class AnnouncementViewModel : BaseServiceNav
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string NavName => "站点公告";

        public override string FolderName => "内容管理";

        [NavField("主键", IsDisplay = false, IsKey = true)]
        public int AnnouncementId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [NavField("公告标题", 300)]
        public string Title { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [NavField("发布时间", 150)]
        public DateTime ShowOpenTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<Announcement, AnnouncementViewModel>(from a in Context.Announcements
                                                                      where a.IsEnable
                                                                      select a);
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new AnnouncementAddModal("Add", "新增公告"), };
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new ConfirmActionButton("Delete", "删除", "是否确定删除"), new AnnouncementEditModal("Edit", "编辑"), };
        }

        public void Delete()
        {
            var exist = (from a in Context.Announcements where a.AnnouncementId == AnnouncementId select a).First();
            exist.IsEnable = false;
            UserOperateLogService.Log($"删除[{Title}]公告", Context);
            Context.SaveChanges();
        }
    }
}