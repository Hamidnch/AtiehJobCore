using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Localization;
using System.ComponentModel;

namespace AtiehJobCore.Web.Framework.Mvc.ModelBinding
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    /// Represents model attribute that specifies the display name by passed key of the locale resource
    /// </summary>
    public class AtiehJobResourceDisplayNameAttribute : DisplayNameAttribute, IModelAttribute
    {
        #region Fields

        private string _resourceValue = string.Empty;

        #endregion

        #region Ctor

        /// <inheritdoc />
        /// <summary>
        /// Create instance of the attribute
        /// </summary>
        /// <param name="resourceKey">Key of the locale resource</param>
        public AtiehJobResourceDisplayNameAttribute(string resourceKey) : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets key of the locale resource 
        /// </summary>
        public string ResourceKey { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the display name
        /// </summary>
        public override string DisplayName
        {
            get
            {
                //get working language identifier
                var workingLanguageId =
                    EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;

                //get locale resource value
                _resourceValue = EngineContext.Current.Resolve<ILocalizationService>()
                   .GetResource(ResourceKey, workingLanguageId, true, ResourceKey);

                return _resourceValue;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets name of the attribute
        /// </summary>
        public string Name => nameof(AtiehJobResourceDisplayNameAttribute);

        #endregion
    }
}
