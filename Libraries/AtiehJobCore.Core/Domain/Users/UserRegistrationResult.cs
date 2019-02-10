﻿using System.Collections.Generic;

namespace AtiehJobCore.Core.Domain.Users
{
    /// <summary>
    /// User Registration result
    /// </summary>
    public class UserRegistrationResult
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public UserRegistrationResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets a value indicating whether request has been completed successfully
        /// </summary>
        public bool Success => Errors.Count == 0;

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