using AllLottery.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Zzb.Common;
using Zzb.ZzbLog;

namespace AllLottery.Spider
{
    public class CaiPiaoKongApiSpider : BaseSpiderThread
    {
        public override string Url => "http://api.caipiaokong.cn/lottery/?name=hnkw&format=json&uid=559504&token=3a3e120afcbcf4b308c687481702a4763723d922";

        Dictionary<string, bool> dic = new Dictionary<string, bool>();

        protected override void StartRun(LotteryType type)
        {
            Dic.Add(type.LotteryTypeId, string.Empty);
            LogHelper.Debug("开始彩票控爬虫");
            new Task(() =>
            {
                try
                {
                    while (true)
                    {

                        LogHelper.Debug("进入循环开始");
                        string html = null;
                        try
                        {
                            html = HttpHelper.GetPage(Url);
                            var json = JsonConvert.DeserializeObject<Dictionary<string, CaiPiaoKongModel>>(html);
                            foreach (KeyValuePair<string, CaiPiaoKongModel> m in json)
                            {
                                m.Value.Id = m.Key;
                                if (!dic.ContainsKey(m.Key))
                                {
                                    LotteryData data = new LotteryData()
                                    {
                                        Data = GetData(m.Value.Number),
                                        LotteryTypeId = type.LotteryTypeId,
                                        Number = m.Value.Id,
                                        Time = m.Value.Dateline
                                    };
                                    if (AddData(data))
                                    {
                                        Log(type, data);
                                    }

                                    dic.Add(m.Key, true);
                                }
                            }

                            if (dic.Count > 10000)
                            {
                                dic = new Dictionary<string, bool>();
                            }
                        }
                        catch (System.Xml.XmlException)
                        {
                            Console.WriteLine($"开奖接口异常，请检查。[{html}]");
                            LogHelper.Error($"开奖接口异常，请检查。[{html}]");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"采集彩种[{type.Name}]失败:{e}");
                            LogHelper.Error($"采集彩种[{type.Name}]失败:{e}");
                        }

                        Thread.Sleep(5000);


                        LogHelper.Debug("循环结束");
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("循环出错", e);
                }
                finally
                {
                    LogHelper.Debug("循环出错");
                }
            }).Start();
        }

        protected virtual string GetData(string data)
        {
            return data;
        }

        protected void Log(LotteryType type, LotteryData data)
        {
            Console.WriteLine($"采集彩种[{type.Name}]成功。期号：[{data.Number}]、开奖数据：[{data.Data}]、 开奖时间：[{data.Time.ToCommonString()}]");
            LogHelper.Debug($"采集彩种[{type.Name}]成功。期号：[{data.Number}]、开奖数据：[{data.Data}]、 开奖时间：[{data.Time.ToCommonString()}]");
        }

        protected static Dictionary<int, string> Dic = new Dictionary<int, string>();

        protected bool AddData(LotteryData data)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var exist = from d in context.LotteryDatas
                            where d.LotteryTypeId == data.LotteryTypeId && d.Number == data.Number
                            select d;
                if (exist.Any())
                {
                    return false;
                }
                else
                {
                    context.LotteryDatas.Add(data);
                    context.SaveChanges();
                    return true;
                }
            }
        }

    }

    public class CaiPiaoKongModel
    {
        public string Id { get; set; }

        public string Number { get; set; }

        public DateTime Dateline { get; set; }
    }

    public class XmlHelper
    {
        public XmlDocument XmlDoc { get; set; } = new XmlDocument();

        public XmlHelper(string text)
        {
            XmlDoc.LoadXml(text);
        }

        public XmlHelper(Xml xml)
        {
            XmlDeclaration dec = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlDoc.AppendChild(dec);
            XmlDoc.AppendChild(CreateXmlNode(xml));
        }

        public string GetXml()
        {
            return XmlDoc.InnerXml;
        }

        private XmlNode CreateXmlNode(Xml xml)
        {
            var ele = XmlDoc.CreateElement(xml.Key);
            if (xml.Xmls != null && xml.Xmls.Count != 0)
            {
                foreach (Xml x in xml.Xmls)
                {
                    ele.AppendChild(CreateXmlNode(x));
                }
            }
            else
            {
                if (xml.IsCData)
                {
                    XmlCDataSection cd = XmlDoc.CreateCDataSection(xml.Value);
                    ele.AppendChild(cd);
                }
                else
                {
                    ele.InnerText = xml.Value;
                }
            }
            return ele;
        }

        public void SetValue(string path, string value)
        {
            XmlNode node = XmlDoc.SelectSingleNode(path);
            if (node == null)
            {
                return;
            }
            XmlCDataSection cData = node.FirstChild as XmlCDataSection;
            if (cData == null)
            {
                node.Value = value;
            }
            else
            {
                cData.Value = value;
            }
        }

        public string GetValue(string path)
        {
            XmlNode node = XmlDoc.SelectSingleNode(path);
            XmlCDataSection cData = node?.FirstChild as XmlCDataSection;
            if (cData != null)
            {
                return cData.Value;
            }
            else
            {
                return node?.InnerText;
            }
        }

        public string GetValue(string path, string attr)
        {
            XmlNode node = XmlDoc.SelectSingleNode(path);
            return node.Attributes[attr].Value;
        }

        public string GetValue(string path, string attr, int index)
        {
            XmlNode node = XmlDoc.SelectNodes(path)[index];
            return node.Attributes[attr].Value;
        }
    }

    public class Xml
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public bool IsCData { get; set; }

        public List<Xml> Xmls { get; set; }
    }
}