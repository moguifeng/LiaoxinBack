using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户关系详细表
    /// </summary>
    public class ClientRelationDetail : BaseModel
    {
        public ClientRelationDetail()
        {
        }
         
  
        public int ClientRelationId { get; set; }

        public virtual ClientRelation ClientRelation { get; set; }


        public int ClientId { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Telephone { get; set; }
        
        public virtual Client Client { get; set; }







    }

 
}