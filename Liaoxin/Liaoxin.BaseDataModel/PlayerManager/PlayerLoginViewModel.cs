using Liaoxin.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.PlayerManager
{
    //public class PlayerLoginViewModel : BaseServiceNav
    //{
    //    public override string NavName => "登录日志";

    //    public override string FolderName => "客户管理";

    //    [NavField("ID", IsKey = true, IsDisplay = false)]
    //    public int PlayerLoginLogId { get; set; }

    //    [NavField("玩家")]
    //    public string Name { get; set; }

    //    [NavField("IP")]
    //    public string IP { get; set; }

    //    [NavField("地址", 200)]
    //    public string Address { get; set; }

    //    [NavField("登录时间", 150)]
    //    public DateTime CreateTime { get; set; }

 

    //    protected override object[] DoGetNavDatas()
    //    {
    //        return CreateEfDatas<PlayerLoginLog, PlayerLoginViewModel>(from l in Context.PlayerLoginLogs
    //                                                                   orderby l.CreateTime descending
    //                                                                   select l, (k, t) =>
    //            {
    //                t.Name = k.Player.Name;             
    //            }, (k, w) => w.Where(t => t.Player.Name.Contains(k)),
    //            (k, w) => w.Where(t => t.Address.Contains(k)));
    //    }

    //    public override BaseFieldAttribute[] GetQueryConditionses()
    //    {
    //        return new[] { new TextFieldAttribute("Name", "玩家"), new TextFieldAttribute("Address", "地址"), };
    //    }
    //}
}