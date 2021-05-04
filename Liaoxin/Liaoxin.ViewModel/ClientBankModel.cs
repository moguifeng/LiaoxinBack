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
        /// 卡号
        /// </summary>
            public string CardNumber { get; set; }

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
        public int AffixId { get; set; }

    }
}
