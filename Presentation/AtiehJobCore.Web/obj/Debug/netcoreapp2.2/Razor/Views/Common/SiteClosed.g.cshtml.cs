#pragma checksum "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Common\SiteClosed.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f43b3b88076970918d9f5063fab1f766b8d35a6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Common_SiteClosed), @"mvc.1.0.view", @"/Views/Common/SiteClosed.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Common/SiteClosed.cshtml", typeof(AspNetCore.Views_Common_SiteClosed))]
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
#line 1 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Common\SiteClosed.cshtml"
using AtiehJobCore.Web.Framework.UI;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f43b3b88076970918d9f5063fab1f766b8d35a6b", @"/Views/Common/SiteClosed.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"910e6c74a464292d19aea2b885b2bf5495068ea8", @"/Views/_ViewImports.cshtml")]
    public class Views_Common_SiteClosed : AtiehJobCore.Web.Framework.Web.Razor.BaseView<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("page store-closed-page"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Common\SiteClosed.cshtml"
  
    Layout = "_BasicLayout";

    //title
    Html.AddTitleParts(T("PageTitle.SiteClosed").Text);

#line default
#line hidden
            BeginContext(147, 126, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f43b3b88076970918d9f5063fab1f766b8d35a6b5044", async() => {
                BeginContext(183, 34, true);
                WriteLiteral("\r\n    <h1 class=\"h2 generalTitle\">");
                EndContext();
                BeginContext(218, 15, false);
#line 9 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Common\SiteClosed.cshtml"
                           Write(T("SiteClosed"));

#line default
#line hidden
                EndContext();
                BeginContext(233, 11, true);
                WriteLiteral("</h1>\r\n    ");
                EndContext();
                BeginContext(245, 20, false);
#line 10 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Common\SiteClosed.cshtml"
Write(T("SiteClosed.Hint"));

#line default
#line hidden
                EndContext();
                BeginContext(265, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(273, 2, true);
            WriteLiteral("\r\n");
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
