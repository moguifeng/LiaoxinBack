using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;

namespace AllLottery.BaseDataModel.PermissionManager
{
    public class MessageViewModel : BaseServiceNav
    {
        public override string NavName => "消息中心";

        public override string FolderName => "后台管理";

        [NavField("消息", 500)]
        public string Description { get; set; }

        [NavField("是否已看")]
        public bool IsLook { get; set; }

        [NavField("发送时间", 200)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            var id = int.Parse(HttpContextAccessor.HttpContext.User.Identity.Name);
            var os = CreateEfDatas<Message, MessageViewModel>(from m in Context.Messages
                                                              where m.InfoType == MessageInfoTypeEnum.User && m.InfoId == id
                                                              orderby m.CreateTime descending
                                                              select m, (k, t) =>
                {
                    k.IsLook = true;
                });
            Context.SaveChanges();
            return os;
        }
    }
}