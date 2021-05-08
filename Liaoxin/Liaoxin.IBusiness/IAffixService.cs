using System;

namespace Liaoxin.IBusiness
{
    public interface IAffixService
    {
        byte[] GetAffix(Guid id);
    }
}