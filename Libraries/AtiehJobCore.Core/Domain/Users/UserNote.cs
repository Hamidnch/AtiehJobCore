using System;
using System.Collections.Generic;
using System.Text;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Users
{
    /// <summary>
    /// Represents an uer note
    /// </summary>
    public partial class UserNote : BaseMongoEntity
    {
        /// <summary>
        /// Gets or sets the uer identifier
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the attached file (download) identifier
        /// </summary>
        public string DownloadId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a uer can see a note
        /// </summary>
        public bool DisplayToUser { get; set; }

        /// <summary>
        /// Gets or sets the date and time of order note creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}
