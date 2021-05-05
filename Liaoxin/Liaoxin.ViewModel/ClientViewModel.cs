using System;
using System.Collections.Generic;
using System.Text;
using static Liaoxin.Model.Client;

namespace Liaoxin.ViewModel
{
   public class ClientViewModel
    {
        public class ClientLoginRequest
        {
            /// <summary>
            /// 电话号码
            /// </summary>
            public string Telephone { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }

        }


        public class ClientSendCodeRequest
        {
            /// <summary>
            /// 电话号码
            /// </summary>
            public string Telephone { get; set; }
        }

        public class ClientLoginByCodeRequest
        {
            /// <summary>
            /// 电话号码
            /// </summary>
            public string Telephone { get; set; }

            /// <summary>
            /// 验证码
            /// </summary>
            public string Code { get; set; }

        }

        public class ClientChangePasswordRequest
        {
            
            /// <summary>
            /// 新的登录密码
            /// </summary>
            public string newPsssword { get; set; }
            /// <summary>
            /// 旧的登录密码
            /// </summary>
            public string oldPassword { get; set; }            
        }


        public class ClientChangeCoinPasswordRequest
        {
          
            /// <summary>
            /// 新的交易密码
            /// </summary>
            public string newCoinPsssword { get; set; }
            /// <summary>
            /// 旧的交易密码
            /// </summary>
            public string oldCoinPassword { get; set; }
        }

        public class ClientBaseInfoResponse
        {
            /// <summary>
            /// 头像
            /// </summary>
            public int? Cover { get; set; }
            /// <summary>
            /// 环信的Id
            /// </summary>
            public string HuanXinId { get; set; }

            /// <summary>
            /// 聊信号
            /// </summary>

            public string LiaoxinNumber { get; set; }

            /// <summary>
            /// 昵称
            /// </summary>
            public string NickName { get; set; }


            /// <summary>
            /// 余额
            /// </summary>
            public decimal Coin { get; set; } 

            /// <summary>
            /// 手机号码
            /// </summary>                    
            public string Telephone { get; set; }



            /// <summary>
            /// 个性签名
            /// </summary>
            public string CharacterSignature { get; set; }


            /// <summary>
            /// 地区
            /// </summary>
            public string AreaCode { get; set; }


            /// <summary>
            /// 添加我为好友时需要验证.
            /// </summary>
            public bool AddMeNeedChecked { get; set; }



            /// <summary>
            /// 显示朋友圈时间范围(enum:ShowFriendCircleEnum)
            /// </summary>
            public ShowFriendCircleEnum ShowFriendCircle { get; set; }

            /// <summary>
            ///更新提醒
            /// </summary>
            public bool UpadteMind { get; set; } = false;  
  
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

      

           

            /// <summary>
            ///新消息通知
            /// </summary>
            public bool NewMessageNotication { get; set; }

            /// <summary>
            /// 音频通话提醒
            /// </summary>
            public bool VideoMessageNotication { get; set; }

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


        }

    }
}
