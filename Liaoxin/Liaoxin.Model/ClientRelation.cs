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

        /// <summary>
        /// 客户Id
        /// </summary>
        public int ClientId { get; set; }


        public virtual List<ClientRelationDetail> ClientRelationDetail { get; set; }

        /// <summary>
        /// 这个客户的关系
        /// </summary>
        public RelationTypeEnum Relation { get; set; }



        public enum RelationTypeEnum
        {
            Friend = 0,
            Black = 1,
        }

    }


}