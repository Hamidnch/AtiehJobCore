#pragma checksum "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Views_UserCard__UserInfoPart), @"mvc.1.0.view", @"/Areas/Identity/Views/UserCard/_UserInfoPart.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Identity/Views/UserCard/_UserInfoPart.cshtml", typeof(AspNetCore.Areas_Identity_Views_UserCard__UserInfoPart))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Areas.Identity;

#line default
#line hidden
#line 2 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Common.Constants;

#line default
#line hidden
#line 3 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Account;

#line default
#line hidden
#line 4 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Settings;

#line default
#line hidden
#line 5 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Password;

#line default
#line hidden
#line 6 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Emails;

#line default
#line hidden
#line 7 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Services.Identity.Interfaces;

#line default
#line hidden
#line 8 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Common;

#line default
#line hidden
#line 9 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Razor;

#line default
#line hidden
#line 10 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Html;

#line default
#line hidden
#line 11 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Routing;

#line default
#line hidden
#line 12 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#line 13 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Globalization;

#line default
#line hidden
#line 14 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Threading.Tasks;

#line default
#line hidden
#line 15 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Net;

#line default
#line hidden
#line 16 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using DNTPersianUtils.Core;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eb0be7577fcb54c7eb7623c6b7fa88187cebab7c", @"/Areas/Identity/Views/UserCard/_UserInfoPart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8ce39622f916d9215a96d4e30e917e84a1f76b", @"/Areas/Identity/Views/_ViewImports.cshtml")]
    public class Areas_Identity_Views_UserCard__UserInfoPart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserCardItemViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-thumbnail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onerror", new global::Microsoft.AspNetCore.Html.HtmlString("this.style.visibility = \'hidden\';this.width=0; this.height=0;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-md-4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("row"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-md-6"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("row mt-4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("mt-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
  
    var user = Model.User;
    var photoUrl = PhotoService.GetUserPhotoUrl(user.PhotoFileName);

