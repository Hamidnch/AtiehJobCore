#pragma checksum "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Account\ForgotPasswordConfirmation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f07ba8403bea9852d4e219ff709699fccc1853d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Views_Account_ForgotPasswordConfirmation), @"mvc.1.0.view", @"/Areas/Identity/Views/Account/ForgotPasswordConfirmation.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Identity/Views/Account/ForgotPasswordConfirmation.cshtml", typeof(AspNetCore.Areas_Identity_Views_Account_ForgotPasswordConfirmation))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Areas.Identity;

#line default
#line hidden
#line 2 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Common.Constants;

#line default
#line hidden
#line 3 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Account;

#line default
#line hidden
#line 4 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Settings;

#line default
#line hidden
#line 5 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Password;

#line default
#line hidden
#line 6 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Emails;

#line default
#line hidden
#line 7 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Services.Identity.Interfaces;

#line default
#line hidden
#line 8 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Common;

#line default
#line hidden
#line 9 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Razor;

#line default
#line hidden
#line 10 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Html;

#line default
#line hidden
#line 11 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Routing;

#line default
#line hidden
#line 12 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#line 13 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Globalization;

#line default
#line hidden
#line 14 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Threading.Tasks;

#line default
#line hidden
#line 15 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Net;

#line default
#line hidden
#line 16 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using DNTPersianUtils.Core;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f07ba8403bea9852d4e219ff709699fccc1853d", @"/Areas/Identity/Views/Account/ForgotPasswordConfirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8ce39622f916d9215a96d4e30e917e84a1f76b", @"/Areas/Identity/Views/_ViewImports.cshtml")]
    public class Areas_Identity_Views_Account_ForgotPasswordConfirmation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-header"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PickupFolder", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-body"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card mt-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Account\ForgotPasswordConfirmation.cshtml"
  
    Layout = "_ColumnsOneLayout";

#line default
#line hidden
#line 4 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Account\ForgotPasswordConfirmation.cshtml"
  
    ViewData["Title"] = "تائید کلمه‌ی عبور فراموش شده";

#line default
#line hidden
            BeginContext(100, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(101, 277, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8f07ba8403bea9852d4e219ff709699fccc1853d8202", async() => {
                BeginContext(124, 5, true);
                WriteLiteral("\n    ");
                EndContext();
                BeginContext(129, 91, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8f07ba8403bea9852d4e219ff709699fccc1853d8584", async() => {
                    BeginContext(154, 32, true);
                    WriteLiteral("\n        <h5 class=\"card-title\">");
                    EndContext();
                    BeginContext(187, 17, false);
#line 10 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Account\ForgotPasswordConfirmation.cshtml"
                          Write(ViewData["Title"]);

#line default
#line hidden
                    EndContext();
                    BeginContext(204, 10, true);
                    WriteLiteral("</h5>\n    ");
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
                BeginContext(220, 5, true);
                WriteLiteral("\n    ");
                EndContext();
                BeginContext(225, 146, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8f07ba8403bea9852d4e219ff709699fccc1853d10392", async() => {
                    BeginContext(248, 80, true);
                    WriteLiteral("\n        لطفا میل‌باکس خود را جهت بازیابی کلمه‌ی عبور خود بررسی نمائید.\n        ");
                    EndContext();
                    BeginContext(328, 32, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8f07ba8403bea9852d4e219ff709699fccc1853d10872", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(360, 5, true);
                    WriteLiteral("\n    ");
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
                BeginContext(371, 1, true);
                WriteLiteral("\n");
                EndContext();
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
        }
        #pragma warning restore 1998
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
