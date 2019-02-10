using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace AtiehJobCore.Web.Framework.TagHelpers.Admin
{
    [HtmlTargetElement("admin-label", Attributes = ForAttributeName)]
    public class LabelRequiredTagHelper : LabelTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string DisplayHintAttributeName = "asp-display-hint";

        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;

        public LabelRequiredTagHelper(IHtmlGenerator generator,
            IWorkContext workContext, ILocalizationService localizationService) : base(generator)
        {
            _workContext = workContext;
            _localizationService = localizationService;
        }

        [HtmlAttributeName(DisplayHintAttributeName)]
        public bool DisplayHint { get; set; } = true;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.TagName = "label";
            output.TagMode = TagMode.StartTagAndEndTag;
            var classValue = output.Attributes.ContainsName("class")
                                ? $"{output.Attributes["class"].Value}"
                                : "control-label col-md-3 col-sm-3";
            output.Attributes.SetAttribute("class", classValue);

            if (For.Metadata.AdditionalValues.TryGetValue("AtiehJobResourceDisplayNameAttribute", out var value))
            {
                if (value is AtiehJobResourceDisplayNameAttribute resourceDisplayName && DisplayHint)
                {
                    var langId = _workContext.WorkingLanguage.Id;
                    var hintResource = _localizationService.GetResource(
                        resourceDisplayName.ResourceKey + ".Hint", langId, returnEmptyIfNotFound: true,
                        logIfNotFound: false);

                    if (!string.IsNullOrEmpty(hintResource))
                    {
                        var i = new TagBuilder("i");
                        i.AddCssClass("help icon-question");
                        i.Attributes.Add("title", hintResource);
                        i.Attributes.Add("data-toggle", "tooltip");
                        i.Attributes.Add("data-placement", "bottom");
                        output.Content.AppendHtml(i.ToHtmlString());
                    }
                }

            }
        }
    }
}