#line default
#line hidden
            BeginContext(176, 3026, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c8984", async() => {
                BeginContext(194, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(200, 276, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c9369", async() => {
                    BeginContext(217, 10, true);
                    WriteLiteral("\r\n        ");
                    EndContext();
                    BeginContext(227, 237, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c9779", async() => {
                        BeginContext(249, 14, true);
                        WriteLiteral("\r\n            ");
                        EndContext();
                        BeginContext(263, 185, false);
                        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c10213", async() => {
                        }
                        );
                        __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
                        __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
                        __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                        BeginWriteTagHelperAttribute();
#line 10 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                                WriteLiteral(photoUrl);

#line default
#line hidden
                        __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                        __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = __tagHelperStringValueBuffer;
                        __tagHelperExecutionContext.AddTagHelperAttribute("src", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                        BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 10 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
AddHtmlAttributeValue("", 311, user.DisplayName, 311, 17, false);

#line default
#line hidden
                        EndAddHtmlAttributeValues(__tagHelperExecutionContext);
#line 10 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
                        __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                        __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                        await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                        if (!__tagHelperExecutionContext.Output.IsContentModified)
                        {
                            await __tagHelperExecutionContext.SetOutputContentAsync();
                        }
                        Write(__tagHelperExecutionContext.Output);
                        __tagHelperExecutionContext = __tagHelperScopeManager.End();
                        EndContext();
                        BeginContext(448, 10, true);
                        WriteLiteral("\r\n        ");
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
                    BeginContext(464, 6, true);
                    WriteLiteral("\r\n    ");
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
                BeginContext(476, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(482, 2712, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c15263", async() => {
                    BeginContext(504, 10, true);
                    WriteLiteral("\r\n        ");
                    EndContext();
                    BeginContext(514, 1983, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c15675", async() => {
                        BeginContext(536, 186, true);
                        WriteLiteral("\r\n            <ul class=\"list-unstyled\">\r\n                <li>\r\n                    <span aria-hidden=\"true\" class=\"fas fa-user\"></span>\r\n                    <strong>عضو شماره:</strong> ");
                        EndContext();
                        BeginContext(723, 26, false);
#line 19 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                                           Write(user.Id.ToPersianNumbers());

#line default
#line hidden
                        EndContext();
                        BeginContext(749, 219, true);
                        WriteLiteral("\r\n                </li>\r\n                <li>\r\n                    <span aria-hidden=\"true\" class=\"fas fa-clock\"></span>\r\n                    <strong>تاریخ عضویت: </strong><span dir=\"ltr\">\r\n                        <abbr");
                        EndContext();
                        BeginWriteAttribute("title", " title=\"", 968, "\"", 1053, 1);
#line 24 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
WriteAttributeValue("", 976, user.CreatedDateTime.Value.ToFriendlyPersianDateTextify().ToPersianNumbers(), 976, 77, false);

#line default
#line hidden
                        EndWriteAttribute();
                        BeginContext(1054, 31, true);
                        WriteLiteral(">\r\n                            ");
                        EndContext();
                        BeginContext(1086, 99, false);
#line 25 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                       Write(user.CreatedDateTime.Value.ToShortPersianDateString(DateTimeOffsetPart.DateTime).ToPersianNumbers());

#line default
#line hidden
                        EndContext();
                        BeginContext(1185, 87, true);
                        WriteLiteral("\r\n                        </abbr>\r\n                    </span>\r\n                </li>\r\n");
                        EndContext();
#line 29 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                 if (user.IsEmailPublic)
                {

#line default
#line hidden
                        BeginContext(1333, 132, true);
                        WriteLiteral("                    <li>\r\n                        <span aria-hidden=\"true\" class=\"fas fa-envelope\"></span>\r\n                        ");
                        EndContext();
                        BeginContext(1465, 218, false);
                        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c18728", async() => {
                        }
                        );
                        __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ImageTagHelper>();
                        __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper);
                        BeginWriteTagHelperAttribute();
#line 33 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                      WriteLiteral(Url.Action(action: "EmailToImage", controller:"UserCard", values: new { id = user.Id }));

#line default
#line hidden
                        __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                        __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src = __tagHelperStringValueBuffer;
                        __tagHelperExecutionContext.AddTagHelperAttribute("src", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.Src, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 34 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion = true;

#line default
#line hidden
                        __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ImageTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                        BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 35 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
AddHtmlAttributeValue("", 1656, user.DisplayName, 1656, 17, false);

#line default
#line hidden
                        AddHtmlAttributeValue(" ", 1673, "Email", 1674, 6, true);
                        EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                        await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                        if (!__tagHelperExecutionContext.Output.IsContentModified)
                        {
                            await __tagHelperExecutionContext.SetOutputContentAsync();
                        }
                        Write(__tagHelperExecutionContext.Output);
                        __tagHelperExecutionContext = __tagHelperScopeManager.End();
                        EndContext();
                        BeginContext(1683, 29, true);
                        WriteLiteral("\r\n                    </li>\r\n");
                        EndContext();
#line 37 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                }

#line default
#line hidden
                        BeginContext(1731, 16, true);
                        WriteLiteral("                ");
                        EndContext();
#line 38 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                 if (!string.IsNullOrWhiteSpace(user.PlaceOfBirth))
                {

#line default
#line hidden
                        BeginContext(1819, 181, true);
                        WriteLiteral("                    <li>\r\n                        <span aria-hidden=\"true\" class=\"fas fa-home\"></span>\r\n                        <strong>محل اقامت:</strong>\r\n                        ");
                        EndContext();
                        BeginContext(2001, 17, false);
#line 43 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                   Write(user.PlaceOfBirth);

#line default
#line hidden
                        EndContext();
                        BeginContext(2018, 29, true);
                        WriteLiteral("\r\n                    </li>\r\n");
                        EndContext();
#line 45 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                }

#line default
#line hidden
                        BeginContext(2066, 16, true);
                        WriteLiteral("                ");
                        EndContext();
#line 46 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                 if (user.LastVisitedDateTime != null)
                {

#line default
#line hidden
                        BeginContext(2141, 194, true);
                        WriteLiteral("                    <li>\r\n                        <span aria-hidden=\"true\" class=\"fas fa-calendar-check\"></span>\r\n                        <strong>آخرین مراجعه:</strong>\r\n                        ");
                        EndContext();
                        BeginContext(2336, 80, false);
#line 51 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                   Write(user.LastVisitedDateTime.Value.ToFriendlyPersianDateTextify().ToPersianNumbers());

#line default
#line hidden
                        EndContext();
                        BeginContext(2416, 29, true);
                        WriteLiteral("\r\n                    </li>\r\n");
                        EndContext();
#line 53 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                }

#line default
#line hidden
                        BeginContext(2464, 27, true);
                        WriteLiteral("            </ul>\r\n        ");
                        EndContext();
                    }
                    );
                    __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
                    __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(2497, 10, true);
                    WriteLiteral("\r\n        ");
                    EndContext();
                    BeginContext(2507, 675, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb0be7577fcb54c7eb7623c6b7fa88187cebab7c26047", async() => {
                        BeginContext(2529, 42, true);
                        WriteLiteral("\r\n            <ul class=\"list-unstyled\">\r\n");
                        EndContext();
#line 58 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                 if (user.DateOfBirth.HasValue)
                {

#line default
#line hidden
                        BeginContext(2639, 183, true);
                        WriteLiteral("                    <li>\r\n                        <span aria-hidden=\"true\" class=\"fas fa-calendar\"></span>\r\n                        <strong>سن:</strong>\r\n                        <abbr");
                        EndContext();
                        BeginWriteAttribute("title", " title=\"", 2822, "\"", 2985, 3);
#line 63 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
WriteAttributeValue("", 2830, user.DateOfBirth.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture), 2830, 76, false);

#line default
#line hidden
                        WriteAttributeValue("", 2906, ",", 2906, 1, true);
#line 63 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
WriteAttributeValue(" ", 2907, user.DateOfBirth.Value.ToShortPersianDateString(DateTimeOffsetPart.DateTime), 2908, 77, false);

#line default
#line hidden
                        EndWriteAttribute();
                        BeginContext(2986, 31, true);
                        WriteLiteral(">\r\n                            ");
                        EndContext();
                        BeginContext(3018, 50, false);
#line 64 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                       Write(user.DateOfBirth.Value.GetAge().ToPersianNumbers());

#line default
#line hidden
                        EndContext();
                        BeginContext(3068, 62, true);
                        WriteLiteral("\r\n                        </abbr>\r\n                    </li>\r\n");
                        EndContext();
#line 67 "D:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\_UserInfoPart.cshtml"
                }

#line default
#line hidden
                        BeginContext(3149, 27, true);
                        WriteLiteral("            </ul>\r\n        ");
                        EndContext();
                    }
                    );
                    __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
                    __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(3182, 6, true);
                    WriteLiteral("\r\n    ");
                    EndContext();
                }
                );
                __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
                __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3194, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3202, 2, true);
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserCardItemViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
