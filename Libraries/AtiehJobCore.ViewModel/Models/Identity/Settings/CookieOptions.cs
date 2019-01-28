using System;

namespace AtiehJobCore.ViewModel.Models.Identity.Settings
{
    public class CookieOptions
    {
        public string AccessDeniedPath { get; set; }
        public string ExternalAccessDeniedPath { get; set; }
        public string CookieName { get; set; }
        public string ExternalCookieName { get; set; }
        public TimeSpan ExpireTimeSpan { get; set; }
        public string LoginPath { get; set; }
        public string ExternalLoginPath { get; set; }
        public string LogoutPath { get; set; }
        public bool SlidingExpiration { get; set; }
        public bool UseDistributedCacheTicketStore { set; get; }
        public DistributedSqlServerCacheOptions DistributedSqlServerCacheOptions { set; get; }
    }
}
