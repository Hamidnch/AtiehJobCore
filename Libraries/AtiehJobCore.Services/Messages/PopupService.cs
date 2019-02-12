using AtiehJobCore.Core.Domain.Messages;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;

namespace AtiehJobCore.Services.Messages
{
    public partial class PopupService : IPopupService
    {
        private readonly IRepository<PopupActive> _popupActiveRepository;
        private readonly IRepository<PopupArchive> _popupArchiveRepository;
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="popupActiveRepository">Popup Active repository</param>
        /// <param name="popupArchiveRepository">Popup Archive repository</param>
        /// <param name="eventPublisher">Event published</param>
        public PopupService(IRepository<PopupActive> popupActiveRepository,
            IRepository<PopupArchive> popupArchiveRepository,
            IEventPublisher eventPublisher)
        {
            this._popupActiveRepository = popupActiveRepository;
            this._popupArchiveRepository = popupArchiveRepository;
            this._eventPublisher = eventPublisher;
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a popup
        /// </summary>
        /// <param name="popup">Popup</param>        
        public virtual void InsertPopupActive(PopupActive popup)
        {
            if (popup == null)
                throw new ArgumentNullException(nameof(popup));

            _popupActiveRepository.Insert(popup);

            //event notification
            _eventPublisher.EntityInserted(popup);
        }


        /// <summary>
        /// Gets a popup by identifier
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Banner</returns>
        public virtual PopupActive GetActivePopupByUserId(string userId)
        {
            var query = from c in _popupActiveRepository.Table
                        where c.UserId == userId
                        orderby c.CreatedOnUtc
                        select c;
            var popup = query.FirstOrDefault();
            return popup;

        }

        public virtual void MovePopupToArchive(string id, string userId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(id))
                return;

            var query = from c in _popupActiveRepository.Table
                        where c.UserId == userId && c.Id == id
                        select c;
            var popup = query.FirstOrDefault();
            if (popup == null)
            {
                return;
            }

            var archiveBanner = new PopupArchive()
            {
                Body = popup.Body,
                BaCreatedOnUtc = popup.CreatedOnUtc,
                CreatedOnUtc = DateTime.UtcNow,
                UserActionId = popup.UserActionId,
                UserId = popup.UserId,
                PopupActiveId = popup.Id,
                PopupTypeId = popup.PopupTypeId,
                Name = popup.Name,
            };
            _popupArchiveRepository.Insert(archiveBanner);
            _popupActiveRepository.Delete(popup);

        }
    }
}
