using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.Common;
using Zzb.EF;

namespace Liaoxin.Model
{
    public partial class Configuration
    {
        private static LiaoxinContext _context;

        public static void Seed(LiaoxinContext context)
        {
            _context = context;

            AddSystemBank();
            context.SaveChanges();
            if (context.Clients.Count() == 0)
            {
                AddClients();
                AddArea();

               
              //  AddConfig();
                //AddPictrueNews();
                context.SaveChanges();
            }
            AddTestClientData();
        }

        private static void AddPictrueNews()
        {
            _context.PictureNewses.Add(new PictureNews() { Affix = new Affix() { Path = "Image/News/1.jpg" }, SortIndex = 1 });
            _context.PictureNewses.Add(new PictureNews() { Affix = new Affix() { Path = "Image/News/2.jpg" }, SortIndex = 2 });
            _context.PictureNewses.Add(new PictureNews() { Affix = new Affix() { Path = "Image/News/3.png" }, SortIndex = 3 });
        }


        private static void AddTestClientData()
        {
            //for (int i = 0; i < 1000; i++)
            //{
            //    Client client = new Client() { NickName ="testdata"+i, Coin = 10000, CoinPassword = "12345678" };
            //    _context.Clients.Add(client);
            //    GroupClient gc = new GroupClient() {  GroupId = Guid.Parse( "623d17d0-f907-4183-a505-59b907fe18d6"), ClientId = client.ClientId,  };
            //    _context.GroupClients.Add(gc);
            //}
            //var res = _context.SaveChanges();
        }


        private static void AddConfig()
        {
            var affix = _context.Affixs.Add(new Affix() { Path = "Image/logo.png" });
            _context.SaveChanges();
            _context.SystemConfigs.Add(new SystemConfig(SystemConfigEnum.Logo, affix.Entity.AffixId.ToString()));
        }

 

