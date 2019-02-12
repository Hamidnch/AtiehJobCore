using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Domain.Catalog;
using AtiehJobCore.Core.Domain.Messages;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Localization;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Messages
{
    public partial class MessageTemplateService : IMessageTemplateService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string MessageTemplatesAllKey = "AtiehJob.messagetemplate.all";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : template name
        /// </remarks>
        private const string MessageTemplatesByNameKey = "AtiehJob.messagetemplate.name-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string MessageTemplatesPatternKey = "AtiehJob.messagetemplate.";

        #endregion

        #region Fields

        private readonly IRepository<MessageTemplate> _messageTemplateRepository;
        private readonly ILanguageService _languageService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="languageService">Language service</param>
        /// <param name="messageTemplateRepository">Message template repository</param>
        /// <param name="catalogSettings">Catalog settings</param>
        /// <param name="eventPublisher">Event published</param>
        public MessageTemplateService(ICacheManager cacheManager,
            ILanguageService languageService,
            IRepository<MessageTemplate> messageTemplateRepository,
            CatalogSettings catalogSettings,
            IEventPublisher eventPublisher)
        {
            this._cacheManager = cacheManager;
            this._languageService = languageService;
            this._messageTemplateRepository = messageTemplateRepository;
            this._catalogSettings = catalogSettings;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Delete a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void DeleteMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException(nameof(messageTemplate));

            _messageTemplateRepository.Delete(messageTemplate);

            _cacheManager.RemoveByPattern(MessageTemplatesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(messageTemplate);
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void InsertMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException(nameof(messageTemplate));

            _messageTemplateRepository.Insert(messageTemplate);

            _cacheManager.RemoveByPattern(MessageTemplatesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(messageTemplate);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void UpdateMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException(nameof(messageTemplate));

            _messageTemplateRepository.Update(messageTemplate);

            _cacheManager.RemoveByPattern(MessageTemplatesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(messageTemplate);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a message template
        /// </summary>
        /// <param name="messageTemplateId">Message template identifier</param>
        /// <returns>Message template</returns>
        public virtual MessageTemplate GetMessageTemplateById(string messageTemplateId)
        {
            return _messageTemplateRepository.GetById(messageTemplateId);
        }

        /// <summary>
        /// Gets a message template
        /// </summary>
        /// <param name="messageTemplateName">Message template name</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Message template</returns>
        public virtual MessageTemplate GetMessageTemplateByName(string messageTemplateName)
        {
            if (string.IsNullOrWhiteSpace(messageTemplateName))
                throw new ArgumentException("messageTemplateName");

            var key = string.Format(MessageTemplatesByNameKey, messageTemplateName);
            return _cacheManager.Get(key, () =>
            {
                var query = _messageTemplateRepository.Table;

                query = query.Where(t => t.Name == messageTemplateName);
                query = query.OrderBy(t => t.Id);
                var templates = query.ToList();

                return templates.FirstOrDefault();
            });

        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all message templates
        /// </summary>
        /// <returns>Message template list</returns>
        public virtual IList<MessageTemplate> GetAllMessageTemplates()
        {
            var key = string.Format(MessageTemplatesAllKey);
            return _cacheManager.Get(key, () =>
            {
                var query = _messageTemplateRepository.Table;

                query = query.OrderBy(t => t.Name);

                return query.ToList();
            });
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a copy of message template with all depended data
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        /// <returns>Message template copy</returns>
        public virtual MessageTemplate CopyMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException(nameof(messageTemplate));

            var mtCopy = new MessageTemplate
            {
                Name = messageTemplate.Name,
                BccEmailAddresses = messageTemplate.BccEmailAddresses,
                Subject = messageTemplate.Subject,
                Body = messageTemplate.Body,
                IsActive = messageTemplate.IsActive,
                AttachedDownloadId = messageTemplate.AttachedDownloadId,
                EmailAccountId = messageTemplate.EmailAccountId,
                LimitedToStores = messageTemplate.LimitedToStores,
                Locales = messageTemplate.Locales,
                Stores = messageTemplate.Stores,
                DelayBeforeSend = messageTemplate.DelayBeforeSend,
                DelayPeriod = messageTemplate.DelayPeriod
            };

            InsertMessageTemplate(mtCopy);

            return mtCopy;
        }

        #endregion
    }
}
