using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ContentManager
{
    public class AnnouncementAddModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public AnnouncementAddModal()
        {
        }

        public AnnouncementAddModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "新增公告";

        /// <summary>
        /// 标题
        /// </summary>
        [TextField("标题")]
        public string Title { get; set; }

        [EditorField("内容")]
        public string Content { get; set; }

        [DateTimeField("发布时间")]
        public DateTime ShowOpenTime { get; set; } = DateTime.Now;

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            Context.Announcements.Add(new Announcement(Title, Content, ShowOpenTime));
            UserOperateLogService.Log($"新增[{Title}]公告", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}