using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 客户黑名单
    /// </summary>
    public class ClientBlack : BaseModel
    {
        public ClientBlack()
        {
        }
        
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

       

        public int FromClientId { get; set; }

        public int ToClientId { get; set; }

        

    }

 
}