using AtiehJobCore.Core.Configuration;

namespace AtiehJobCore.Core.Domain.Catalog
{
    public class CatalogSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether users are allowed to change product view mode
        /// </summary>
        public string DefaultViewMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a 'Share button' is enabled
        /// </summary>
        public bool ShowShareButton { get; set; }

        /// <summary>
        /// Gets or sets a share code (e.g. AddThis button code)
        /// </summary>
        public string PageShareCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether product 'Email a friend' feature is enabled
        /// </summary>
        public bool EmailAFriendEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'ask product question' feature is enabled
        /// </summary>
        public bool AskQuestionEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow anonymous users to email a friend.
        /// </summary>
        public bool AllowAnonymousUsersToEmailAFriend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether autocomplete is enabled
        /// </summary>
        public bool SearchAutoCompleteEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether search by description is enabled
        /// </summary>
        public bool SearchByDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show images in the auto complete search
        /// </summary>
        public bool ShowImagesInSearchAutoComplete { get; set; }

        /// <summary>
        /// Gets or sets a minimum search term length
        /// </summary>
        public int SearchTermMinimumLength { get; set; }

        /// <summary>
        /// Gets or sets save search autocomplete
        /// </summary>
        public bool SaveSearchAutoComplete { get; set; }

        /// <summary>
        /// Gets or sets a number of products per page on the search products page
        /// </summary>
        public int SearchPageProductsPerPage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether users are allowed to select page size on the search products page
        /// </summary>
        public bool SearchPageAllowUsersToSelectPageSize { get; set; }
        /// <summary>
        /// Gets or sets the available user selectable page size options on the search products page
        /// </summary>
        public string SearchPagePageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should process attribute change using AJAX. It's used for dynamical attribute change, SKU/GTIN update of combinations, conditional attributes
        /// </summary>
        public bool AjaxProcessAttributeChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore ACL rules (side-wide). It can significantly improve performance when enabled.
        /// </summary>
        public bool IgnoreAcl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore load Filterable available start and end date products (side-wide). It can significantly improve performance when enabled.
        /// </summary>
        public bool IgnoreFilterableAvailableStartEndDateTime { get; set; }
    }
}
