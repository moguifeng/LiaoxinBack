using System;
using System.Linq;
using AllLottery.IBusiness;
using AllLottery.Model;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.PlayerManager
{
    public class SoftwareExpiredViewModel : BaseServiceNav
    {
        public override bool ShowOperaColumn => true;

        public override string FolderName => "玩家管理";
        public override string NavName => "软件期限";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int SoftwareExpiredId { get; set; }

        [NavField("玩家", 100)]
        public string Name { get; set; }

        [NavField("过期时间", 200)]
        public DateTime Expired { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<SoftwareExpired, SoftwareExpiredViewModel>(from s in Context.SoftwareExpireds
                                                                            select s, (k, t) =>
                                                                            {
                                                                                t.Name = k.Player.Name;
                                                                            });
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new SoftwareExpiredAddViewModel("Add", "新建期限") { Icon = "plus" } };
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new SoftwareExpiredEditViewModel("Edit", "编辑"), new ConfirmActionButton("Delete", "删除", "是否确定删除？"), };
        }

        public void Delete()
        {
            var expired = (from p in Context.SoftwareExpireds where p.SoftwareExpiredId == SoftwareExpiredId select p).First();
            UserOperateLogService.Log($"删除[{expired.Player.Name}]玩家软件权限", Context);
            Context.SoftwareExpireds.Remove(expired);
            Context.SaveChanges();
        }
    }
}