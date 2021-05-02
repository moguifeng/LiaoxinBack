using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户添加详细
    /// </summary>
    public class ClientAddDetail : BaseModel
    {
        public ClientAddDetail()
        {
        }
 
       public int ClientAddDetailId { get; set; }

        public int ClientAddId { get; set; }


        public virtual ClientAdd ClientAdd { get; set; }
 
        /// <summary>
        /// 当前登录客户的申请客户
        /// </summary>
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        /// <summary>
        /// 添加备注
        /// </summary>
        public string AddRemark { get; set; }
       

        

    }

 
}