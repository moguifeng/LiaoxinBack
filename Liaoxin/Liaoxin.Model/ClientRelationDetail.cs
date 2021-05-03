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
         public Guid ClientRelationDetailId { get; set; } = Guid.NewGuid();


        [ZzbIndex]
        public Guid ClientRelationId { get; set; }

        public virtual ClientRelation ClientRelation { get; set; }


        /// <summary>
        /// 电话号码
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public Guid? ClientTagId { get; set; }
        

        /// <summary>
        /// 标签
        /// </summary>
        public virtual ClientTag ClientTag { get; set; }

        public Guid ClientId { get; set; }


        /// <summary>
        /// 与客户关系的客户
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// 不看Ta
        /// </summary>
        public bool NotSee { get; set; } = false;



        /// <summary>
        /// 不让Ta看
        /// </summary>
        public bool NotLetSee { get; set; } = false;



        /// <summary>
        /// 特别关注
        /// </summary>
        public bool SpecialAttention { get; set; } = false;

        /// <summary>
        /// 客户备注
        /// </summary>
        public string ClientRemark { get; set; }




        /// <summary>
        /// 来自于
        /// </summary>
        public AddSourceTypeEnum AddSource { get; set; }  



        public enum AddSourceTypeEnum
        {
            [Description("通过手机添加好友")]
            Phone = 0 ,

            [Description("通过扫描添加好友")]
            Scan = 1,

            [Description("通过群组添加好友")]
            Group = 1,


        }

        /// <summary>
        /// 共同群聊
        /// </summary>
        public int MutipleGroup { get; set; } = 0;




    }

 
}