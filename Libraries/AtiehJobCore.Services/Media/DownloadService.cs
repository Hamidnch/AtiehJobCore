using AtiehJobCore.Core.Domain.Media;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;

namespace AtiehJobCore.Services.Media
{
    /// <inheritdoc />
    /// <summary>
    /// Download service
    /// </summary>
    public partial class DownloadService : IDownloadService
    {
        #region Fields

        private readonly IRepository<Download> _downloadRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="downloadRepository">Download repository</param>
        /// <param name="eventPublisher"></param>
        public DownloadService(IRepository<Download> downloadRepository,
            IEventPublisher eventPublisher)
        {
            _downloadRepository = downloadRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Gets a download
        /// </summary>
        /// <param name="downloadId">Download identifier</param>
        /// <returns>Download</returns>
        public virtual Download GetDownloadById(string downloadId)
        {
            if (string.IsNullOrEmpty(downloadId))
                return null;

            var download = _downloadRepository.GetById(downloadId);
            if (!download.UseDownloadUrl)
                download.DownloadBinary = DownloadAsBytes(download.DownloadObjectId);

            return download;
        }

        protected virtual byte[] DownloadAsBytes(ObjectId objectId)
        {
            var bucket = new MongoDB.Driver.GridFS.GridFSBucket(_downloadRepository.Database);
            var binary = bucket.DownloadAsBytesAsync(objectId,
                new MongoDB.Driver.GridFS.GridFSDownloadOptions() { CheckMD5 = true, Seekable = true }).Result;
            return binary;
        }
        /// <inheritdoc />
        /// <summary>
        /// Gets a download by GUID
        /// </summary>
        /// <param name="downloadGuid">Download GUID</param>
        /// <returns>Download</returns>
        public virtual Download GetDownloadByGuid(Guid downloadGuid)
        {
            if (downloadGuid == Guid.Empty)
                return null;

            var query = from o in _downloadRepository.Table
                        where o.DownloadGuid == downloadGuid
                        select o;
            var order = query.FirstOrDefault();
            if (!order.UseDownloadUrl)
                order.DownloadBinary = DownloadAsBytes(order.DownloadObjectId);

            return order;
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes a download
        /// </summary>
        /// <param name="download">Download</param>
        public virtual void DeleteDownload(Download download)
        {
            if (download == null)
                throw new ArgumentNullException(nameof(download));

            _downloadRepository.Delete(download);

            _eventPublisher.EntityDeleted(download);
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a download
        /// </summary>
        /// <param name="download">Download</param>
        public virtual void InsertDownload(Download download)
        {
            if (download == null)
                throw new ArgumentNullException(nameof(download));
            if (!download.UseDownloadUrl)
            {
                var bucket = new MongoDB.Driver.GridFS.GridFSBucket(_downloadRepository.Database);
                var id = bucket.UploadFromBytesAsync(download.Filename, download.DownloadBinary).Result;
                download.DownloadObjectId = id;
            }

            download.DownloadBinary = null;
            _downloadRepository.Insert(download);

            _eventPublisher.EntityInserted(download);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the download
        /// </summary>
        /// <param name="download">Download</param>
        public virtual void UpdateDownload(Download download)
        {
            if (download == null)
                throw new ArgumentNullException(nameof(download));

            _downloadRepository.Update(download);

            _eventPublisher.EntityUpdated(download);
        }
        #endregion
    }
}
