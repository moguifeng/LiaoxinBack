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
    public class ClientTag : BaseModel
    {
        public ClientTag()
        {
        }           
        public int ClientTagId { get; set; }
 
        public int ClientId { get; set; }

        public string Name { get; set; }
 
    }

 
}