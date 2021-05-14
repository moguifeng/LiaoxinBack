using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static Liaoxin.Model.RedPacket;

namespace LIaoxin.ViewModel
{
    public class GroupRedPacketResponse
    {

        public Guid RedPacketId { get; set; }


        public Guid GroupId { get; set; }

        /// <summary>
        /// 红包发送者
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 祝福语(尾数)
        /// </summary>
        public string Greeting { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }


        /// <summary>
        /// 红包类型
        /// </summary>
        public RedPacketTypeEnum Type { get; set; }


        /// <summary>
        /// 红包个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 剩余红包金额
        /// </summary>
        public decimal Over { get; set; }


        /// <summary>
        /// 红包状态
        /// </summary>
        public RedPacketStatus Status { get; set; }



        public IList<RedPacketReceiveResponse> RedPacketReceives { get; set; } = new List<RedPacketReceiveResponse>();
    }

    public class RedPacketPersonalResponse
    {

        public Guid RedPacketPersonalId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 红包发送者
        /// </summary>
        public Guid FromClientId { get; set; }

        /// <summary>
        /// 红包接收者者
        /// </summary>
        public Guid ToClientId { get; set; }


        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }


        /// <summary>
        /// 红包类型
        /// </summary>
        public RedPacketTranferTypeEnum Type { get; set; }



        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal Money { get; set; }



        /// <summary>
        /// 是否领取
        /// </summary>
        public bool IsReceive { get; set; }

        /// <summary>
        /// 祝福语
        /// </summary>
        public string Greeting { get; set; }

    }

    public class RedPacketReceiveResponse  
    {
        public RedPacketReceiveResponse()
        {
        }

        public Guid RedPacketReceiveId { get; set; } = Guid.NewGuid();


        public Guid RedPacketId { get; set; }
 
        /// <summary>
        /// 抢到的红包
        /// </summary>
        public decimal SnatchMoney { get; set; }

        /// <summary>
        /// 红包接收者
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 是否手气王
        /// </summary>
        public bool IsWin { get; set; } = false;

        /// <summary>
        /// 是否中奖
        /// </summary>
        public bool IsLuck { get; set; } = false;

        /// <summary>
        /// 当前中的Lucknumbers
        /// </summary>
        public string LuckNumber { get; set; }
    }

    public class CreateGroupRedPacketsRequest
    {
        /// <summary>
        /// 发红包者ClientId
        /// </summary>
        public Guid SenderClientId { get; set; }
        /// <summary>
        /// 群Id
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// 红包总金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 红包总个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 红包类型 0:拼手气红包;1:普通红包
        /// </summary>
        public int ReadPacketsType { get; set; }

        /// <summary>
        /// 祝福语
        /// </summary>
        public string Greeting { get; set; }

        /// <summary>
        /// 钱包密码
        /// </summary>
        public string CoinPassword { get; set; }

        public Guid? ClientBankId { get; set; }
        ///// <summary>
        ///// 中奖末几位
        ///// </summary>
        //public int LuckIndex { get; set; }
    }

    public class CreateRedPacketPersonalRequest
    {
        /// <summary>
        /// 发红包者ClientId
        /// </summary>
        public Guid SenderClientId { get; set; }

        /// <summary>
        /// 收红包者ClientId
        /// </summary>
        public Guid ReceiverClientId { get; set; }

        /// <summary>
        /// 红包总金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 0:红包;1:转账
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 祝福语
        /// </summary>
        public string Greeting { get; set; }
        /// <summary>
        /// 钱包密码
        /// </summary>
        public string CoinPassword { get; set; }

        public Guid? ClientBankId { get; set; }
    }

    public class ReceiveGroupRedPacketRequest
    {
        public Guid RedPacketId { get; set; }

        public Guid ClientId { get; set; }
    }

    public class ReceiveRedPacketPersonalRequest
    {
        public Guid RedPacketId { get; set; }

        public Guid ClientId { get; set; }
    }
}
