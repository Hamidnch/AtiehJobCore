#pragma checksum "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2ae20fdf43502a7870ea4178d4adf9693ebd2605"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__BasicLayout), @"mvc.1.0.view", @"/Views/Shared/_BasicLayout.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_BasicLayout.cshtml", typeof(AspNetCore.Views_Shared__BasicLayout))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using System.Net;

#line default
#line hidden
#line 2 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Routing;

#line default
#line hidden
#line 3 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#line 4 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using System.Globalization;

#line default
#line hidden
#line 5 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using System.Threading.Tasks;

#line default
#line hidden
#line 6 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using DNTPersianUtils.Core;

#line default
#line hidden
#line 7 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Framework.Models;

#line default
#line hidden
#line 8 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using AtiehJobCore.Core.Contracts;

#line default
#line hidden
#line 9 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Framework.Models.Account;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ae20fdf43502a7870ea4178d4adf9693ebd2605", @"/Views/Shared/_BasicLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"910e6c74a464292d19aea2b885b2bf5495068ea8", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__BasicLayout : AtiehJobCore.Web.Framework.Web.Razor.BaseView<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("content"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("row"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("container"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml"
  
    Layout = "_RootLayout";

#line default
#line hidden
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(39, 34, false);
#line 5 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml"
Write(await Html.PartialAsync("_Header"));

#line default
#line hidden
            EndContext();
            BeginContext(73, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(76, 38, false);
#line 6 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml"
Write(await Html.PartialAsync("_NavbarMenu"));

#line default
#line hidden
            EndContext();
            BeginContext(114, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(117, 38, false);
#line 7 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml"
Write(await Html.PartialAsync("_BreadCrumb"));

#line default
#line hidden
            EndContext();
            BeginContext(155, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(159, 151, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2ae20fdf43502a7870ea4178d4adf9693ebd26056661", async() => {
                BeginContext(182, 34, true);
                WriteLiteral("\r\n    <main role=\"main\">\r\n        ");
                EndContext();
                BeginContext(216, 73, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2ae20fdf43502a7870ea4178d4adf9693ebd26057078", async() => {
                    BeginContext(246, 14, true);
                    WriteLiteral("\r\n            ");
                    EndContext();
                    BeginContext(261, 12, false);
#line 12 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml"
       Write(RenderBody());

#line default
#line hidden
                    EndContext();
                    BeginContext(273, 10, true);
                    WriteLiteral("\r\n        ");
                    EndContext();
                }
                );
                __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
                __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(289, 15, true);
                WriteLiteral("\r\n    </main>\r\n");
                EndContext();
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(310, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(352, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(355, 34, false);
#line 19 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml"
Write(await Html.PartialAsync("_Footer"));

#line default
#line hidden
            EndContext();
            BeginContext(389, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(392, 32, false);
#line 20 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml"
Write(await Html.PartialAsync("_Foot"));

#line default
#line hidden
            EndContext();
            BeginContext(424, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(427, 52, false);
#line 21 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\_BasicLayout.cshtml"
Write(await RenderSectionAsync("Scripts", required: false));

#line default
#line hidden
            EndContext();
            BeginContext(479, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IWebHelper WebHelper { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
