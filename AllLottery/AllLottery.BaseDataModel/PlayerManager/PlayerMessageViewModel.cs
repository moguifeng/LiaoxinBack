using System;
using System.Linq;
using AllLottery.Model;
using Remotion.Linq.Clauses;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.PlayerManager
{
    public class PlayerMessageViewModel : BaseServiceNav
    {
        public override string NavName => "消息通知";

        public override string FolderName => "玩家管理";

        [NavField("玩家", 100)]
        public string Name { get; set; }

        [NavField("消息内容", 700)]
        public string Description { get; set; }

        [NavField("发送时间", 200)]
        public DateTime CreateTime { get; set; }


        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<Message, PlayerMessageViewModel>(from m in Context.Messages
                                                                  where m.InfoType == MessageInfoTypeEnum.Player
                                                                  orderby m.CreateTime descending
                                                                  select m, (k, t) =>
                                                                  {
                                                                      var player = (from p in Context.Players
                                                                                    where p.PlayerId == k.InfoId
                                                                                    select p).First();
                                                                      t.Name = player.Name;
                                                                  });
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new PlayerMessageAddViewModel("Add", "新建通知") { Icon = "plus" } };
        }
    }
}