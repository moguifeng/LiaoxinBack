using System;
using System.Collections.Generic;
using System.Text;

namespace LIaoxin.ViewModel
{

    public class AddWithdrawRequest
    {
        public Guid ClientBankId { get; set; }


        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 支付密码
        /// </summary>
        public string CoinPassword { get; set; }
    }


    public class WithdrawResponse
    {
        public Guid WithdrawId { get; set; }


        public string OrderNo { get; set; }

        public Guid ClientId { get; set; }


        public decimal Money { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public int State { get; set; }

        public Guid ClientBankId { get; set; }

    }
}
