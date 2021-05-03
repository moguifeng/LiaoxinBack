using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class RateOfGroupClient : BaseModel
    {
        public RateOfGroupClient()
        {
        }           
        public Guid RateOfGroupClientId { get; set; } = Guid.NewGuid();

        public Guid GroupId { get; set; }

        public virtual Group Group { get; set; }


        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        public int Rate { get; set; } = 0;

        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsStop { get; set; } = false;


        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; } = -1;       
 
    }

 
}