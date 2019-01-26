using AtiehJobCore.Domain.Entities.Identity.Plus;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using Microsoft.EntityFrameworkCore;

namespace AtiehJobCore.Data.Mappings.Identity.Plus
{
    public static class SqlCacheConfig
    {
        public static void AddSqlCacheConfig(this ModelBuilder modelBuilder, SiteSettings siteSettings)
        {
            modelBuilder.Entity<SqlCache>(builder =>
            {
                // For Microsoft.Extensions.Caching.SqlServer
                var cacheOptions = siteSettings.CookieOptions.DistributedSqlServerCacheOptions;
                builder.HasIndex(e => e.ExpiresAtTime).HasName("Index_ExpiresAtTime");
                builder.Property(e => e.Id).HasMaxLength(449);
                builder.Property(e => e.Value).IsRequired();

                builder.ToTable(cacheOptions.TableName, cacheOptions.SchemaName);
            });

            //modelBuilder.Entity<RoleClaim>(builder =>
            //{
            //    builder.HasOne(rc => rc.Role)
            //        .WithMany(r => r.RoleClaims)
            //        .HasForeignKey(rc => rc.RoleId);

            //    builder.ToTable("RoleClaims");
            //});

            //modelBuilder.Entity<Role>(builder =>
            //{
            //    builder.ToTable("Roles");
            //});

            //modelBuilder.Entity<UserClaim>(builder =>
            //{
            //    builder.HasOne(uc => uc.User)
            //        .WithMany(u => u.UserClaims)
            //        .HasForeignKey(uc => uc.UserId);

            //    builder.ToTable("UserClaims");
            //});

            //modelBuilder.Entity<UserLogin>(builder =>
            //{
            //    builder.HasOne(ul => ul.User)
            //        .WithMany(u => u.UserLogins)
            //        .HasForeignKey(ul => ul.UserId);

            //    builder.ToTable("UserLogins");
            //});

            //modelBuilder.Entity<User>(builder =>
            //{
            //    builder.ToTable("Users");
            //});

            //modelBuilder.Entity<UserRole>(builder =>
            //{
            //    builder.HasOne(ur => ur.Role)
            //        .WithMany(r => r.UserRoles)
            //        .HasForeignKey(ur => ur.RoleId);

            //    builder.HasOne(ur => ur.User)
            //        .WithMany(u => u.UserRoles)
            //        .HasForeignKey(ur => ur.UserId);

            //    builder.ToTable("UserRoles");
            //});

            //modelBuilder.Entity<UserToken>(builder =>
            //{
            //    builder.HasOne(ut => ut.User)
            //        .WithMany(u => u.UserTokens)
            //        .HasForeignKey(ut => ut.UserId);

            //    builder.ToTable("UserTokens");
            //});

            //modelBuilder.Entity<UserUsedPassword>(builder =>
            //{
            //    builder.Property(up => up.HashedPassword)
            //        .HasMaxLength(450)
            //        .IsRequired();
            //    builder.HasOne(up => up.User)
            //        .WithMany(u => u.UserUsedPasswords);

            //    builder.ToTable("UserUsedPasswords");
            //});
        }
    }
}