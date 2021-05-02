using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [TextField("源聊信号", IsRequired = true)]
        public string SourceLiaoxinNumber { get; set; }

        [TextField("目标聊信号", IsRequired = true)]
        public string ToLiaoxinNumber { get; set; }

        [TextField("申请添加备注")]
        public string Remark { get; set; }



        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
        
            List<string> lis = new List<string>();
            lis.Add(SourceLiaoxinNumber);
            lis.Add(ToLiaoxinNumber);
            var source = Context.Clients.Where(c => lis.Contains(c.LiaoxinNumber)).Select(c => new { c.ClientId, c.LiaoxinNumber }).ToList();
            var sourceClient = source.FirstOrDefault(c => c.LiaoxinNumber == SourceLiaoxinNumber);

            if (sourceClient == null)
            {
                return new ServiceResult(ServiceResultCode.QueryNull, "没有找到源聊信号");
            }
            var  toClient = source.FirstOrDefault(c => c.LiaoxinNumber == ToLiaoxinNumber);
            if (toClient == null)
            {
                return new ServiceResult(ServiceResultCode.QueryNull, "没有找到目标聊信号");
            }

            var applySourceCnt =  Context.ClientAdds.Where(c => c.ClientId == sourceClient.ClientId).Count();
            var applyToCnt = Context.ClientAddDetails.Where(c => c.ClientId == toClient.ClientId).Count();
            if (applySourceCnt > 0 && applyToCnt > 0)
            {
                return new ServiceResult(ServiceResultCode.Error, "你已申请添加此客户,无需要重复申请");
            }

            var sourceCnt = Context.ClientRelations.Where(c => c.ClientId == sourceClient.ClientId && c.RelationType == ClientRelation.RelationTypeEnum.Friend).Count();
            var toCnt = Context.ClientRelationDetails.Where(c => c.ClientId == toClient.ClientId ).Count();

            if (sourceCnt > 0 && toCnt > 0)
            {
                return new ServiceResult(ServiceResultCode.Error, "你已添加此客户,无需要重复添加");

            }
            ClientAdd clientAddEntity = new ClientAdd();
            clientAddEntity.ClientId = sourceClient.ClientId;
            Context.ClientAdds.Add(clientAddEntity);
            ClientAddDetail detailEntity = new ClientAddDetail()
            {
                ClientAddId = clientAddEntity.ClientAddId,
                AddRemark = Remark,
                ClientId = toClient.ClientId
            };
            Context.ClientAddDetails.Add(detailEntity);
            UserOperateLogService.Log($"添加客户申请,源客户[{sourceClient.LiaoxinNumber}]申请添加客户[{toClient.LiaoxinNumber}]", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}