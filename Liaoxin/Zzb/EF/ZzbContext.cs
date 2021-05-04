using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Zzb.EF
{
    public class ZzbDbContext : DbContext
    {
        //static ZzbDbContext()
        //{
        //    var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        //    path = Path.Combine(path, @"ConnectString.txt");
        //    if (File.Exists(path))
        //    {
        //        DefaultString = File.ReadAllText(path);
        //    }
        //}

        public static string ConnectString;

        public virtual string DefaultString =>
            //"server=103.193.174.33;port=3306;database=Liaoxin;user=root;password=60e7df86753874a2;TreatTinyAsBoolean=true";
            "server=8.129.61.181;port=3306;database=Liaoxin;user=zzb;password=86809223;TreatTinyAsBoolean=true";
        //"server=localhost;port=3306;database=Liaoxin;user=root;password=86833695;TreatTinyAsBoolean=true";

        public ZzbDbContext()
        {

        }

        public ZzbDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 添加索引

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                #region Convert UniqueKeyAttribute on Entities to UniqueKey in DB
                var properties = entityType.GetProperties();
                if ((properties != null) && (properties.Any()))
                {
                    foreach (var property in properties)
                    {
                        var uniqueKeys = GetUniqueKeyAttributes(entityType, property);
                        if (uniqueKeys != null)
                        {
                            foreach (var uniqueKey in uniqueKeys.Where(x => x.Order == 0))
                            {
                                // Single column Unique Key
                                if (String.IsNullOrWhiteSpace(uniqueKey.GroupId))
                                {
                                    var index = entityType.FindIndex(property);
                                    if (index != null)
                                    {
                                        index.IsUnique = uniqueKey.IsUnique;
                                    }
                                    else
                                    {
                                        entityType.AddIndex(property).IsUnique = uniqueKey.IsUnique;
                                    }
                                }
                                // Multiple column Unique Key
                                else
                                {
                                    var mutableProperties = new List<IMutableProperty>();
                                    properties.ToList().ForEach(x =>
                                    {
                                        var uks = GetUniqueKeyAttributes(entityType, x);
                                        if (uks != null)
                                        {
                                            foreach (var uk in uks)
                                            {
                                                if ((uk != null) && (uk.GroupId == uniqueKey.GroupId))
                                                {
                                                    mutableProperties.Add(x);
                                                }
                                            }
                                        }
                                    });
                                    entityType.AddIndex(mutableProperties).IsUnique = uniqueKey.IsUnique;
                                }
                            }
                        }
                    }
                }
                #endregion Convert UniqueKeyAttribute on Entities to UniqueKey in DB
            }

            #endregion

            #region decimal精度变高

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18, 2)";
            }

            #endregion


            modelBuilder.Entity<UserInfoPermission>().HasKey(t => new { t.PermissionId, t.UserInfoId });
            modelBuilder.Entity<UserInfoRole>().HasKey(t => new { t.RoleId, t.UserInfoId });
            modelBuilder.Entity<RolePermission>().HasKey(t => new { t.PermissionId, t.RoleId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(ConnectString))
            {
                var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                path = Path.Combine(path, "connectString.txt");
                if (File.Exists(path))
                {

                    //StreamReader reader = new FileInfo(path).OpenText();
                    string connect = File.ReadAllText(path);
                    if (!string.IsNullOrEmpty(connect))
                    {
                        ConnectString = connect;
                    }
                }

                if (string.IsNullOrEmpty(ConnectString))
                {
                    ConnectString = DefaultString;
                }
            }
            optionsBuilder.UseLazyLoadingProxies().UseMySql(ConnectString);
            base.OnConfiguring(optionsBuilder);
        }

        private static IEnumerable<ZzbIndexAttribute> GetUniqueKeyAttributes(IMutableEntityType entityType, IMutableProperty property)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException(nameof(entityType));
            }
            else if (entityType.ClrType == null)
            {
                throw new ArgumentNullException(nameof(entityType.ClrType));
            }
            else if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            else if (property.Name == null)
            {
                throw new ArgumentNullException(nameof(property.Name));
            }
            var propInfo = entityType.ClrType.GetProperty(
                property.Name);
            if (propInfo == null)
            {
                return null;
            }
            return propInfo.GetCustomAttributes<ZzbIndexAttribute>();
        }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserInfoPermission> UserInfoPermissions { get; set; }

        public DbSet<UserInfoRole> UserInfoRoles { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Affix> Affixs { get; set; }
    }
}