using Liaoxin.Model;
using System.Collections.Generic;

namespace Liaoxin.IBusiness
{
    public interface IBaseService
    {
        int Update<T>(T t, string keyName, IList<string> updateFieldList) where T : class;
    }
}