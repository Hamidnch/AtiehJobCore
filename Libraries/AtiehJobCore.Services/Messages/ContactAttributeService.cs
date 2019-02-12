using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Catalog;
using AtiehJobCore.Core.Domain.Messages;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Extensions;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Messages
{
    /// <inheritdoc />
    /// <summary>
    /// Contact attribute service
    /// </summary>
    public partial class ContactAttributeService : IContactAttributeService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : store ID
        /// {1} : ignore ACL?
        /// </remarks>
        private const string ContactAttributesAllKey = "AtiehJob.contactattribute.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : contact attribute ID
        /// </remarks>
        private const string ContactAttributesByIdKey = "AtiehJob.contactattribute.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : contact attribute ID
        /// </remarks>
        private const string ContactAttributeValuesAllKey = "AtiehJob.contactattributevalue.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : contact attribute value ID
        /// </remarks>
        private const string ContactAttributeValuesByIdKey = "AtiehJob.contactattributevalue.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ContactAttributesPatternKey = "AtiehJob.contactattribute.";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ContactAttributeValuesPatternKey = "AtiehJob.contactattributevalue.";
        #endregion

        #region Fields

        private readonly IRepository<ContactAttribute> _contactAttributeRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly CatalogSettings _catalogSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="contactAttributeRepository">Contact attribute repository</param>
        /// <param name="eventPublisher">Event published</param>
        /// <param name="workContext"></param>
        /// <param name="catalogSettings"></param>
        public ContactAttributeService(ICacheManager cacheManager,
            IRepository<ContactAttribute> contactAttributeRepository,
            IEventPublisher eventPublisher,
            IWorkContext workContext,
            CatalogSettings catalogSettings)
        {
            this._cacheManager = cacheManager;
            this._contactAttributeRepository = contactAttributeRepository;
            this._eventPublisher = eventPublisher;
            this._workContext = workContext;
            this._catalogSettings = catalogSettings;
        }

        #endregion

        #region Methods

        #region Contact attributes

        /// <inheritdoc />
        /// <summary>
        /// Deletes a contact attribute
        /// </summary>
        /// <param name="contactAttribute">Contact attribute</param>
        public virtual void DeleteContactAttribute(ContactAttribute contactAttribute)
        {
            if (contactAttribute == null)
                throw new ArgumentNullException(nameof(contactAttribute));

            _contactAttributeRepository.Delete(contactAttribute);

            _cacheManager.RemoveByPattern(ContactAttributesPatternKey);
            _cacheManager.RemoveByPattern(ContactAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(contactAttribute);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all contact attributes
        /// </summary>
        /// <returns>Contact attributes</returns>
        public virtual IList<ContactAttribute> GetAllContactAttributes(bool ignoreAcl = false)
        {
            var key = string.Format(ContactAttributesAllKey, ignoreAcl);
            return _cacheManager.Get(key, () =>
            {
                var query = _contactAttributeRepository.Table;
                query = query.OrderBy(c => c.DisplayOrder);

                if ((ignoreAcl || _catalogSettings.IgnoreAcl))
                {
                    return query.ToList();
                }

                if (_catalogSettings.IgnoreAcl)
                {
                    return query.ToList();
                }

                var allowedCustomerRolesIds = _workContext.CurrentUser.GetUserRoleIds();
                query = from p in query
                        where !p.SubjectToAcl || allowedCustomerRolesIds.Any(x => p.UserRoles.Contains(x))
                        select p;
                return query.ToList();

            });
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a contact attribute 
        /// </summary>
        /// <param name="contactAttributeId">Contact attribute identifier</param>
        /// <returns>Contact attribute</returns>
        public virtual ContactAttribute GetContactAttributeById(string contactAttributeId)
        {
            var key = string.Format(ContactAttributesByIdKey, contactAttributeId);
            return _cacheManager.Get(key, () => _contactAttributeRepository.GetById(contactAttributeId));
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a contact attribute
        /// </summary>
        /// <param name="contactAttribute">Contact attribute</param>
        public virtual void InsertContactAttribute(ContactAttribute contactAttribute)
        {
            if (contactAttribute == null)
                throw new ArgumentNullException(nameof(contactAttribute));

            _contactAttributeRepository.Insert(contactAttribute);

            _cacheManager.RemoveByPattern(ContactAttributesPatternKey);
            _cacheManager.RemoveByPattern(ContactAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(contactAttribute);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the contact attribute
        /// </summary>
        /// <param name="contactAttribute">Contact attribute</param>
        public virtual void UpdateContactAttribute(ContactAttribute contactAttribute)
        {
            if (contactAttribute == null)
                throw new ArgumentNullException(nameof(contactAttribute));

            _contactAttributeRepository.Update(contactAttribute);

            _cacheManager.RemoveByPattern(ContactAttributesPatternKey);
            _cacheManager.RemoveByPattern(ContactAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(contactAttribute);
        }

        #endregion

        #endregion
    }
}
