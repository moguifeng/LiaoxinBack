using System.Collections.Generic;

namespace Zzb.EF
{
    public class Permission : BaseModel
    {
        public int PermissionId { get; set; }

        /// <summary>
        /// 基础数据的NavId
        /// </summary>
        public string NavId { get; set; }

        public virtual List<UserInfoPermission> UserInfoPermissions { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }
    }
}