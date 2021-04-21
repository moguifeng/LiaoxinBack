using AllLottery.Model;
using AllLottery.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using Zzb;
using Zzb.Common;

namespace AllLottery.Business.ThirdPay
{
    public class TiaoMaThirdPay : BaseThirdPay
    {
        private string _notifyUrl => HttpContextAccessor.HttpContext.Request.GetDefaultUrl() + "api/ThirdPay/TiaoMaCallBack";

        protected override ThirdPayModel ThirdPayUrl(Recharge recharge, decimal money, string url, MerchantsBank bank)
        {
            RequestHandler reqHandler = new RequestHandler(null);
            reqHandler.setGateUrl("https://pay.swiftpass.cn/pay/gateway");
            reqHandler.setKey(bank.MerchantsKey);
            reqHandler.setParameter("out_trade_no", recharge.OrderNo);//商户订单号
            reqHandler.setParameter("body", "recharge");//商品描述
            reqHandler.setParameter("attach", "recharge");//附加信息
            reqHandler.setParameter("total_fee", (money * 100).ToString("#"));//总金额
            reqHandler.setParameter("mch_create_ip", "127.0.0.1");//终端IP
            reqHandler.setParameter("time_start", ""); //订单生成时间
            reqHandler.setParameter("time_expire", "");//订单超时时间
            reqHandler.setParameter("service", bank.Aisle);//接口类型：pay.weixin.native
            reqHandler.setParameter("mch_id", bank.MerchantsNumber);//必填项，商户号，由平台分配
            reqHandler.setParameter("version", "1.0");//接口版本号
            reqHandler.setParameter("notify_url", _notifyUrl);
            //通知地址，必填项，接收平台通知的URL，需给绝对路径，255字符内;此URL要保证外网能访问   
            reqHandler.setParameter("nonce_str", Utils.random());//随机字符串，必填项，不长于 32 位
            reqHandler.createSign();//创建签名

            string data = Utils.toXml(reqHandler.getAllParameters());//生成XML报文
            PayHttpClient pay = new PayHttpClient();
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", data);
            pay.setReqContent(reqContent);

            if (pay.call())
            {
                ClientResponseHandler resHandler = new ClientResponseHandler();
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(bank.MerchantsKey);
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    //当返回状态与业务结果都为0时才返回支付二维码，其它结果请查看接口文档
                    if (int.Parse(param["status"].ToString()) == 0 && int.Parse(param["result_code"].ToString()) == 0)
                    {
                        return new ThirdPayModel(HttpType.Img, param["code_img_url"].ToString());
                    }
                    else
                    {
                        throw new ZzbException("错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"]);
                    }

                }
                else
                {
                    throw new ZzbException("错误代码：" + param["status"] + ",错误信息：" + param["message"]);
                }
            }
            else
            {
                throw new ZzbException("错误代码：" + pay.getResponseCode() + ",错误信息：" + pay.getErrInfo());
            }
        }

        protected override bool CreateRecharge(out Recharge thrid)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RequestHandler
    {
        public RequestHandler(HttpContext httpContext)
        {
            parameters = new Hashtable();

            this.httpContext = httpContext;
        }

        /// <summary>
        /// 网关url地址
        /// </summary>
        private string gateUrl;

        /// <summary>
        /// 密钥
        /// </summary>
        private string key;

        /// <summary>
        /// 请求的参数
        /// </summary>
        protected Hashtable parameters;

        /// <summary>
        /// debug信息
        /// </summary>
        private string debugInfo;

        protected HttpContext httpContext;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void init()
        {
            //nothing to do
        }

        /// <summary>
        /// 获取入口地址,不包含参数值
        /// </summary>
        /// <returns></returns>
        public String getGateUrl()
        {
            return gateUrl;
        }

        /// <summary>
        /// 设置入口地址,不包含参数值
        /// </summary>
        /// <param name="gateUrl">入口地址</param>
        public void setGateUrl(String gateUrl)
        {
            this.gateUrl = gateUrl;
        }

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public String getKey()
        {
            return key;
        }

        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="key">密钥字符串</param>
        public void setKey(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// 获取带参数的请求URL
        /// </summary>
        /// <returns></returns>
        public virtual string getRequestURL()
        {
            this.createSign();

            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();
            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + Utils.UrlEncode(v, getCharset()) + "&");
                }
            }

            //去掉最后一个&
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return this.getGateUrl() + "?" + sb.ToString();
        }

        /// <summary>
        ///创建md5摘要,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。
        /// </summary>
        public virtual void createSign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());

            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToUpper();

            this.setParameter("sign", sign);

            //debug信息
            this.setDebugInfo(sb.ToString() + " => sign:" + sign);
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="parameter">参数名</param>
        /// <returns></returns>
        public string getParameter(string parameter)
        {
            string s = (string)parameters[parameter];
            return (null == s) ? "" : s;
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="parameter">参数名</param>
        /// <param name="parameterValue">参数值</param>
        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }

        public void doSend()
        {
            this.httpContext.Response.Redirect(this.getRequestURL());
        }

        /// <summary>
        /// 获取debug信息
        /// </summary>
        /// <returns></returns>
        public String getDebugInfo()
        {
            return debugInfo;
        }

        /// <summary>
        /// 设置debug信息
        /// </summary>
        /// <param name="debugInfo"></param>
        public void setDebugInfo(String debugInfo)
        {
            this.debugInfo = debugInfo;
        }

        /// <summary>
        /// 获取所有参数
        /// </summary>
        /// <returns></returns>
        public Hashtable getAllParameters()
        {
            return this.parameters;
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <returns></returns>
        protected virtual string getCharset()
        {
            //return this.httpContext.Request.ContentEncoding.BodyName;
            return "utf-8";
        }

        /// <summary>
        /// 设置页面提交的请求参数
        /// </summary>
        /// <param name="paramNames">参数名</param>
        public void setReqParameters(string[] paramNames)
        {
            this.parameters.Clear();
            foreach (string pName in paramNames)
            {
                string reqVal = this.httpContext.Request.Query[pName];
                if (String.IsNullOrEmpty(reqVal))
                {
                    continue;
                }
                this.parameters.Add(pName, reqVal);
            }
        }
    }

    public class Utils
    {
        public Utils() { }

        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="instr">URL字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                    Console.WriteLine(ex);
                }


                return res;
            }
        }


        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="instr">编码的URL字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                    Console.WriteLine(ex);
                }


                return res;

            }
        }


        /// <summary>
        /// 取时间戳生成随即数,替换交易单号中的后10位流水号
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }


        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length">随机数的长度</param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        /// <summary>
        /// 生成32位随机数
        /// </summary>
        /// <returns></returns>
        public static string random()
        {
            char[] constant = {'0','1','2','3','4','5','6','7','8','9',
                               'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            StringBuilder sb = new StringBuilder(32);
            Random rd = new Random();
            for (int i = 0; i < 32; i++)
            {
                sb.Append(constant[rd.Next(62)]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成16位订单号 by  hyf 2016年2月16日17:48:43
        /// </summary>
        /// <returns></returns>
        public static string Nmrandom()
        {
            string rm = "";
            Random ra = new Random();
            for (int i = 0; i < 16; i++)
            {
                rm += ra.Next(0, 9).ToString();
            }
            return rm;
        }
        /// <summary>
        /// 将Hashtable参数传为XML
        /// </summary>
        /// <param name="_params"></param>
        /// <returns></returns>
        public static string toXml(Hashtable _params)
        {
            StringBuilder sb = new StringBuilder("<xml>");
            foreach (DictionaryEntry de in _params)
            {
                string key = de.Key.ToString();
                sb.Append("<").Append(key).Append("><![CDATA[").Append(de.Value.ToString()).Append("]]></").Append(key).Append(">");
            }

            return sb.Append("</xml>").ToString();
        }

    }

    /// <summary>
    /// MD5Util 的摘要说明。
    /// </summary>
    public class MD5Util
    {
        public MD5Util()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr">需要签名的串</param>
        /// <param name="charset">编码</param>
        /// <returns>返回大写的MD5签名结果</returns>
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
                Console.WriteLine(ex);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
    }
    public class PayHttpClient
    {
        /// <summary>
        /// 请求内容
        /// </summary>
        private Dictionary<String, String> reqContent;

        /// <summary>
        /// 应答内容
        /// </summary>
        private string resContent;

        /// <summary>
        /// 请求方法
        /// </summary>
        private string method;

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errInfo;

        /// <summary>
        /// 超时时间,以秒为单位 
        /// </summary>
        private int timeOut;

        /// <summary>
        /// http应答编码 
        /// </summary>
        private int responseCode;

        public PayHttpClient()
        {
            this.reqContent = new Dictionary<String, String>();
            this.reqContent["url"] = "";
            this.reqContent["data"] = "";

            this.resContent = "";
            this.method = "POST";
            this.errInfo = "";
            this.timeOut = 1 * 60;//5分钟

            this.responseCode = 0;
        }

        /// <summary>
        /// 设置请求内容
        /// </summary>
        /// <param name="reqContent">内容</param>
        public void setReqContent(Dictionary<String, String> reqContent)
        {
            this.reqContent = reqContent;
        }

        /// <summary>
        /// 获取结果内容
        /// </summary>
        /// <returns></returns>
        public string getResContent()
        {
            return this.resContent;
        }

        /// <summary>
        /// 设置请求方法
        /// </summary>
        /// <param name="method">请求方法，可选:POST或GET</param>
        public void setMethod(string method)
        {
            this.method = method;
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns></returns>
        public string getErrInfo()
        {
            return this.errInfo;
        }

        /// <summary>
        /// 设置超时时间
        /// </summary>
        /// <param name="timeOut">超时时间，单位：秒</param>
        public void setTimeOut(int timeOut)
        {
            this.timeOut = timeOut;
        }

        /// <summary>
        /// 获取http状态码
        /// </summary>
        /// <returns></returns>
        public int getResponseCode()
        {
            return this.responseCode;
        }

        //执行http调用
        public bool call()
        {
            StreamReader sr = null;
            HttpWebResponse wr = null;

            HttpWebRequest hp = null;
            try
            {
                hp = (HttpWebRequest)WebRequest.Create(this.reqContent["url"]);
                string postData = this.reqContent["data"];

                hp.Timeout = this.timeOut * 1000;
                //hp.Timeout = 10 * 1000;
                if (postData != null)
                {
                    byte[] data = Encoding.UTF8.GetBytes(postData);
                    hp.Method = "POST";

                    hp.ContentLength = data.Length;

                    Stream ws = hp.GetRequestStream();

                    // 发送数据
                    ws.Write(data, 0, data.Length);
                    ws.Close();


                }


                wr = (HttpWebResponse)hp.GetResponse();
                sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);

                this.resContent = sr.ReadToEnd();
                sr.Close();
                wr.Close();
            }
            catch (Exception exp)
            {
                this.errInfo += exp.Message;
                if (wr != null)
                {
                    this.responseCode = Convert.ToInt32(wr.StatusCode);
                }

                return false;
            }

            this.responseCode = Convert.ToInt32(wr.StatusCode);

            return true;
        }
    }
    public class ClientResponseHandler
    {
        /// <summary>
        /// 密钥
        /// </summary>
        private string key;

        /// <summary>
        /// 应答的参数
        /// </summary>
        protected Hashtable parameters;

        /// <summary>
        /// debug信息
        /// </summary>
        private string debugInfo;

        /// <summary>
        /// 原始内容
        /// </summary>
        protected string content;

        private string charset = "UTF-8";

        /// <summary>
        /// 获取服务器通知数据方式，进行参数获取
        /// </summary>
        public ClientResponseHandler()
        {
            parameters = new Hashtable();
        }

        /// <summary>
        /// 获取返回内容
        /// </summary>
        /// <returns></returns>
        public string getContent()
        {
            return this.content;
        }

        /// <summary>
        /// 设置返回内容
        /// </summary>
        /// <param name="content">XML内容</param>
        public virtual void setContent(string content)
        {
            this.content = content;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);
            XmlNode root = xmlDoc.SelectSingleNode("xml");
            XmlNodeList xnl = root.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                this.setParameter(xnf.Name, xnf.InnerText);
            }
        }

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public string getKey()
        { return key; }

        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="key">密钥</param>
        public void setKey(string key)
        { this.key = key; }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="parameter">参数名</param>
        /// <returns></returns>
        public string getParameter(string parameter)
        {
            string s = (string)parameters[parameter];
            return (null == s) ? "" : s;
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="parameter">参数名</param>
        /// <param name="parameterValue">参数值</param>
        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }

        /// <summary>
        /// 是否支付平台签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。
        /// </summary>
        /// <returns></returns>
        public virtual Boolean isTenpaySign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());
            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            this.setDebugInfo(sb.ToString() + " => sign:" + sign);
            return getParameter("sign").ToLower().Equals(sign);
        }

        /// <summary>
        /// 获取debug信息
        /// </summary>
        /// <returns></returns>
        public string getDebugInfo()
        { return debugInfo; }

        /// <summary>
        /// 设置debug信息
        /// </summary>
        /// <param name="debugInfo"></param>
        protected void setDebugInfo(String debugInfo)
        { this.debugInfo = debugInfo; }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <returns></returns>
        protected virtual string getCharset()
        {
            return this.charset;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="charset">编码</param>
        public void setCharset(String charset)
        {
            this.charset = charset;
        }

        /// <summary>
        /// 是否支付平台签名,规则是:按参数名称a-z排序,遇到空值的参数不参加签名。
        /// </summary>
        /// <param name="akeys"></param>
        /// <returns></returns>
        public virtual Boolean _isTenpaySign(ArrayList akeys)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string k in akeys)
            {
                string v = (string)parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.getKey());
            string sign = MD5Util.GetMD5(sb.ToString(), getCharset()).ToLower();

            //debug信息
            this.setDebugInfo(sb.ToString() + " => sign:" + sign);
            return getParameter("sign").ToLower().Equals(sign);
        }

        /// <summary>
        /// 获取返回的所有参数
        /// </summary>
        /// <returns></returns>
        public Hashtable getAllParameters()
        {
            return this.parameters;
        }
    }

}