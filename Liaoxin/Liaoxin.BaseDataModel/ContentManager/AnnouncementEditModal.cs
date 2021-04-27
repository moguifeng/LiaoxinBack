using System;
using System.Linq;
using Liaoxin.IBusiness;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.ContentManager
{
    public class AnnouncementEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public AnnouncementEditModal()
        {
        }

        public AnnouncementEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑公告";

        [HiddenTextField]
        public int AnnouncementId { get; set; }

        [TextField("标题")]
        public string Title { get; set; }

        [EditorField("内容")]
        public string Content { get; set; }

        [DateTimeField("发布时间")]
        public DateTime ShowOpenTime { get; set; }

        public override void Init()
        {
            var exist = (from a in Context.Announcements where a.AnnouncementId == AnnouncementId select a).First();
            Content = exist.Content;
        }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public void Save()
        {
            var exist = (from a in Context.Announcements where a.AnnouncementId == AnnouncementId select a).First();
            exist.Content = Content;
            exist.Title = Title;
            exist.ShowOpenTime = ShowOpenTime;
            UserOperateLogService.Log($"编辑[{Title}]公告",Context);
            Context.SaveChanges();
        }
    }
}