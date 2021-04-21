using AllLottery.Model;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute.Field;

namespace AllLottery.BaseDataModel
{
    public class LotteryTypeDropListFieldAttribute : DropListFieldAttribute
    {
        public LotteryTypeDropListFieldAttribute()
        {
        }

        public LotteryTypeDropListFieldAttribute(string name) : base(name)
        {
        }

        public LotteryTypeDropListFieldAttribute(string id, string name) : base(id, name)
        {
        }

        public bool IsSystem { get; set; } = false;

        public override List<DropListModel> Source
        {
            get
            {
                using (var context = LotteryContext.CreateContext())
                {
                    List<DropListModel> list = new List<DropListModel>();
                    foreach (LotteryType type in context.LotteryTypes.OrderBy(t => t.SortIndex))
                    {
                        if (IsSystem)
                        {
                            if (type.CalType == LotteryCalNumberTypeEnum.Automatic)
                            {
                                list.Add(new DropListModel(type.LotteryTypeId.ToString(), type.Name));
                            }
                        }
                        else
                        {
                            list.Add(new DropListModel(type.LotteryTypeId.ToString(), type.Name));
                        }
                    }
                    return list;
                }
            }
            set { }
        }
    }
}