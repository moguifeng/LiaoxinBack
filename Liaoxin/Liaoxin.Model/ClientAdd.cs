using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户添加
    /// </summary>
    public class ClientAdd : BaseModel
    {
        public ClientAdd()
        {
        }
 
       public int ClientAddId { get; set; }

        public int ClientId { get; set; }

          
        public virtual Client Client { get; set; }

       

        

    }

 
}