        private static void AddSystemBank()
        {


            _context.SystemBanks.Add(new SystemBank() { Name = "北部湾银行", Affix = new Affix() { Path = "Image/Bank/北部湾银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "渤海银行", Affix = new Affix() { Path = "Image/Bank/渤海银行.png" } });
             _context.SystemBanks.Add(new SystemBank() { Name = "成都农商银行", Affix = new Affix() { Path = "Image/Bank/成都农商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "成都银行", Affix = new Affix() { Path = "Image/Bank/成都银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "承德银行", Affix = new Affix() { Path = "Image/Bank/承德银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "稠州银行", Affix = new Affix() { Path = "Image/Bank/稠州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "大连银行", Affix = new Affix() { Path = "Image/Bank/大连银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "德州银行", Affix = new Affix() { Path = "Image/Bank/德州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "东营银行", Affix = new Affix() { Path = "Image/Bank/东营银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "福建农信", Affix = new Affix() { Path = "Image/Bank/福建农信.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "抚顺银行", Affix = new Affix() { Path = "Image/Bank/抚顺银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "富邦华一银行", Affix = new Affix() { Path = "Image/Bank/富邦华一银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "甘肃银行", Affix = new Affix() { Path = "Image/Bank/甘肃银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "广东南粤银行", Affix = new Affix() { Path = "Image/Bank/广东南粤银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "广东农信银行", Affix = new Affix() { Path = "Image/Bank/广东农信银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "广州银行", Affix = new Affix() { Path = "Image/Bank/广州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "桂林银行", Affix = new Affix() { Path = "Image/Bank/桂林银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "哈尔滨银行", Affix = new Affix() { Path = "Image/Bank/哈尔滨银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "汉口银行", Affix = new Affix() { Path = "Image/Bank/汉口银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "杭州银行", Affix = new Affix() { Path = "Image/Bank/杭州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "河北银行", Affix = new Affix() { Path = "Image/Bank/河北银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "湖北农信", Affix = new Affix() { Path = "Image/Bank/湖北农信.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "湖南农信", Affix = new Affix() { Path = "Image/Bank/湖南农信.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "华润银行", Affix = new Affix() { Path = "Image/Bank/华润银行.png" } });
        _context.SystemBanks.Add(new SystemBank() { Name = "华夏银行", Affix = new Affix() { Path = "Image/Bank/华夏银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "黄河农商", Affix = new Affix() { Path = "Image/Bank/黄河农商.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "徽商银行", Affix = new Affix() { Path = "Image/Bank/徽商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "吉林银行", Affix = new Affix() { Path = "Image/Bank/吉林银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "江苏农信", Affix = new Affix() { Path = "Image/Bank/江苏农信.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "江苏银行", Affix = new Affix() { Path = "Image/Bank/江苏银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "江西银行", Affix = new Affix() { Path = "Image/Bank/江西银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "江阴农商", Affix = new Affix() { Path = "Image/Bank/江阴农商.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "锦州银行", Affix = new Affix() { Path = "Image/Bank/锦州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "晋商银行", Affix = new Affix() { Path = "Image/Bank/晋商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "九江银行", Affix = new Affix() { Path = "Image/Bank/九江银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "昆仑银行", Affix = new Affix() { Path = "Image/Bank/昆仑银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "兰州银行", Affix = new Affix() { Path = "Image/Bank/兰州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "柳州银行", Affix = new Affix() { Path = "Image/Bank/柳州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "洛阳银行", Affix = new Affix() { Path = "Image/Bank/洛阳银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "蒙商银行", Affix = new Affix() { Path = "Image/Bank/蒙商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "民泰银行", Affix = new Affix() { Path = "Image/Bank/民泰银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "南京银行", Affix = new Affix() { Path = "Image/Bank/南京银行.png" } });

            _context.SystemBanks.Add(new SystemBank() { Name = "内蒙古银行", Affix = new Affix() { Path = "Image/Bank/内蒙古银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "宁波银行", Affix = new Affix() { Path = "Image/Bank/宁波银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "宁夏银行", Affix = new Affix() { Path = "Image/Bank/宁夏银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "平安银行", Affix = new Affix() { Path = "Image/Bank/平安银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "平顶山银行", Affix = new Affix() { Path = "Image/Bank/平顶山银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "齐鲁银行", Affix = new Affix() { Path = "Image/Bank/齐鲁银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "齐商银行", Affix = new Affix() { Path = "Image/Bank/齐商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "青海银行", Affix = new Affix() { Path = "Image/Bank/青海银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "山西省农信", Affix = new Affix() { Path = "Image/Bank/山西省农信.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "深圳农商银行", Affix = new Affix() { Path = "Image/Bank/深圳农商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "盛京银行", Affix = new Affix() { Path = "Image/Bank/盛京银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "顺德农村商业银行", Affix = new Affix() { Path = "Image/Bank/顺德农村商业银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "苏州农商银行", Affix = new Affix() { Path = "Image/Bank/苏州农商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "苏州银行", Affix = new Affix() { Path = "Image/Bank/苏州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "台州银行", Affix = new Affix() { Path = "Image/Bank/台州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "泰隆商行", Affix = new Affix() { Path = "Image/Bank/泰隆商行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "唐山银行", Affix = new Affix() { Path = "Image/Bank/唐山银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "天津银行", Affix = new Affix() { Path = "Image/Bank/天津银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "威海银行", Affix = new Affix() { Path = "Image/Bank/威海银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "潍坊银行", Affix = new Affix() { Path = "Image/Bank/潍坊银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "温州银行", Affix = new Affix() { Path = "Image/Bank/温州银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "乌鲁木齐银行", Affix = new Affix() { Path = "Image/Bank/乌鲁木齐银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "无锡农商", Affix = new Affix() { Path = "Image/Bank/无锡农商.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "西安银行", Affix = new Affix() { Path = "Image/Bank/西安银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "邮储银行", Affix = new Affix() { Path = "Image/Bank/邮储银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "长春发展农村商业银行", Affix = new Affix() { Path = "Image/Bank/长春发展农村商业银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "长沙银行", Affix = new Affix() { Path = "Image/Bank/长沙银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "浙江农信", Affix = new Affix() { Path = "Image/Bank/浙江农信.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "浙商银行", Affix = new Affix() { Path = "Image/Bank/浙商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "郑州银行", Affix = new Affix() { Path = "Image/Bank/郑州银行.png" } });            
            _context.SystemBanks.Add(new SystemBank() { Name = "中信银行", Affix = new Affix() { Path = "Image/Bank/中信银行.png" } });




            _context.SystemBanks.Add(new SystemBank() { Name = "工商银行", Affix = new Affix() { Path = "Image/Bank/工商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "北京银行", Affix = new Affix() { Path = "Image/Bank/北京银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "广发银行", Affix = new Affix() { Path = "Image/Bank/广发银行.png" } });            
            _context.SystemBanks.Add(new SystemBank() { Name = "交通银行", Affix = new Affix() { Path = "Image/Bank/交通银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "浦发银行", Affix = new Affix() { Path = "Image/Bank/浦发银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "上海银行", Affix = new Affix() { Path = "Image/Bank/上海银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "兴业银行", Affix = new Affix() { Path = "Image/Bank/兴业银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "招商银行", Affix = new Affix() { Path = "Image/Bank/招商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "光大银行", Affix = new Affix() { Path = "Image/Bank/光大银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "建设银行", Affix = new Affix() { Path = "Image/Bank/建设银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "民生银行", Affix = new Affix() { Path = "Image/Bank/民生银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "农业银行", Affix = new Affix() { Path = "Image/Bank/农业银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国银行", Affix = new Affix() { Path = "Image/Bank/中国银行.png" } });
        }




        public static void AddClients()
        {
            //List<Guid> clientIds = new List<Guid>();
            //for (int i = 1; i <= 200; i++)
            //{
            //    var client = new Client() {  LiaoxinNumber = "fff" + i, HuanXinId = "abc" + i, NickName = "测试客户" + i, Password = SecurityHelper.Encrypt("1"), CoinPassword = SecurityHelper.Encrypt("1"), IsEnable = true };
            //    clientIds.Add(client.ClientId);
            //    _context.Clients.Add(client);
            //}


            //for (int i = 1; i <= 20; i++)
            //{
            //    var group = new Group() { Name = "群组" + i, AllBlock = false, HuanxinGroupId = "eifjeifje" + i, Notice = "公告" + i, ClientId = clientIds[0] };
            //    _context.Groups.Add(group);
            //}


            //for (int i = 1; i <= 200; i++)
            //{
            //    if (i % 5 == 0 && i <= 190)
            //    {
            //        ClientRelation cr = new ClientRelation() { ClientId = clientIds[i], RelationType = RelationTypeEnum.Friend };
            //        _context.ClientRelations.Add(cr);
            //        for (int j = i + 1; j < i + 5; j++)
            //        {
            //            ClientRelationDetail crd = new ClientRelationDetail()
            //            {
            //                ClientId = clientIds[j],                            
            //                ClientRelationId = cr.ClientRelationId,
            //                AddSource = ClientRelationDetail.AddSourceTypeEnum.Phone
            //            };
            //            _context.ClientRelationDetails.Add(crd);
            //        }



            //    }
            //}


            //_context.SaveChanges();
        }


        public static void AddArea()
        {
            var areas = Configuration.AreaStr.Split(",");
            for (int i = 0; i < areas.Length; i++)
            {
                var area = areas[i];
                try
                {

                    string[] fields = area.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length == 5)
                    {
                        string code = fields[0].ToString().Trim();
                        string name = fields[1].ToString().Trim();
                        string fullName = fields[2].ToString().Trim();
                        string longCode = fields[3].ToString().Trim();
                        int level = Int32.Parse(fields[4].ToString());
                        string parentCode = "";
                        string[] codes = longCode.Split('.');
                        if (codes.Length > 1)
                        {
                            parentCode = codes[codes.Length - 2];
                        }

                        Area areaEntity = new Area() { Code = code, FullName = fullName, Level = level, Name = name, LongCode = longCode, ParentCode = parentCode };
                        _context.Areas.Add(areaEntity);
                    }

                }
                catch (Exception ex)
                {

                    int a = 1;
                }


            }
            int res = _context.SaveChanges();
        }


    }

}