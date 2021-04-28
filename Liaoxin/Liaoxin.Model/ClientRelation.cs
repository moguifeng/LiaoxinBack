using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户关系表
    /// </summary>
    public class ClientRelation : BaseModel
    {
        public ClientRelation()
        {
        }

        public int ClientRelationId { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public int ClientId { get; set; }

        public  virtual Client Client { get; set; }

        public virtual List<ClientRelationDetail> ClientRelationDetail { get; set; }

        /// <summary>
        /// 这个客户的关系
        /// </summary>
        public RelationTypeEnum RelationType { get; set; }



        public enum RelationTypeEnum
        {
            [Description("好友")]
            Friend = 0,
            [Description("黑名单")]
            Black = 1,
        }

    }


}