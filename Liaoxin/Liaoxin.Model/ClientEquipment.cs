using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 用户设备
    /// </summary>
    public class ClientEquipment : BaseModel
    {
        public ClientEquipment()
        {
        }
 
       
        public Guid ClientEquipmentId { get; set; } = Guid.NewGuid();


        /// <summary>
        /// 客户id
        /// </summary>
        public Guid ClientId { get; set; }
       

        public virtual Client Client { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        

    }

 
}