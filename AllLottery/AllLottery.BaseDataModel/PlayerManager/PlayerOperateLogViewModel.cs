using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;

namespace AllLottery.BaseDataModel.PlayerManager
{
    public class PlayerOperateLogViewModel : BaseServiceNav
    {
        public override string NavName => "玩家操作记录";

        public override string FolderName => "玩家管理";

        [NavField("玩家")]
        public string PlayerName { get; set; }

        [NavField("操作记录", 1000)]
        public string Message { get; set; }

        [NavField("操作时间",200)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<PlayerOperateLog, PlayerOperateLogViewModel>(
                from p in Context.PlayerOperateLogs where p.IsEnable orderby p.CreateTime descending select p,
                (k, t) =>
                {
                    t.PlayerName = k.Player.Name;
                }, (k, w) => w.Where(t => t.Player.Name.Contains(k)), (k, w) => w.Where(t => t.Message.Contains(k)));
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new[] { new TextFieldAttribute("Name", "玩家"), new TextFieldAttribute("Message", "操作记录"), };
        }
    }
}