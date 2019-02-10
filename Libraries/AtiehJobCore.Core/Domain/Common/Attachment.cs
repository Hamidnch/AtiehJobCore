using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Common
{
    public class Attachment : BaseMongoEntity
    {
        public Attachment() { }

        public Attachment(string fileName, string contentType, long? size,
            string extensions, long? downloadsCount, int userId)
        {
            FileName = fileName;
            ContentType = contentType;
            Size = size;
            Extensions = extensions;
            DownloadsCount = downloadsCount;
            UserId = userId;
        }

        #region Properties
        /// <summary>
        /// نام فایل
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// نوع محتوای فایل
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// اندازه فایل برحسب بایت
        /// </summary>
        public long? Size { get; set; }
        /// <summary>
        /// پسوند فایل
        /// </summary>
        public string Extensions { get; set; }
        /// <summary>
        /// تعداد دانلود شدن فایل
        /// </summary>
        public long? DownloadsCount { get; set; }

        #endregion

        #region NavigationProperties
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public int UserId { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
