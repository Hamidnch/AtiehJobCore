#pragma checksum "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Home\ColumnsOne.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d2e21d3ec6980ed584812e38e2b2acc612f2a4cc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ColumnsOne), @"mvc.1.0.view", @"/Views/Home/ColumnsOne.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/ColumnsOne.cshtml", typeof(AspNetCore.Views_Home_ColumnsOne))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d2e21d3ec6980ed584812e38e2b2acc612f2a4cc", @"/Views/Home/ColumnsOne.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"910e6c74a464292d19aea2b885b2bf5495068ea8", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ColumnsOne : AtiehJobCore.Web.Framework.Web.Razor.BaseView<dynamic>
    {
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
            BeginContext(104, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 4 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Home\ColumnsOne.cshtml"
  
    ViewData["Title"] = "Columns One";
    Layout = "_ColumnsOneLayout";

    //var localizer = HtmlLocalizerFactory.Create(
    //  baseName: "Controllers.HomeController" /*مشخصات كنترلر جاري*/,
    //  location: "AtiehJobCore.Resources" /*نام اسمبلي ثالث*/);

    //var sharedLocalizer = HtmlLocalizerFactory.Create(
    //  baseName: "SharedResource" /*مشخصات*/,
    //  location: "AtiehJobCore.Resources" /*نام اسمبلي ثالث*/);



#line default
#line hidden
            BeginContext(560, 22, true);
            WriteLiteral("\r\n<h1>OneColumn</h1>\r\n");
            EndContext();
            BeginContext(582, 11, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d2e21d3ec6980ed584812e38e2b2acc612f2a4cc5068", async() => {
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(593, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(598, 19, false);
#line 22 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Home\ColumnsOne.cshtml"
Write(T("account.avatar"));

#line default
#line hidden
            EndContext();
            BeginContext(617, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(621, 43, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d2e21d3ec6980ed584812e38e2b2acc612f2a4cc6452", async() => {
                BeginContext(627, 31, false);
#line 24 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Home\ColumnsOne.cshtml"
Write(DateTime.Now.ToLongDateString());

#line default
#line hidden
                EndContext();
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(664, 2, true);
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
