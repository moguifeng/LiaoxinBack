using System;
using System.Collections.Generic;
using System.Text;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 群管理员
    /// </summary>
    public class GroupManager : BaseModel
    {
        public Guid GroupManagerId { get; set; } = Guid.NewGuid();

        public Guid GroupId { get; set; }

        public virtual Group Group { get; set; }

        public Guid ClientId { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public virtual Client Client{get;set;}


    }
}
