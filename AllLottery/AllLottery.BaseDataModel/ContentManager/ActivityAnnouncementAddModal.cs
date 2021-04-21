using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ContentManager
{
    public class ActivityAnnouncementAddModal : BaseServiceModal
    {
        public ActivityAnnouncementAddModal()
        {
        }

        public ActivityAnnouncementAddModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "新增";

        [TextField("标题", IsRequired = true)]
        public string Title { get; set; }

        [DateTimeField("开始时间")]
        public DateTime BeginTime { get; set; } = DateTime.Now;

        [DateTimeField("结束时间")]
        public DateTime EndTime { get; set; } = DateTime.Now.AddDays(30);

        [ImageField("活动图片")]
        public int? AffixId { get; set; }

        [EditorField("活动内容")]
        public string Content { get; set; }

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
                return new ServiceResult(ServiceResultCode.Error, "请上传活动图片");
            }

            Context.ActivityAnnouncements.Add(new ActivityAnnouncement(Title, BeginTime, EndTime, Content,
                AffixId.Value)
            { SortIndex = SortIndex });

            UserOperateLogService.Log($"新增活动公告[{Title}]", Context);

            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}