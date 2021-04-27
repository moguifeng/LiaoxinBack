using System.Collections.Generic;

namespace Zzb.EF
{
    public class UserInfo : BaseModel
    {
        public int UserInfoId { get; set; }

        [ZzbIndex(IsUnique = true)]
        public string Name { get; set; }

        public string Password { get; set; }

        public virtual List<UserInfoPermission> UserInfoPermissions { get; set; }

        public virtual List<UserInfoRole> UserInfoRoles { get; set; }
    }
}