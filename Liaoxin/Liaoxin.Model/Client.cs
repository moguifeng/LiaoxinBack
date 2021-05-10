using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.Common;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户表
    /// </summary>
    public class Client : BaseModel
    {
        public Client()
        {
         }

        public Guid ClientId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 头像
        /// </summary>
        public Guid? Cover { get; set; }
        /// <summary>
        /// 环信的Id
        /// </summary>
        public string HuanXinId { get; set; } = SecurityCodeHelper.CreateRandomCode(32);

        /// <summary>
        /// 聊信号
        /// </summary>
        [MaxLength(15)]
        [ZzbIndex(IsUnique = true)]
        public string LiaoxinNumber { get; set; } =  SecurityCodeHelper.CreateRandomCode(13);

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

     
 

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 资金密码
        /// </summary>
        public string CoinPassword { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Coin { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>        
        [ZzbIndex]
        public string Telephone { get; set; }
        

      
        /// <summary>
        /// 个性签名
        /// </summary>
        public string CharacterSignature { get; set; }


        /// <summary>
        /// 地区
        /// </summary>
        public string AreaCode { get; set; }


  
        public virtual List<ClientEquipment> ClientEquipments { get; set; }


        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        


        /// <summary>
        /// 添加我为好友时需要验证.
        /// </summary>
        public bool AddMeNeedChecked { get; set; }



        /// <summary>
        /// 显示朋友圈时间范围.
        /// </summary>
        public ShowFriendCircleEnum ShowFriendCircle { get; set; } =  ShowFriendCircleEnum.All;

        /// <summary>
        ///更新提醒
        /// </summary>
        public bool UpadteMind { get; set; } = false;

        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFreeze { get; set; } = false;

        public int ErrorPasswordCount { get; set; } = 0;
         
        
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string UniqueNo { get; set; }

        /// <summary>
        /// 身份证前面照片
        /// </summary>
        public Guid? UniqueFrontImg { get; set; }


        /// <summary>
        /// 身份证后面照片
        /// </summary>
        public Guid? UniqueBackImg { get; set; }


        /// <summary>
        /// 是否允许提现
        /// </summary>
        public bool CanWithdraw { get; set; } = true;

        #region 通用设置
        /// <summary>
        /// 设置->字体大小
        /// </summary>
        public int FontSize { get; set; } = 12;


        /// <summary>
        /// 听筒模式
        /// </summary>
        public bool HandFree { get; set; } = false;



        /// <summary>
        /// 移动网络下视频自动播放
        /// </summary>
        public bool WifiVideoPlay { get; set; } = false;

        #endregion

        #region  推送消息设置

        /// <summary>
        ///新消息通知
        /// </summary>
        public bool NewMessageNotication { get; set; }

        /// <summary>
        /// 音频通话提醒
        /// </summary>
        public  bool VideoMessageNotication { get; set; }

        /// <summary>
        /// 通知显示消息内容
        /// </summary>
        public bool ShowMessageNotication { get; set; }

        /// <summary>
        /// 打开时声音
        /// </summary>
       public bool AppOpenWhileSound { get; set; }

        /// <summary>
        /// 打开时震动
        /// </summary>
        public bool OpenWhileShake { get; set; }
        
        #endregion  

        public enum ShowFriendCircleEnum
        {
            [Description("三天")]
            ThreeDay = 0,
            [Description("一个月")]
            OneMonth = 1,
            [Description("半年")]
            SixMonth = 2,
            [Description("全部")]
            All = 3,
            
        }

        

        public void AddMoney(decimal money, CoinLogTypeEnum type, Guid aboutId, out CoinLog coinLog, string remark)
        {
            Coin += money;
            coinLog = new CoinLog(ClientId, money, Coin,   type, aboutId, remark);
        }


    }

 
}