#pragma checksum "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80a1eb254a8b6adbd720de927990b8d8576bd653"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_LanguageSelector_Default), @"mvc.1.0.view", @"/Views/Shared/Components/LanguageSelector/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/LanguageSelector/Default.cshtml", typeof(AspNetCore.Views_Shared_Components_LanguageSelector_Default))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"80a1eb254a8b6adbd720de927990b8d8576bd653", @"/Views/Shared/Components/LanguageSelector/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"910e6c74a464292d19aea2b885b2bf5495068ea8", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_LanguageSelector_Default : AtiehJobCore.Web.Framework.Web.Razor.BaseView<LanguageSelectorModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn-group btn-group-sm float-xs-right align-self-center d-none d-lg-block pr-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("dropdown-divider"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("dropdown-menu"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("aria-labelledby", new global::Microsoft.AspNetCore.Html.HtmlString("userDropdown"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(30, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
 if (Model.AvailableLanguages.Count > 1)
{
    var rawUrl = WebHelper.GetRawUrl(HttpContextAccessor.HttpContext.Request);

    if (Model.UseImages)
    {

#line default
#line hidden
            BeginContext(192, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(200, 962, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80a1eb254a8b6adbd720de927990b8d8576bd6536475", async() => {
                BeginContext(292, 90, true);
                WriteLiteral("\r\n            <ul class=\"list-inline language-list pl-0 mb-0 d-flex align-items-center\">\r\n");
                EndContext();
#line 11 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                 foreach (var lang in Model.AvailableLanguages)
                {


#line default
#line hidden
                BeginContext(468, 77, true);
                WriteLiteral("                    <li class=\"list-inline-item\">\r\n                        <a");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 545, "\"", 763, 1);
#line 15 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
WriteAttributeValue("", 552, WebHelper.ModifyQueryString(Url.RouteUrl("ChangeLanguage",
                                     new {langId = lang.Id}),
                                     "returnurl=" + WebUtility.UrlEncode(rawUrl), null), 552, 211, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginWriteAttribute("title", " title=\"", 764, "\"", 782, 1);
#line 17 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
WriteAttributeValue("", 772, lang.Name, 772, 10, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(783, 35, true);
                WriteLiteral(">\r\n                            <img");
                EndContext();
                BeginWriteAttribute("title", " title=\'", 818, "\'", 836, 1);
#line 18 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
WriteAttributeValue("", 826, lang.Name, 826, 10, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginWriteAttribute("alt", "\r\n                                 alt=\'", 837, "\'", 887, 1);
#line 19 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
WriteAttributeValue("", 877, lang.Name, 877, 10, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(888, 1, true);
                WriteLiteral(" ");
                EndContext();
                BeginContext(891, 61, false);
#line 19 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                                              Write(lang.Id == Model.CurrentLanguageId ? " class=selected" : null);

#line default
#line hidden
                EndContext();
                BeginContext(953, 40, true);
                WriteLiteral("\r\n                                 src=\"");
                EndContext();
                BeginContext(994, 52, false);
#line 20 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                                 Write(Url.Content("~/img/flags/" + lang.FlagImageFileName));

#line default
#line hidden
                EndContext();
                BeginContext(1046, 64, true);
                WriteLiteral(" \" />\r\n                        </a>\r\n                    </li>\r\n");
                EndContext();
#line 23 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                }

#line default
#line hidden
                BeginContext(1129, 27, true);
                WriteLiteral("            </ul>\r\n        ");
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
            BeginContext(1162, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 44 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                    
    }
    else
    {

        var languages = Model.AvailableLanguages.Select(lang => new SelectListItem
        {
            Text = lang.Name,
            Value = WebHelper.ModifyQueryString(Url.RouteUrl("ChangeLanguage",
                new { langid = lang.Id }), "returnurl=" + WebUtility.UrlEncode(rawUrl), null),
            Selected = lang.Id.Equals(Model.CurrentLanguageId)
        });




#line default
#line hidden
            BeginContext(2680, 229, true);
            WriteLiteral("        <li class=\"nav-item dropdown\">\r\n            <a class=\"nav-link dropdown-toggle\" data-toggle=\"dropdown\"\r\n               id=\"userDropdown\" aria-haspopup=\"true\" role=\"button\"\r\n               aria-expanded=\"false\" href=\"#\">\r\n");
            EndContext();
            BeginContext(3103, 126, true);
            WriteLiteral("                <i class=\"fa fa-language\">\r\n                </i>\r\n                زبان سیستم\r\n            </a>\r\n\r\n            ");
            EndContext();
            BeginContext(3229, 433, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80a1eb254a8b6adbd720de927990b8d8576bd65312626", async() => {
                BeginContext(3287, 4, true);
                WriteLiteral("\r\n\r\n");
                EndContext();
#line 72 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                 foreach (var item in languages)
                {
                    var className = item.Selected ? "active" : "";

#line default
#line hidden
                BeginContext(3428, 22, true);
                WriteLiteral("                    <a");
                EndContext();
                BeginWriteAttribute("class", " class=\"", 3450, "\"", 3482, 2);
                WriteAttributeValue("", 3458, "dropdown-item", 3458, 13, true);
#line 75 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
WriteAttributeValue(" ", 3471, className, 3472, 10, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginWriteAttribute("href", " href=\"", 3483, "\"", 3501, 1);
#line 75 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
WriteAttributeValue("", 3490, item.Value, 3490, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3502, 27, true);
                WriteLiteral(">\r\n                        ");
                EndContext();
                BeginContext(3530, 9, false);
#line 76 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                   Write(item.Text);

#line default
#line hidden
                EndContext();
                BeginContext(3539, 48, true);
                WriteLiteral("\r\n                    </a>\r\n                    ");
                EndContext();
                BeginContext(3587, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80a1eb254a8b6adbd720de927990b8d8576bd65314791", async() => {
                }
                );
                __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
                __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3623, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 79 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                }

#line default
#line hidden
                BeginContext(3644, 12, true);
                WriteLiteral("            ");
                EndContext();
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3662, 17, true);
            WriteLiteral("\r\n        </li>\r\n");
            EndContext();
#line 97 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Views\Shared\Components\LanguageSelector\Default.cshtml"
                    
    }
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LanguageSelectorModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
