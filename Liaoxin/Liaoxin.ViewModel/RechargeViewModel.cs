using System;
using System.Collections.Generic;
using System.Text;

namespace LIaoxin.ViewModel
{

    public class AddRechargeRequest
    {
        public Guid ClientBankId { get; set; }

        public decimal Money { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 资金密码
        /// </summary>
        public string CoinPassword { get; set; }
    }


    public class RechargeResponse
    {
        public Guid RechargeId { get; set; }


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
