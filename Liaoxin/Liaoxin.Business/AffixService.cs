using Liaoxin.IBusiness;
using System;
using System.IO;
using System.Linq;
using Zzb.Context;

namespace Liaoxin.Business
{
    public class AffixService : BaseService, IAffixService
    {

        public byte[] GetAffix(Guid id)
        {
            var affix = (from a in Context.Affixs where a.AffixId == id select a).FirstOrDefault();

            if (affix == null)
            {
                return null;
            }
            if (affix.ClientId.HasValue)
            {
                if (UserContext.Current.Id != affix.ClientId)
                {
                    return null;
                }
            }

            if (File.Exists(Path.Combine(HostingEnvironment.ContentRootPath, affix.Path)))
            {
                return File.ReadAllBytes(Path.Combine(HostingEnvironment.ContentRootPath, affix.Path));
            }
            return null;
        }
    }
}