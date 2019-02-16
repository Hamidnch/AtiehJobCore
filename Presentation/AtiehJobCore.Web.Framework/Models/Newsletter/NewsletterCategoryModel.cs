using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Models.Newsletter
{
    public partial class NewsletterCategoryModel : BaseMongoModel
    {
        public NewsletterCategoryModel()
        {
            NewsletterCategories = new List<NewsletterSimpleCategory>();
        }
        public string NewsletterEmailId { get; set; }
        public IList<NewsletterSimpleCategory> NewsletterCategories { get; set; }
    }
    public class NewsletterSimpleCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }
    }
}
