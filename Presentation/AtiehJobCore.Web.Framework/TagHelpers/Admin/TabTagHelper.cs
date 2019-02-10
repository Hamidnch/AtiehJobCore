using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AtiehJobCore.Web.Framework.TagHelpers.Admin
{
    [HtmlTargetElement("li", Attributes = ForAttributeName)]
    public class TabTagHelper : TagHelper
    {
        private const string ForAttributeName = "tab-index";

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(ForAttributeName)]
        public int CurrentIndex { set; get; }

        private int GetSelectedTabIndex()
        {
            var index = 0;
            const string dataKey = "AtiehJob.selected-tab-index";
            if (ViewContext.ViewData[dataKey] is int i)
            {
                index = i;
            }
            if (ViewContext.TempData[dataKey] is int i1)
            {
                index = i1;
            }

            if (index < 0)
                index = 0;

            return index;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var selectedIndex = GetSelectedTabIndex();

            if (selectedIndex == CurrentIndex)
            {
                output.Attributes.SetAttribute("class", "k-state-active");
            }
        }
    }
}
