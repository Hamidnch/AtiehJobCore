using AtiehJobCore.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace AtiehJobCore.Web.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("div")]
    public class VisibilityTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-is-visible")]
        public bool IsVisible { get; set; } = true;
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            context.CheckArgumentIsNull(nameof(context));
            output.CheckArgumentIsNull(nameof(output));

            if (!IsVisible) output.SuppressOutput();

            return base.ProcessAsync(context, output);
        }
    }
}
