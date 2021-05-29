using System;
using System.Collections.Generic;
using System.Text;

namespace LIaoxin.ViewModel
{
   public class ClientBankResponse
    {
        /// <summary>
        /// 客户银行卡id
        /// </summary>
        public Guid ClientBankId { get; set; }


        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; } = "储蓄卡";


        /// <summary>
        /// 卡名称
        /// </summary>
        public string CardName { get; set; }


        /// <summary>
        /// 卡的前位数
        /// </summary>
        public string FrontCardNumber { get; set; }




        /// <summary>
        /// 卡的后位数
        /// </summary>
        /// 
        public string BackCardNumber { get; set;  }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 银行卡id
        /// </summary>
        public Guid SystemBankId { get; set; }

        /// <summary>
        /// 附件id
        /// </summary>
        public Guid AffixId { get; set; }

    }
}
