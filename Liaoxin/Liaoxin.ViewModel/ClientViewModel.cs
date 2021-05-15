using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Zzb.Common;
using static Liaoxin.Model.Client;
using static Liaoxin.Model.ClientAddDetail;
using static Liaoxin.Model.ClientRelationDetail;

namespace Liaoxin.ViewModel
{



    public class ResgerClientRequest
    {
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }



        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 头像
        /// </summary>
        public Guid? Cover { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string EquimentType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquimentName { get; set; }


    }

    public class ClientSendCodeRequest
    {
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 发送类型  0:登录  1:找回密码
        /// </summary>
        public VerificationCodeTypes Type { get; set; }

    }

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

            /// <summary>
            /// 设备名称
            /// </summary>
            public string EquimentName { get; set; }

            /// <summary>
            /// 设备类型
            /// </summary>
            public string EquimentType { get; set; }

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

            /// <summary>
            /// 设备名称
            /// </summary>
            public string EquimentName { get; set; }

            /// <summary>
            /// 设备类型
            /// </summary>
            public string EquimentType { get; set; }

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


        public class SetClientCoinPasswordRequest
        {

            /// <summary>
            /// 交易密码
            /// </summary>
            public string CoinPsssword { get; set; }
          
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

        public class CEquiment
        {
         
            /// <summary>
            /// 设备名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 设备类型
            /// </summary>
            public string Type { get; set; }
            /// <summary>
            /// 最后登录时间
            /// </summary>
            public DateTime LastLoginDate { get; set; }
        }

        public class ClientBaseInfoResponse
        {

            public ClientBaseInfoResponse()
            {
                Equiments = new List<CEquiment>();
            }

            public Guid ClientId { get; set; }
            /// <summary>
            /// 我的登录设备列表
            /// </summary>
            public List<CEquiment> Equiments { get; set; }
            /// <summary>
            /// 头像
            /// </summary>
            public Guid? Cover { get; set; }
            /// <summary>
            /// 环信的Id
            /// </summary>
            public string HuanXinId { get; set; }


            /// <summary>
            /// 环信token
            /// </summary>
            public string HuanxinToken { get; set; }

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
            /// 性别 1:男  0:女
            /// </summary>
            public int Gender { get; set; }


            /// <summary>
            ///电子邮箱
            /// </summary>
            public string Email { get; set; }

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
            /// 是否已经设置了资金密码
            /// </summary>
            public bool IsSetCoinPassword { get; set; }



            /// <summary>
            /// 显示朋友圈时间范围(枚举值:ShowFriendCircleEnum)
            /// </summary>
            public int ShowFriendCircle { get; set; }

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

        public class ClientAddDetailResponse
        {
            /// <summary>
            /// 添加时备注
            /// </summary>
            public string AddRemark { get; set; }

            /// <summary>
            /// 环信id
            /// </summary>
            public string HuanxinId { get; set; }

            /// <summary>
            /// 聊信Id
            /// </summary>
            public string LiaoxinNumber { get; set; }


            /// <summary>
            ///添加客户头像id
            /// </summary>
            public Guid? Cover { get; set; }

            /// <summary>
            /// 申请添加时间
            /// </summary>
            public DateTime CreateTime { get; set; }

            /// <summary>
            /// 申请添加状态 (枚举值:ClientAddDetailTypeEnum);
            /// </summary>
            public int Status { get; set; }

            public string StatusName { get; set; }


            /// <summary>
            /// 客户昵称
            /// </summary>
            public string NickName { get; set; }


        }

        public class ClientFriendDetailResponse
        {

            /// <summary>
            /// 好友的唯一id
            /// </summary>
            public Guid ClientId { get; set; }

            /// <summary>
            /// 环信id
            /// </summary>
            public string HuanxinId { get; set; }

            /// <summary>
            /// 聊信Id
            /// </summary>
            public string LiaoxinNumber { get; set; }

 
            /// <summary>
            /// 个性签名
            /// </summary>
 
            public string CharacterSignature { get; set; }


            /// <summary>
            ///添加客户头像id
            /// </summary>

            public Guid? Cover { get; set; }

