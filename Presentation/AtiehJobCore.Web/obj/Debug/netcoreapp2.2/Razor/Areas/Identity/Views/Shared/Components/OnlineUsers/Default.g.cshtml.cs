#pragma checksum "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99752871481b6381f6ab97195c0e607edd9b250e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Views_Shared_Components_OnlineUsers_Default), @"mvc.1.0.view", @"/Areas/Identity/Views/Shared/Components/OnlineUsers/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Identity/Views/Shared/Components/OnlineUsers/Default.cshtml", typeof(AspNetCore.Areas_Identity_Views_Shared_Components_OnlineUsers_Default))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99752871481b6381f6ab97195c0e607edd9b250e", @"/Areas/Identity/Views/Shared/Components/OnlineUsers/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8ce39622f916d9215a96d4e30e917e84a1f76b", @"/Areas/Identity/Views/_ViewImports.cshtml")]
    public class Areas_Identity_Views_Shared_Components_OnlineUsers_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OnlineUsersViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "OnlineUsers", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "UserPanel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-header"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-thumbnail userImage"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onerror", new global::Microsoft.AspNetCore.Html.HtmlString("this.style.visibility = \'hidden\';this.width = 0;this.height = 0;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("previous pagerSpan float-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-md-12"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("row"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-footer"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card mt-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
  
    var minutes = Model.MinutesToTake.ToPersianNumbers();
    var usersCount = Model.NumbersToTake.ToPersianNumbers();

#line default
#line hidden
            BeginContext(198, 2115, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99752871481b6381f6ab97195c0e607edd9b250e10567", async() => {
                BeginContext(221, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(227, 578, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99752871481b6381f6ab97195c0e607edd9b250e10953", async() => {
                    BeginContext(252, 47, true);
                    WriteLiteral("\r\n        <h5 class=\"card-title\">\r\n            ");
                    EndContext();
                    BeginContext(299, 479, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99752871481b6381f6ab97195c0e607edd9b250e11405", async() => {
                        BeginContext(385, 80, true);
                        WriteLiteral("\r\n                <span class=\"fas fa-object-group\" aria-hidden=\"true\"></span>\r\n");
                        EndContext();
#line 12 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                 if (Model.ShowMoreItemsLink)
                {

#line default
#line hidden
                        BeginContext(531, 44, true);
                        WriteLiteral("                    <span>کاربران آنلاین در ");
                        EndContext();
                        BeginContext(576, 7, false);
#line 14 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                                       Write(minutes);

#line default
#line hidden
                        EndContext();
                        BeginContext(583, 19, true);
                        WriteLiteral(" دقیقه قبل</span>\r\n");
                        EndContext();
#line 15 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                }
                else
                {

#line default
#line hidden
                        BeginContext(662, 26, true);
                        WriteLiteral("                    <span>");
                        EndContext();
                        BeginContext(689, 10, false);
#line 18 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                     Write(usersCount);

#line default
#line hidden
                        EndContext();
                        BeginContext(699, 17, true);
                        WriteLiteral(" کاربر آنلاین در ");
                        EndContext();
                        BeginContext(717, 7, false);
#line 18 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                                                 Write(minutes);

#line default
#line hidden
                        EndContext();
                        BeginContext(724, 19, true);
                        WriteLiteral(" دقیقه قبل</span>\r\n");
                        EndContext();
#line 19 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                }

#line default
#line hidden
                        BeginContext(762, 12, true);
                        WriteLiteral("            ");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                    BeginWriteTagHelperAttribute();
#line 10 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                                                                 WriteLiteral(AreaNames.Identity);

#line default
#line hidden
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = __tagHelperStringValueBuffer;
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-area", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(778, 21, true);
                    WriteLiteral("\r\n        </h5>\r\n    ");
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
                BeginContext(805, 63, true);
                WriteLiteral("\r\n    <ul id=\"voteslist\" class=\"list-group list-group-flush\">\r\n");
                EndContext();
#line 24 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
         foreach (var user in Model.Users)
        {
            var photoUrl = PhotoService.GetUserPhotoUrl(user.PhotoFileName);

#line default
#line hidden
                BeginContext(1001, 128, true);
                WriteLiteral("            <li class=\"list-group-item d-flex justify-content-between align-items-center\" role=\"presentation\">\r\n                ");
                EndContext();
                BeginContext(1129, 393, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99752871481b6381f6ab97195c0e607edd9b250e18211", async() => {
                    BeginContext(1233, 22, true);
                    WriteLiteral("\r\n                    ");
                    EndContext();
                    BeginContext(1255, 206, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "99752871481b6381f6ab97195c0e607edd9b250e18634", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                    BeginWriteTagHelperAttribute();
#line 29 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                                                  WriteLiteral(photoUrl);

#line default
#line hidden
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = __tagHelperStringValueBuffer;
                    __tagHelperExecutionContext.AddTagHelperAttribute("src", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 29 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
AddHtmlAttributeValue("", 1313, user.DisplayName, 1313, 17, false);

#line default
#line hidden
                    EndAddHtmlAttributeValues(__tagHelperExecutionContext);
#line 29 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(1461, 22, true);
                    WriteLiteral("\r\n                    ");
                    EndContext();
                    BeginContext(1484, 16, false);
#line 31 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
               Write(user.DisplayName);

#line default
#line hidden
                    EndContext();
                    BeginContext(1500, 18, true);
                    WriteLiteral("\r\n                ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                BeginWriteTagHelperAttribute();
#line 28 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                                                               WriteLiteral(AreaNames.Identity);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-area", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#line 28 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                                                                                                  WriteLiteral(user.Id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1522, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 33 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                 if (!Model.ShowMoreItemsLink)
                {

#line default
#line hidden
                BeginContext(1591, 62, true);
                WriteLiteral("                    <span class=\"badge badge-info badge-pill\">");
                EndContext();
                BeginContext(1654, 61, false);
#line 35 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                                                         Write(user.LastVisitedDateTime.Value.ToFriendlyPersianDateTextify());

#line default
#line hidden
                EndContext();
                BeginContext(1715, 9, true);
                WriteLiteral("</span>\r\n");
                EndContext();
#line 36 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                }

#line default
#line hidden
                BeginContext(1743, 19, true);
                WriteLiteral("            </li>\r\n");
                EndContext();
#line 38 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
        }

#line default
#line hidden
                BeginContext(1773, 11, true);
                WriteLiteral("    </ul>\r\n");
                EndContext();
#line 40 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
     if (Model.ShowMoreItemsLink && Model.Users.Any())
    {

#line default
#line hidden
                BeginContext(1847, 8, true);
                WriteLiteral("        ");
                EndContext();
                BeginContext(1855, 443, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99752871481b6381f6ab97195c0e607edd9b250e27255", async() => {
                    BeginContext(1880, 14, true);
                    WriteLiteral("\r\n            ");
                    EndContext();
                    BeginContext(1894, 388, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99752871481b6381f6ab97195c0e607edd9b250e27672", async() => {
                        BeginContext(1911, 18, true);
                        WriteLiteral("\r\n                ");
                        EndContext();
                        BeginContext(1929, 333, false);
                        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99752871481b6381f6ab97195c0e607edd9b250e28113", async() => {
                            BeginContext(1952, 22, true);
                            WriteLiteral("\r\n                    ");
                            EndContext();
                            BeginContext(1974, 264, false);
                            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "99752871481b6381f6ab97195c0e607edd9b250e28578", async() => {
                                BeginContext(2122, 112, true);
                                WriteLiteral("\r\n                        بیشتر\r\n                        <span aria-hidden=\"true\">←</span>\r\n                    ");
                                EndContext();
                            }
                            );
                            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                            BeginWriteTagHelperAttribute();
#line 45 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
                                                           WriteLiteral(AreaNames.Identity);

#line default
#line hidden
                            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = __tagHelperStringValueBuffer;
                            __tagHelperExecutionContext.AddTagHelperAttribute("asp-area", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                            if (!__tagHelperExecutionContext.Output.IsContentModified)
                            {
                                await __tagHelperExecutionContext.SetOutputContentAsync();
                            }
                            Write(__tagHelperExecutionContext.Output);
                            __tagHelperExecutionContext = __tagHelperScopeManager.End();
                            EndContext();
                            BeginContext(2238, 18, true);
                            WriteLiteral("\r\n                ");
                            EndContext();
                        }
                        );
                        __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
                        __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
                        __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                        await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                        if (!__tagHelperExecutionContext.Output.IsContentModified)
                        {
                            await __tagHelperExecutionContext.SetOutputContentAsync();
                        }
                        Write(__tagHelperExecutionContext.Output);
                        __tagHelperExecutionContext = __tagHelperScopeManager.End();
                        EndContext();
                        BeginContext(2262, 14, true);
                        WriteLiteral("\r\n            ");
                        EndContext();
                    }
                    );
                    __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
                    __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(2282, 10, true);
                    WriteLiteral("\r\n        ");
                    EndContext();
                }
                );
                __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
                __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2298, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 53 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\Shared\Components\OnlineUsers\Default.cshtml"
    }

#line default
#line hidden
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
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
        public IUsersPhotoService PhotoService { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<OnlineUsersViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591