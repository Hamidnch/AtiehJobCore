using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Data.Extensions;
using AtiehJobCore.Data.Mappings.Identity.Plus;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Domain.Entities.Identity.Plus;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AtiehJobCore.Data.DbContext
{
    public class AtiehJobCoreDbContext : IdentityDbContext<User, Role, int,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {
        public AtiehJobCoreDbContext(DbContextOptions<AtiehJobCoreDbContext> options) : base(options)
        { }

        #region DbSet

        public virtual DbSet<LogItem> LogItems { get; set; }
        public virtual DbSet<SqlCache> SqlCaches { get; set; }
        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public virtual DbSet<UserUsedPassword> UserUsedPasswords { get; set; }

        #endregion DbSet

        #region IUnitOfWork

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public async void AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            await Set<TEntity>().AddRangeAsync(entities);
        }
        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return Entry(entity).Property(propertyName).CurrentValue;
        }

        public void ExecuteSqlCommand(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        public async void ExecuteSqlCommandAsync(string query)
        {
            await Database.ExecuteSqlCommandAsync(query);
        }
        public void ExecuteSqlCommand(string query, params object[] parameters)
        {
            Database.ExecuteSqlCommand(query, parameters);
        }
        public async void ExecuteSqlCommandAsync(string query, params object[] parameters)
        {
            await Database.ExecuteSqlCommandAsync(query, parameters);
        }

        #region Override

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            ValidateEntities();
            SetShadowProperties();
            this.ApplyCorrectYeKe();

            // for performance reasons, to avoid calling DetectChanges() again.
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            ValidateEntities();
            SetShadowProperties();
            this.ApplyCorrectYeKe();

            // for performance reasons, to avoid calling DetectChanges() again.
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            ValidateEntities();
            SetShadowProperties();
            this.ApplyCorrectYeKe();

            // for performance reasons, to avoid calling DetectChanges() again.
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChangesAsync(cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            ValidateEntities();
            SetShadowProperties();
            this.ApplyCorrectYeKe();

            // for performance reasons, to avoid calling DetectChanges() again.
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }
        #endregion Override

        #region Private Methods
        private void ValidateEntities()
        {
            var errors = this.GetValidationErrors();

            if (string.IsNullOrWhiteSpace(errors)) return;

            // we can't use constructor injection anymore,
            // because we are using the `AddDbContextPool<>`
            var loggerFactory = this.GetService<ILoggerFactory>();
            loggerFactory.CheckArgumentIsNull(nameof(loggerFactory));
            var logger = loggerFactory.CreateLogger<AtiehJobCoreDbContext>();
            logger.LogError(errors);
            throw new InvalidOperationException(errors);
        }
        private void SetShadowProperties()
        {
            // we can't use constructor injection anymore,
            // because we are using the `AddDbContextPool<>`
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();
            httpContextAccessor.CheckArgumentIsNull(nameof(httpContextAccessor));
            ChangeTracker.SetAuditableEntityPropertyValues(httpContextAccessor);
        }

        #endregion Private Methods

        #endregion IUnitOfWork

        #region Override OnModelCreationg

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
            var siteSettings = this.GetService<IOptionsSnapshot<SiteSettings>>();
            siteSettings.CheckArgumentIsNull(nameof(siteSettings));
            siteSettings.Value.CheckArgumentIsNull(nameof(siteSettings.Value));

            // Adds all of the ASP.NET Core Identity related mappings at once.
            builder.ApplyConfigurationsFromAssembly(typeof(AtiehJobCoreDbContext).Assembly);

            builder.AddSqlCacheConfig(siteSettings.Value);

            builder.AddAuditableShadowProperties();
        }

        #endregion Override OnModelCreationg
    }
}