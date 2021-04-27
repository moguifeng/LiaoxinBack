﻿namespace Zzb.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Used on an EntityFramework Entity class to mark a property to be used as a Unique Key
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class ZzbIndexAttribute : ValidationAttribute
    {
        /// <summary>
        /// Marker attribute for unique key
        /// </summary>
        /// <param name="groupId">Optional, used to group multiple entity properties together into a combined Unique Key</param>
        /// <param name="order">Optional, used to order the entity properties that are part of a combined Unique Key</param>
        public ZzbIndexAttribute(string groupId = null, int order = 0)
        {
            GroupId = groupId;
            Order = order;
        }

        public string GroupId { get; set; }
        public int Order { get; set; }

        public bool IsUnique { get; set; }
    }
}