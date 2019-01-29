#pragma checksum "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eab5a1eff7ca315f883b237ba8c1c1b293b806d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Views_UsersManager__UsersList), @"mvc.1.0.view", @"/Areas/Identity/Views/UsersManager/_UsersList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Identity/Views/UsersManager/_UsersList.cshtml", typeof(AspNetCore.Areas_Identity_Views_UsersManager__UsersList))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Areas.Identity;

#line default
#line hidden
#line 2 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Common.Constants;

#line default
#line hidden
#line 3 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Account;

#line default
#line hidden
#line 4 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Settings;

#line default
#line hidden
#line 5 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Password;

#line default
#line hidden
#line 6 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Emails;

#line default
#line hidden
#line 7 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.Services.Identity.Interfaces;

#line default
#line hidden
#line 8 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using AtiehJobCore.ViewModel.Models.Identity.Common;

#line default
#line hidden
#line 9 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Razor;

#line default
#line hidden
#line 10 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Html;

#line default
#line hidden
#line 11 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Routing;

#line default
#line hidden
#line 12 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#line 13 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Globalization;

#line default
#line hidden
#line 14 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Threading.Tasks;

#line default
#line hidden
#line 15 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using System.Net;

#line default
#line hidden
#line 16 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\_ViewImports.cshtml"
using DNTPersianUtils.Core;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eab5a1eff7ca315f883b237ba8c1c1b293b806d7", @"/Areas/Identity/Views/UsersManager/_UsersList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8ce39622f916d9215a96d4e30e917e84a1f76b", @"/Areas/Identity/Views/_ViewImports.cshtml")]
    public class Areas_Identity_Views_UsersManager__UsersList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PagedUsersListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Areas/Identity/Views/UsersManager/_SearchUsers.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Areas/Identity/Views/UserCard/_UserCardItem.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("alert alert-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pagenumber-param", "page", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-ajax-target", "#gridcontainer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "UsersManager", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-first-page-text", "ابتدا", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-last-page-text", "انتها", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pager-li-current-class", "active", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pager-li-non-active-class", "disabled", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pager-ul-class", "pagination", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pager-li-current-class", new global::Microsoft.AspNetCore.Html.HtmlString("page-item active"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_13 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pager-li-other-class", "page-item", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_14 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pager-li-non-active-class", new global::Microsoft.AspNetCore.Html.HtmlString("page-item disabled"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_15 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pager-link-current-class", "page-link", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_16 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("cs-pager-link-other-class", "page-link", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper;
        private global::cloudscribe.Web.Pagination.PagerTagHelper __cloudscribe_Web_Pagination_PagerTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
  
    ViewData["Title"] = "مدیریت کاربران";

#line default
#line hidden
            BeginContext(82, 109, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "eab5a1eff7ca315f883b237ba8c1c1b293b806d711963", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 5 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = new SearchUsersViewModel();

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(191, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 6 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
 if (Model.Users.Any())
{
    foreach (var user in Model.Users)
    {

#line default
#line hidden
            BeginContext(267, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(275, 289, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eab5a1eff7ca315f883b237ba8c1c1b293b806d714023", async() => {
                BeginContext(303, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
                BeginContext(317, 231, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "eab5a1eff7ca315f883b237ba8c1c1b293b806d714418", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 12 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = new UserCardItemViewModel { User = user, ShowAdminParts = true, Roles = Model.Roles, ActiveTab = UserCardItemActiveTab.UserInfo };

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(548, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 284, "usercard-", 284, 9, true);
#line 10 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
AddHtmlAttributeValue("", 293, user.Id, 293, 8, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(564, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 14 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
    }
}
else
{

#line default
#line hidden
            BeginContext(585, 4, true);
            WriteLiteral("    ");
            EndContext();
            BeginContext(589, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eab5a1eff7ca315f883b237ba8c1c1b293b806d717906", async() => {
                BeginContext(622, 16, true);
                WriteLiteral("کاربری یافت نشد.");
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
            BeginContext(644, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 19 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
}

#line default
#line hidden
            BeginContext(649, 1107, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eab5a1eff7ca315f883b237ba8c1c1b293b806d719320", async() => {
                BeginContext(654, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(660, 1088, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("cs-pager", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eab5a1eff7ca315f883b237ba8c1c1b293b806d719707", async() => {
                }
                );
                __cloudscribe_Web_Pagination_PagerTagHelper = CreateTagHelper<global::cloudscribe.Web.Pagination.PagerTagHelper>();
                __tagHelperExecutionContext.Add(__cloudscribe_Web_Pagination_PagerTagHelper);
#line 21 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
__cloudscribe_Web_Pagination_PagerTagHelper.PageSize = Model.Paging.ItemsPerPage;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("cs-paging-pagesize", __cloudscribe_Web_Pagination_PagerTagHelper.PageSize, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 22 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
__cloudscribe_Web_Pagination_PagerTagHelper.PageNumber = Model.Paging.CurrentPage;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("cs-paging-pagenumber", __cloudscribe_Web_Pagination_PagerTagHelper.PageNumber, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 23 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
__cloudscribe_Web_Pagination_PagerTagHelper.TotalItems = Model.Paging.TotalItems;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("cs-paging-totalitems", __cloudscribe_Web_Pagination_PagerTagHelper.TotalItems, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __cloudscribe_Web_Pagination_PagerTagHelper.PageNumberParam = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __cloudscribe_Web_Pagination_PagerTagHelper.AjaxTarget = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                __cloudscribe_Web_Pagination_PagerTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __cloudscribe_Web_Pagination_PagerTagHelper.Action = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                if (__cloudscribe_Web_Pagination_PagerTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-pagesize", "cloudscribe.Web.Pagination.PagerTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#line 28 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
                      WriteLiteral(Model.Paging.ItemsPerPage);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __cloudscribe_Web_Pagination_PagerTagHelper.RouteValues["pagesize"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pagesize", __cloudscribe_Web_Pagination_PagerTagHelper.RouteValues["pagesize"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 29 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
__cloudscribe_Web_Pagination_PagerTagHelper.ShowFirstLast = true;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("cs-show-first-last", __cloudscribe_Web_Pagination_PagerTagHelper.ShowFirstLast, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 30 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
__cloudscribe_Web_Pagination_PagerTagHelper.SuppressEmptyNextPrev = true;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("cs-suppress-empty-nextprev", __cloudscribe_Web_Pagination_PagerTagHelper.SuppressEmptyNextPrev, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 31 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UsersManager\_UsersList.cshtml"
__cloudscribe_Web_Pagination_PagerTagHelper.SuppressInActiveFirstLast = true;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("cs-suppress-inactive-firstlast", __cloudscribe_Web_Pagination_PagerTagHelper.SuppressInActiveFirstLast, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __cloudscribe_Web_Pagination_PagerTagHelper.FirstPageText = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                __cloudscribe_Web_Pagination_PagerTagHelper.LastPageText = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
                __cloudscribe_Web_Pagination_PagerTagHelper.LiCurrentCssClass = (string)__tagHelperAttribute_9.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
                __cloudscribe_Web_Pagination_PagerTagHelper.LiNonActiveCssClass = (string)__tagHelperAttribute_10.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
                __cloudscribe_Web_Pagination_PagerTagHelper.UlCssClass = (string)__tagHelperAttribute_11.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
                __cloudscribe_Web_Pagination_PagerTagHelper.LiOtherCssClass = (string)__tagHelperAttribute_13.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
                __cloudscribe_Web_Pagination_PagerTagHelper.LinkCurrentCssClass = (string)__tagHelperAttribute_15.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
                __cloudscribe_Web_Pagination_PagerTagHelper.LinkOtherCssClass = (string)__tagHelperAttribute_16.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1748, 2, true);
                WriteLiteral("\r\n");
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
            BeginContext(1756, 2, true);
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PagedUsersListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
