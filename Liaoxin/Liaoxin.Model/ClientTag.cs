using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户标签表
    /// </summary>
    public class ClientTag : BaseModel
    {
        public ClientTag()
        {
        }           
        public Guid ClientTagId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Name { get; set; }
 
    }

 
}