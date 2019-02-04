namespace AtiehJobCore.Common.MongoDb.Domain.Seo
{
    /// <summary>
    /// Represents a page title SEO adjustment
    /// </summary>
    public enum PageTitleSeoAdjustment
    {
        /// <summary>
        /// Pagename comes after site name
        /// </summary>
        PagenameAfterSiteName = 0,
        /// <summary>
        /// SiteName comes after pagename
        /// </summary>
        SiteNameAfterPagename = 10
    }
}