            /// <summary>
            /// 来自于
            /// </summary>
            public string Source { get; set; }

            /// <summary>
            /// 共同群聊
            /// </summary>
            public int MutipleGroupCnt { get; set; }

            /// <summary>
            /// 客户昵称
            /// </summary>
            public string NickName { get; set; }

            /// <summary>
            /// 客户昵称备注(自定义)
            /// </summary>
            public string ClientRemark { get; set; }


            /// <summary>
            /// 关系  0:好友   1:黑名单 2:陌生人
            /// </summary>
            public int FriendShipType { get; set; }



        }

        public class ClientFriendResponse
        {

            public Guid ClientId { get; set; }
            /// <summary>
            /// 环信id
            /// </summary>
            public string HuanxinId { get; set; }

            /// <summary>
            /// 聊信Id
            /// </summary>
            public string LiaoxinNumber { get; set; }


            /// <summary>
            ///添加客户头像id
            /// </summary>
            public Guid? Cover { get; set; }
        


            /// <summary>
            /// 客户昵称
            /// </summary>
            public string NickName { get; set; }

            /// <summary>
            /// 客户昵称备注(自定义)
            /// </summary>
            public string ClientRemark { get; set; }

        }

        public class ApplyAddFriendRequest
        {
            /// <summary>
            /// 环信Id
            /// </summary>
            public Guid ClientId { get; set; }

            /// <summary>
            /// 申请添加备注
            /// </summary>
            public string AddRemark { get; set; }


        }


        public class SureAddFriendRequest
        {
   
            public Guid ClientId { get; set; }

            /// <summary>
            /// 添加方式 (枚举:AddSourceTypeEnum)
            /// </summary>
            public int AddSource { get; set; }
        }


        public class SetFriendRemarkRequest
        {
            /// <summary>
            /// 好友的环信Id
            /// </summary>
            public Guid ClientId { get; set; }

            /// <summary>
            /// 好友备注
            /// </summary>
            public string Remark { get; set; }
        }


        public class ClientRelationShipRequest
        {
            /// <summary>
            /// 关系(好友/黑名单)环信Id
            /// </summary>
            public Guid ClientId { get; set; }
        }

        public class SetClientNickNameRequest
        { 
            /// <summary>
            /// 昵称
            /// </summary>
            public string NickName { get; set; }
        }



 

        public class ClientRealNameRequest
        {
            /// <summary>
            /// 真实姓名
            /// </summary>
            public string RealName { get; set; }
            /// <summary>
            /// 身份证号码
            /// </summary>
            public string UniqueNo { get; set; }

            /// <summary>
            /// 身份证正面
            /// </summary>
            public Guid FrontCover { get; set; }

            /// <summary>
            /// 身份证反面
            /// </summary>
            public Guid BackCover { get; set; }
        }



    }

    public class BindClientBankRequest
    {

        /// <summary>
        /// 系统银行卡编号 获取SystemBanks列表
        /// </summary>
        public Guid SystemBankdId { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 支付密码
        /// </summary>
        public string CoinPassword { get; set; }
    }


    public class GlobalSearchCliengResponse
    {
        public Guid ClientId { get; set; }
        /// <summary>
        /// 环信id
        /// </summary>
        public string HuanxinId { get; set; }

        /// <summary>
        /// 聊信Id
        /// </summary>
        public string LiaoxinNumber { get; set; }


        /// <summary>
        ///添加客户头像id
        /// </summary>
        public Guid? Cover { get; set; }


        /// <summary>
        /// 关系  0:好友   1:黑名单 2:陌生人
        /// </summary>

        public int FriendShipType { get; set; }



        /// <summary>
        /// 客户昵称
        /// </summary>
        public string NickName { get; set; }
    }

    public class FindPasswordByPhoneRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 新的密码
        /// </summary>
        public string NewPassword { get; set; }
        
    }

    public class ModifyClientTelephoneRequest
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string OldTelephone { get; set; }

        /// <summary>
        /// 新手机号码
        /// </summary>

        public string NewTelephone { get; set; }
        /// <summary>
        /// 新手机号验证码
        /// </summary>
        public string Code { get; set; }

      

    }


}
