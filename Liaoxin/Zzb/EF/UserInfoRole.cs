namespace Zzb.EF
{
    public class UserInfoRole
    {
        public int UserInfoId { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}