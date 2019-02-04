﻿using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Users
{
    /// <summary>
    /// Change password result
    /// </summary>
    public class ChangePasswordResult
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ChangePasswordResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets a value indicating whether request has been completed successfully
        /// </summary>
        public bool Success => (Errors.Count == 0);

        /// <summary>
        /// Add error
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Errors
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}
