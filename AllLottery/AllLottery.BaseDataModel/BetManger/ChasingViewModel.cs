using AllLottery.Business.Cathectic;
using AllLottery.Business.Generate;
using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;
using System.Text;

namespace AllLottery.BaseDataModel.BetManger
{
//    public class ChasingViewModel : BaseServiceNav
//    {
//        public override string NavName => "追号管理";

//        public override string FolderName => "投注管理";

//        public override bool NavIsShow => true;

//        [NavField("主键", IsKey = true, IsDisplay = false)]
//        public int ChasingOrderId { get; set; }



//        [NavField("玩家姓名")]
//        public string PlayerName { get; set; }


//        [NavField("彩种")]
//        public string LotteryType { get; set; }

//        [NavField("玩法", Width = 150)]
//        public string LotteryDetailName { get; set; }

//        [NavField("开始期号")]
//        public string BeginNo { get; set; }

//        [NavField("投注号码", Width = 200)]
//        public string BetNo { get; set; }


//        [NavField("投注金额")]
//        public decimal BetMoney { get; set; }


//        [NavField("冻结金额")]

//        public decimal FreezeCoin { get; set; }



//        [NavField("中奖金额")]
//        public decimal AllWinMoney { get; set; }


//        [NavField("状态")]
//        public ChasingStatus Status { get; set; }

//        [NavField("创建时间", Width = 150)]
//        public DateTime CreateTime { get; set; }



//        [NavField("投注模式")]
//        public string BetModeName { get; set; }

//        [NavField("订单号", Width = 150)]

//        public string Order { get; set; }


//        [NavField("彩种类型", IsDisplay = false)]
//        public LotteryCalNumberTypeEnum CalType { get; set; } = LotteryCalNumberTypeEnum.Automatic;

//        [NavField("彩种类", IsDisplay = false)]
//        public LotteryClassifyType Type { get; set; } = LotteryClassifyType.Ssc;

//        [NavField("反射扩展", IsDisplay = false)]
//        public string ReflectClass { get; set; }

//        [NavField("中奖是否停止", IsDisplay = false)]
//        public bool IsWinStop { get; set; }

//        public ILotteryOpenTimeServer LotteryOpenTimeServer { get; set; }




//        protected override object[] DoGetNavDatas()
//        {

//            IQueryable<ChasingOrder> query = null;
//            if ((Query.ContainsKey("IsShowTestPlayer") && bool.Parse(Query["IsShowTestPlayer"])) || !Query.ContainsKey("IsShowTestPlayer"))
//            {
//                query = from o in Context.ChasingOrders where o.IsEnable orderby o.CreateTime descending select o;
//            }
//            else
//            {
//                query = from o in Context.ChasingOrders where o.IsEnable && !(from d in Context.NotReportPlayers select d.PlayerId).Contains(o.PlayerId) orderby o.CreateTime descending select o;
//            }
//            return CreateEfDatas<ChasingOrder, ChasingViewModel>(
//            query,
//             (k, t) =>
//             {
//                 t.PlayerName = (k.Status == ChasingStatus.Wait || k.Status == ChasingStatus.Doing)?
//                 Color(k.ChasingOrderDetails, k.Player.Name, k.LotteryPlayDetail.ReflectClass,k.BetNo,k.LotteryPlayDetail.LotteryPlayType.LotteryTypeId):k.Player.Name;
//                 t.LotteryType = k.LotteryPlayDetail.LotteryPlayType.LotteryType.Name;
//                 t.Type = k.LotteryPlayDetail.LotteryPlayType.LotteryType.LotteryClassify.Type;
//                 t.LotteryDetailName = k.LotteryPlayDetail.LotteryPlayType.Name + "-" + k.LotteryPlayDetail.Name;
//                 t.ReflectClass = k.LotteryPlayDetail.ReflectClass;
//                 t.Order = k.Order;
//                 t.BeginNo = k.ChasingOrderDetails.OrderBy(v => v.Index).First().Number;
//                 t.BetModeName = k.BetMode.Name;
//                 t.CreateTime = k.CreateTime;
//                 t.BetMoney = CalBetMoney(k.ChasingOrderDetails);
//                 t.FreezeCoin = k.ChasingOrderDetails.Sum(v => v.BetMoney);
//                 t.AllWinMoney = CalWinMoney(k.ChasingOrderDetails);
//                 t.CalType = k.LotteryPlayDetail.LotteryPlayType.LotteryType.CalType;
//                 t.IsWinStop = k.IsWinStop;


//             }, (k, w) => w.Where(t => t.Order.Contains(k)),
//                 (k, w) => w.Where(t => t.Player.Name.Contains(k)),
//                (k, w) => w.Where(t => t.LotteryPlayDetail.LotteryPlayType.LotteryType.Name.Contains(k)),
//               (k, w) => w.Where(t => t.LotteryPlayDetail.Name.Contains(k) || t.LotteryPlayDetail.LotteryPlayType.Name.Contains(k)),
//                (k, w) => ConvertEnum<ChasingOrder, ChasingStatus>(w, k, m => w.Where(t => t.Status == m))
//             );


//        }

//        decimal CalWinMoney(List<ChasingOrderDetail> ds)
//        {
//            decimal money = 0;
//            if (ds != null)
//            {
//                ds.ForEach(d => money += d.Bet == null ? 0 : d.Bet.WinMoney);
//            }

//            return money;

//        }


//        decimal CalBetMoney(List<ChasingOrderDetail> ds)
//        {
//            decimal money = 0;
//            if (ds != null)
//            {
//                ds.ForEach(d => money += d.Bet == null ? 0 : d.BetMoney);
//            }

//            return money;

//        }

//        public override BaseButton[] CreateRowButtons()
//        {
//            if (CalType == LotteryCalNumberTypeEnum.Automatic && (Status == ChasingStatus.Wait || Status == ChasingStatus.Doing))
//            {
//                return new[] { new ConfirmActionButton("ALLKill", "一键杀", "是否确定一键杀？"), new ConfirmActionButton("CancelKIll", "解除", "是否确定解除？") };
//            }
//            else
//            {
//                return null;
//            }


//        }

//        public override bool ShowOperaColumn => true;

//        public ServiceResult ALLKill()
//        {
//            var entitys = (from c in Context.ChasingOrderDetails
//                           where c.ChasingOrderId == ChasingOrderId && (c.Bet == null ||
//(c.Bet != null && c.Bet.IsEnable && c.Bet.Status == BetStatusEnum.Wait))
//                           orderby c.ChasingOrderDetailId
//                           select c).ToList();
//            StringBuilder sbText = new StringBuilder();
//            int errorCount = 0;
//            foreach (var entity in entitys)
//            {
//                var res = ExecuteMethod(entity, true);
//                string resStr = res.ReturnCode == ServiceResultCode.Success ? "成功" : "失败";
//                if (res.ReturnCode != ServiceResultCode.Success) { errorCount++; }
//                sbText.Append("【" + entity.Number + "期处理结果：" + resStr + ",信息：" + res.Message + "】");
//            }
//            if (errorCount > 0)
//            {
//                return new ServiceResult(ServiceResultCode.Error, sbText.ToString());
//            }
//            return new ServiceResult(ServiceResultCode.Success, sbText.ToString());
//        }

//        private string Color(List<ChasingOrderDetail> ds, string str,string refClass,string betNo,int lotteryTypeId)
//        {

//            int lostCount = 0;
//            int virtualLostCount = 0;
//            var realData = (from c in ds
//                            where (c.Bet == null || (c.Bet != null && c.Bet.IsEnable && c.Bet.Status == BetStatusEnum.Wait))
//                            select c).ToList();
//            var ls = (from l in Context.LotteryDatas where l.LotteryTypeId == lotteryTypeId && l.Time >=DateTime.Now.AddHours(-2) && (from c in realData select c.Number).Contains(l.Number)  select l);
//            if (ls.Count() == realData.Count && realData.Count>0)
//            {
               
//                ls.ToList().ForEach(d => {
//                    virtualLostCount++;
//                    var numbers = BaseCathectic.ConvertoNumbers(d.Data);
//                    if (BaseCathectic.CreateCathectic(refClass).IsWin(betNo, numbers) == 0)
//                    {
//                        lostCount++;
//                    }
                
//                });
             

//                if (lostCount == realData.Count)
//                {
//                    return $"<b style='color:red;margin:0'>{str}</b>";
//                } 
//            }
//            return str+"(追:"+realData.Count+",输:"+lostCount+")";

//        }

//        public ServiceResult CancelKIll()
//        {
//            var entitys = (from c in Context.ChasingOrderDetails
//                           where
//c.ChasingOrderId == ChasingOrderId && (c.Bet == null || (c.Bet != null && c.Bet.IsEnable && c.Bet.Status == BetStatusEnum.Wait))
//                           orderby c.ChasingOrderDetailId
//                           select c).ToList();
//            foreach (var entity in entitys)
//            {

//                ExecuteMethod(entity, false);

//            }

//            return null;
//        }

//        List<string> lisCache = new List<string>();

//        ServiceResult ExecuteMethod(ChasingOrderDetail entity, bool kill)
//        {
//            var lotteryTypeId = entity.ChasingOrder.LotteryPlayDetail.LotteryPlayType.LotteryTypeId;

//            var datePreCal = DateTime.Now;

//            while ((DateTime.Now - datePreCal).TotalSeconds <= 5)
//            {
//                var data = BaseGenerate.CreateGenerate(Type).RandomGenerate();
//                var numbers = BaseCathectic.ConvertoNumbers(data);

//                int count = 0;
//                //不开重复
//                while (lisCache.Contains(data))
//                {
//                    count++;
//                    if (count > 10)
//                    {
//                        return new ServiceResult(ServiceResultCode.Error, "已用光可能性.杀号失败");
//                    }
//                }
//                long winCount = 0;


//                bool nextStep = false;
//                //全杀了。
//                if (kill)
//                {
//                    try
//                    {
//                        winCount = BaseCathectic.CreateCathectic(ReflectClass).IsWin(BetNo, numbers);
//                    }
//                    catch (Exception e)
//                    {
//                        continue;
//                    }
//                    nextStep = winCount == 0 ? true : false;
//                }


//                if (nextStep || !kill)
//                {

//                    var lottery = (from l in Context.LotteryTypes where l.LotteryTypeId == lotteryTypeId select l).First();
//                    var openTime = LotteryOpenTimeServer.GetBetTime(lottery.LotteryTypeId, entity.Number);
//                    if (openTime == null)
//                    {
//                        return new ServiceResult(ServiceResultCode.Error, "无法找到开奖信息");
//                    }

//                    if (!BaseGenerate.CheckData(lottery.LotteryTypeId, data))
//                    {
//                        return new ServiceResult(ServiceResultCode.Error, "开奖号码格式错误，请重新输入");
//                    }


//                    lisCache.Add(data);
//                    var exist = from d in Context.LotteryDatas
//                                where d.LotteryTypeId == lotteryTypeId && d.Number == entity.Number
//                                select d;
//                    if (!exist.Any())
//                    {
//                        var openDataEntity = new LotteryData()
//                        {
//                            LotteryTypeId = lotteryTypeId,
//                            Data = data,
//                            Number = entity.Number,
//                            Time = openTime.OpenTime
//                        };
//                        if (openTime.OpenTime > DateTime.Now)
//                        {
//                            openDataEntity.IsEnable = false;
//                        }
//                        Context.LotteryDatas.Add(openDataEntity);
//                    }
//                    else
//                    {

//                        exist.FirstOrDefault().Data = data;
//                    }
//                    Context.SaveChanges();
//                    return new ServiceResult(ServiceResultCode.Success, data);
//                }

//            }
//            return new ServiceResult(ServiceResultCode.Error, "已用光可能性");


//        }
//        public override BaseFieldAttribute[] GetQueryConditionses()
//        {
//            return new BaseFieldAttribute[]
//            {
//                new TextFieldAttribute("Order", "订单号"), new TextFieldAttribute("Name", "投注用户"),
//                new TextFieldAttribute("Type", "彩种"), new TextFieldAttribute("PlayType", "玩法"),
//             new DropListFieldAttribute("status", "状态",ChasingStatus.Doing.GetDropListModels("全部")),
//               new DropListFieldAttribute("IsShowTestPlayer", "是否显示未统计玩家", TrueFalseEnum.True.GetDropListModels())

//            };
//        }

//    }
}