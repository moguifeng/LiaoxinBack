using System.ComponentModel.DataAnnotations;

namespace Zzb.EF
{
    public class UserInfoPermission
    {
        public int UserInfoId { get; set; }

        public virtual UserInfo UserInfo { get; set; }
        
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}