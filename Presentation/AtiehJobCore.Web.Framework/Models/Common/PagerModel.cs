using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Localization;

namespace AtiehJobCore.Web.Framework.Models.Common
{
    public partial class PagerModel
    {
        #region Constructors

        public PagerModel()
            : this(EngineContext.Current.Resolve<ILocalizationService>())
        {

        }

        public PagerModel(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        #endregion Constructors

        #region Fields

        private readonly ILocalizationService _localizationService;
        private int _individualPagesDisplayedCount;
        private int _pageIndex = -2;
        private int _pageSize;

        private bool? _showFirst;
        private bool? _showIndividualPages;
        private bool? _showLast;
        private bool? _showNext;
        private bool? _showPagerItems;
        private bool? _showPrevious;
        private bool? _showTotalSummary;

        private string _firstButtonText;
        private string _lastButtonText;
        private string _nextButtonText;
        private string _previousButtonText;
        private string _currentPageText;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the current page index
        /// </summary>
        public int CurrentPage => (PageIndex + 1);

        /// <summary>
        /// Gets or sets a count of individual pages to be displayed
        /// </summary>
        public int IndividualPagesDisplayedCount
        {
            get => _individualPagesDisplayedCount <= 0 ? 5 : _individualPagesDisplayedCount;
            set => _individualPagesDisplayedCount = value;
        }

        /// <summary>
        /// Gets the current page index
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex < 0 ? 0 : _pageIndex;
            set => _pageIndex = value;
        }

        /// <summary>
        /// Gets or sets a page size
        /// </summary>
        public int PageSize
        {
            get => (_pageSize <= 0) ? 10 : _pageSize;
            set => _pageSize = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "first"
        /// </summary>
        public bool ShowFirst
        {
            get => _showFirst ?? true;
            set => _showFirst = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "individual pages"
        /// </summary>
        public bool ShowIndividualPages
        {
            get => _showIndividualPages ?? true;
            set => _showIndividualPages = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "last"
        /// </summary>
        public bool ShowLast
        {
            get => _showLast ?? true;
            set => _showLast = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "next"
        /// </summary>
        public bool ShowNext
        {
            get => _showNext ?? true;
            set => _showNext = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show pager items
        /// </summary>
        public bool ShowPagerItems
        {
            get => _showPagerItems ?? true;
            set => _showPagerItems = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "previous"
        /// </summary>
        public bool ShowPrevious
        {
            get => _showPrevious ?? true;
            set => _showPrevious = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "total summary"
        /// </summary>
        public bool ShowTotalSummary
        {
            get => _showTotalSummary ?? false;
            set => _showTotalSummary = value;
        }

        /// <summary>
        /// Gets a total pages count
        /// </summary>
        public int TotalPages
        {
            get
            {
                if ((TotalRecords == 0) || (PageSize == 0))
                {
                    return 0;
                }
                var num = TotalRecords / PageSize;
                if ((TotalRecords % PageSize) > 0)
                {
                    num++;
                }
                return num;
            }
        }

        /// <summary>
        /// Gets or sets a total records count
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the first button text
        /// </summary>
        public string FirstButtonText
        {
            get => (!string.IsNullOrEmpty(_firstButtonText)) ?
                _firstButtonText :
                _localizationService.GetResource("Pager.First");
            set => _firstButtonText = value;
        }

        /// <summary>
        /// Gets or sets the last button text
        /// </summary>
        public string LastButtonText
        {
            get => (!string.IsNullOrEmpty(_lastButtonText)) ?
                _lastButtonText :
                _localizationService.GetResource("Pager.Last");
            set => _lastButtonText = value;
        }

        /// <summary>
        /// Gets or sets the next button text
        /// </summary>
        public string NextButtonText
        {
            get => (!string.IsNullOrEmpty(_nextButtonText)) ?
                _nextButtonText :
                _localizationService.GetResource("Pager.Next");
            set => _nextButtonText = value;
        }

        /// <summary>
        /// Gets or sets the previous button text
        /// </summary>
        public string PreviousButtonText
        {
            get => (!string.IsNullOrEmpty(_previousButtonText)) ?
                _previousButtonText :
                _localizationService.GetResource("Pager.Previous");
            set => _previousButtonText = value;
        }

        /// <summary>
        /// Gets or sets the current page text
        /// </summary>
        public string CurrentPageText
        {
            get => (!string.IsNullOrEmpty(_currentPageText)) ?
                _currentPageText :
                _localizationService.GetResource("Pager.CurrentPage");
            set => _currentPageText = value;
        }

        /// <summary>
        /// Gets or sets the route name or action name
        /// </summary>
        public string RouteActionName { get; set; }

        /// <summary>
        /// Gets or sets whether the links are created using RouteLink instead of Action Link 
        /// (for additional route values such as slugs or page numbers)
        /// </summary>
        public bool UseRouteLinks { get; set; }

        /// <summary>
        /// Gets or sets the RouteValues object. Allows for custom route values other than page.
        /// </summary>
        public IRouteValues RouteValues { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets first individual page index
        /// </summary>
        /// <returns>Page index</returns>
        public int GetFirstIndividualPageIndex()
        {
            if ((TotalPages < IndividualPagesDisplayedCount) ||
                ((PageIndex - (IndividualPagesDisplayedCount / 2)) < 0))
            {
                return 0;
            }
            if ((PageIndex + (IndividualPagesDisplayedCount / 2)) >= TotalPages)
            {
                return (TotalPages - IndividualPagesDisplayedCount);
            }
            return (PageIndex - (IndividualPagesDisplayedCount / 2));
        }

        /// <summary>
        /// Get last individual page index
        /// </summary>
        /// <returns>Page index</returns>
        public int GetLastIndividualPageIndex()
        {
            var num = IndividualPagesDisplayedCount / 2;
            if ((IndividualPagesDisplayedCount % 2) == 0)
            {
                num--;
            }
            if ((TotalPages < IndividualPagesDisplayedCount) ||
                ((PageIndex + num) >= TotalPages))
            {
                return (TotalPages - 1);
            }
            if ((PageIndex - (IndividualPagesDisplayedCount / 2)) < 0)
            {
                return (IndividualPagesDisplayedCount - 1);
            }
            return (PageIndex + num);
        }

        #endregion Methods
    }

    #region Classes

    /// <summary>
    /// Interface for custom RouteValues objects
    /// </summary>
    public interface IRouteValues
    {
        int PageNumber { get; set; }
    }

    /// <inheritdoc />
    /// <summary>
    /// Class that has a slug and page for route values. Used for Topic (posts) and 
    /// Forum (topics) pagination
    /// </summary>
    public partial class RouteValues : IRouteValues
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public int PageNumber { get; set; }
    }

    #endregion Classes
}
