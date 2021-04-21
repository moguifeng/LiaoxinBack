using System.Linq;
using AllLottery.IBusiness;
using AllLottery.Model;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.PlayerManager
{
    public class PlayerMessageAddViewModel : BaseServiceModal
    {
        public PlayerMessageAddViewModel()
        {
        }

        public PlayerMessageAddViewModel(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "新增通知";

        [TextField("玩家", IsRequired = true)]
        public string Name { get; set; }

        [TextField("消息内容", IsRequired = true)]
        public string Description { get; set; }

        public override BaseButton[] Buttons()
        {
            return new BaseButton[] { new ActionButton("Save", "保存") };
        }

        public ServiceResult Save()
        {
            var player = (from p in Context.Players where p.Name == Name select p).FirstOrDefault();
            if (player == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "找不到该玩家");
            }

            Context.Messages.Add(new Message(player.PlayerId, MessageInfoTypeEnum.Player, MessageTypeEnum.Message, Description) { Type = MessageTypeEnum.BackMessage });
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}