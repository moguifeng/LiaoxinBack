using AllLottery.Model;
using Microsoft.AspNetCore.Http;
using Zzb.BaseData.Model;

namespace AllLottery.BaseDataModel
{
    public abstract class BaseServiceModal : BaseModal
    {
        protected BaseServiceModal()
        {
        }

        protected BaseServiceModal(string id, string name) : base(id, name)
        {
        }

        public LotteryContext Context { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }
    }
}