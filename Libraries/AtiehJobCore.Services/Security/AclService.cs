using System;
using System.Linq;
using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Catalog;
using AtiehJobCore.Core.Domain.Security;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.MongoDb;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;

namespace AtiehJobCore.Services.Security
{
    /// <inheritdoc />
    /// <summary>
    /// ACL service
    /// </summary>
    public partial class AclService : IAclService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// </remarks>
        private const string AclRecordByEntityIdNameKey = "AtiehJob.aclrecord.entityid-name-{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string AclRecordPatternKey = "AtiehJob.aclrecord.";

        #endregion

        #region Fields

        private readonly IRepository<AclRecord> _aclRecordRepository;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private readonly IEventPublisher _eventPublisher;
        private readonly CatalogSettings _catalogSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="workContext">Work context</param>
        /// <param name="aclRecordRepository">ACL record repository</param>
        /// <param name="catalogSettings">Catalog settings</param>
        /// <param name="eventPublisher">Event publisher</param>
        public AclService(ICacheManager cacheManager,
            IWorkContext workContext,
            IRepository<AclRecord> aclRecordRepository,
            IEventPublisher eventPublisher,
            CatalogSettings catalogSettings)
        {
            _cacheManager = cacheManager;
            _workContext = workContext;
            _aclRecordRepository = aclRecordRepository;
            _eventPublisher = eventPublisher;
            _catalogSettings = catalogSettings;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Deletes an ACL record
        /// </summary>
        /// <param name="aclRecord">ACL record</param>
        public virtual void DeleteAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException(nameof(aclRecord));

            _aclRecordRepository.Delete(aclRecord);

            //cache
            _cacheManager.RemoveByPattern(AclRecordPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(aclRecord);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets an ACL record
        /// </summary>
        /// <param name="aclRecordId">ACL record identifier</param>
        /// <returns>ACL record</returns>
        public virtual AclRecord GetAclRecordById(string aclRecordId)
        {
            return string.IsNullOrEmpty(aclRecordId) ? null : _aclRecordRepository.GetById(aclRecordId);
        }


        /// <inheritdoc />
        /// <summary>
        /// Inserts an ACL record
        /// </summary>
        /// <param name="aclRecord">ACL record</param>
        public virtual void InsertAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException(nameof(aclRecord));

            _aclRecordRepository.Insert(aclRecord);

            //cache
            _cacheManager.RemoveByPattern(AclRecordPatternKey);

            //event notification
            _eventPublisher.EntityInserted(aclRecord);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the ACL record
        /// </summary>
        /// <param name="aclRecord">ACL record</param>
        public virtual void UpdateAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException(nameof(aclRecord));

            _aclRecordRepository.Update(aclRecord);

            //cache
            _cacheManager.RemoveByPattern(AclRecordPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(aclRecord);
        }

        /// <inheritdoc />
        /// <summary>
        /// Authorize ACL permission
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize<T>(T entity) where T : BaseMongoEntity, IAclSupported
        {
            return Authorize(entity, _workContext.CurrentUser);
        }

        /// <inheritdoc />
        /// <summary>
        /// Authorize ACL permission
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="user">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize<T>(T entity, User user) where T : BaseMongoEntity, IAclSupported
        {
            if (entity == null)
                return false;

            if (user == null)
                return false;

            if (!entity.SubjectToAcl)
                return true;

            if (_catalogSettings.IgnoreAcl)
                return true;

            return user.Roles.Where(cr => cr.Active).Any(role1 => entity.UserRoles.Any(role2Id => role1.Id == role2Id));

            //no permission found
        }
        #endregion
    }
}
