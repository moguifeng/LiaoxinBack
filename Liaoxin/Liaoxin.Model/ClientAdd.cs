using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户添加(默认3天)
    /// </summary>
    public class ClientAdd : BaseModel
    {
        public ClientAdd()
        {
        }

        public Guid ClientAddId { get; set; } = Guid.NewGuid();

 

        
        public virtual List<ClientAddDetail> ClientAddDetails { get; set; }

        /// <summary>
        /// 当前设备登录的客户
        /// </summary>
        public Guid ClientId { get; set; }


        /// <summary>
        /// 当前设备登录的客户
        /// </summary>
        public virtual Client Client { get; set; }

       

        

    }

 
}