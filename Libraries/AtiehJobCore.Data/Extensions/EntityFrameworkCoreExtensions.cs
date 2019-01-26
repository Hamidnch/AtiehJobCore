using AtiehJobCore.Common;
using AtiehJobCore.Common.Constants;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Extensions;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Reflection;

namespace AtiehJobCore.Data.Extensions
{
    public static class EntityFrameworkCoreExtensions
    {
        public static void ApplyCorrectYeKe(this Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            if (dbContext == null) return;

            //پیدا کردن موجودیت‌های تغییر کرده
            var changedEntities = dbContext.ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
                var entity = item.Entity;
                if (item.Entity == null)
                {
                    continue;
                }

                //یافتن خواص قابل تنظیم و رشته‌ای این موجودیت‌ها
                var propertyInfos = entity.GetType().GetProperties(
                    BindingFlags.Public | BindingFlags.Instance
                ).Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                var reflections = new Reflections();

                //اعمال یکپارچگی نهایی
                foreach (var propertyInfo in propertyInfos)
                {
                    var propName = propertyInfo.Name;
                    var value = reflections.GetValue(entity, propName);
                    if (value == null) continue;
                    var strValue = value.ToString();
                    var newVal = strValue.ApplyCorrectYeKe();
                    if (newVal == strValue)
                    {
                        continue;
                    }
                    reflections.SetValue(entity, propName, newVal);
                }
            }
        }
        public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                .GetEntityTypes()
                .Where(e => typeof(IAuditableEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>(Properties.CreatedByBrowserName).HasMaxLength(1000);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>(Properties.ModifiedByBrowserName).HasMaxLength(1000);

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>(Properties.CreatedByIp).HasMaxLength(255);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>(Properties.ModifiedByIp).HasMaxLength(255);

                modelBuilder.Entity(entityType.ClrType)
                    .Property<int?>(Properties.CreatedByUserId);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<int?>(Properties.ModifiedByUserId);

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTimeOffset?>(Properties.CreatedDateTime);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTimeOffset?>(Properties.ModifiedDateTime);

            }
        }
        public static void SetAuditableEntityPropertyValues(
            this ChangeTracker changeTracker, IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor?.HttpContext;
            var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString();
            var userIp = httpContext?.Connection?.RemoteIpAddress?.ToString();
            var now = DateTimeOffset.UtcNow;
            var userId = GetUserId(httpContext);

            var modifiedEntries = changeTracker.Entries<IAuditableEntity>()
                                               .Where(x => x.State == EntityState.Modified);
            foreach (var modifiedEntry in modifiedEntries)
            {
                modifiedEntry.Property(Properties.ModifiedDateTime).CurrentValue = now;
                modifiedEntry.Property(Properties.ModifiedByBrowserName).CurrentValue = userAgent;
                modifiedEntry.Property(Properties.ModifiedByIp).CurrentValue = userIp;
                modifiedEntry.Property(Properties.ModifiedByUserId).CurrentValue = userId;
            }

            var addedEntries = changeTracker.Entries<IAuditableEntity>()
                                            .Where(x => x.State == EntityState.Added);
            foreach (var addedEntry in addedEntries)
            {
                addedEntry.Property(Properties.CreatedDateTime).CurrentValue = now;
                addedEntry.Property(Properties.CreatedByBrowserName).CurrentValue = userAgent;
                addedEntry.Property(Properties.CreatedByIp).CurrentValue = userIp;
                addedEntry.Property(Properties.CreatedByUserId).CurrentValue = userId;
            }
        }
        private static int? GetUserId(HttpContext httpContext)
        {
            int? userId = null;
            var userIdValue = httpContext?.User?.Identity?.GetUserId();
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                userId = int.Parse(userIdValue);
            }
            return userId;
        }
    }
}