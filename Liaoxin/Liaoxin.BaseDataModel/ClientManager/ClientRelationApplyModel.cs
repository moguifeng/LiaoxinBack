using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class ClientRelationApplyModel : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public ClientRelationApplyModel()
        {
        }

        public ClientRelationApplyModel(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "添加客户申请(通常用于机器人)";

        [TextField("源聊信号", IsRequired =true)]
        public string SourceLiaoxinNumber { get; set; }

        [TextField("目标聊信号", IsRequired = true)]
        public string ToLiaoxinNumber { get; set; }



        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            ClientAdd clientAddEntity = new ClientAdd();         
            //Context.Clients.Add(client);
            //UserOperateLogService.Log($"新增[{client.LiaoxinNumber}]聊信客户", Context);
            //Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}