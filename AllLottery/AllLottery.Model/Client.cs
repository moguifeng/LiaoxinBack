using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 客户表
    /// </summary>
    public class Client : BaseModel
    {
        public Client()
        {
         }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 环信的Id
        /// </summary>
        public string HuanXinId { get; set; }

        /// <summary>
        /// 聊信号
        /// </summary>
        [MaxLength(20)]
        [ZzbIndex(IsUnique = true)]
        public string LiaoxinNumber { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(20)]
        public string NickName { get; set; }

     
        public string Cover2 { get; set; }

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
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        

      
        /// <summary>
        /// 个性签名
        /// </summary>
        public string CharacterSignature { get; set; }


        /// <summary>
        /// 国家
        /// </summary>
        public int Country { get; set; }



        /// <summary>
        /// 省份
        /// </summary>
        public int Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int City { get; set; }


        /// <summary>
        /// 当前设备
        /// </summary>

       public int CurrentDeviceId { get; set; }


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
        public bool UpadteMind { get; set; }


        public enum ShowFriendCircleEnum
        {
            ThreeDay = 0,
            OneMonth = 1,
            SixMonth = 2,
            All = 3,
            
        }



    }

 
}