using System.Collections.Generic;

namespace Zzb.EF
{
    public class Role : BaseModel
    {
        public int RoleId { get; set; }

        [ZzbIndex(IsUnique = true)]
        public string Name { get; set; }

        public virtual List<UserInfoRole> UserInfoRoles { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }
    }
}