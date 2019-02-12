using AtiehJobCore.Core.Domain.Messages;

namespace AtiehJobCore.Services.Messages
{
    public partial interface IPopupService
    {
        /// <summary>
        /// Inserts a popup
        /// </summary>
        /// <param name="popup"></param>        
        void InsertPopupActive(PopupActive popup);
        /// <summary>
        /// Gets active banner for user
        /// </summary>
        /// <returns>BannerActive</returns>
        PopupActive GetActivePopupByUserId(string userId);

        /// <summary>
        /// Move popup to archive
        /// </summary>
        void MovePopupToArchive(string id, string userId);

    }
}
