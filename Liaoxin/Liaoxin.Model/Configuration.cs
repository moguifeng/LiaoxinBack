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

            if (context.Clients.Count() == 0)
            {
                AddClients();
                AddArea();
                AddSystemBank();
               
                AddConfig();
                AddPictrueNews();
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
            _context.SystemBanks.Add(new SystemBank() { Name = "工商银行", Affix = new Affix() { Path = "Image/Bank/工商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "北京银行", Affix = new Affix() { Path = "Image/Bank/北京银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "广发银行", Affix = new Affix() { Path = "Image/Bank/广发银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "华夏银行", Affix = new Affix() { Path = "Image/Bank/华夏银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "交通银行", Affix = new Affix() { Path = "Image/Bank/交通银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "浦发银行", Affix = new Affix() { Path = "Image/Bank/浦发银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "上海银行", Affix = new Affix() { Path = "Image/Bank/上海银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "兴业银行", Affix = new Affix() { Path = "Image/Bank/兴业银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "招商银行", Affix = new Affix() { Path = "Image/Bank/招商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国光大银行", Affix = new Affix() { Path = "Image/Bank/中国光大银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国建设银行", Affix = new Affix() { Path = "Image/Bank/中国建设银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国民生银行", Affix = new Affix() { Path = "Image/Bank/中国民生银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国农业银行", Affix = new Affix() { Path = "Image/Bank/中国农业银行.png" } });
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