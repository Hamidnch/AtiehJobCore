#pragma checksum "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cfd1c206cb24fb29b8233ead8c0553aee193dc45"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Views_RolesManager_Index), @"mvc.1.0.view", @"/Areas/Identity/Views/RolesManager/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Identity/Views/RolesManager/Index.cshtml", typeof(AspNetCore.Areas_Identity_Views_RolesManager_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cfd1c206cb24fb29b8233ead8c0553aee193dc45", @"/Areas/Identity/Views/RolesManager/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8ce39622f916d9215a96d4e30e917e84a1f76b", @"/Areas/Identity/Views/_ViewImports.cshtml")]
    public class Areas_Identity_Views_RolesManager_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<RoleAndUsersCountViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("alert alert-warning mt-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-header"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AllRolesList", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-body"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card mt-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
  
    ViewData["Title"] = "مدیریت نقش‌های سیستم";

#line default
#line hidden
            BeginContext(104, 1472, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cfd1c206cb24fb29b8233ead8c0553aee193dc458151", async() => {
                BeginContext(142, 1428, true);
                WriteLiteral(@"
    <a href=""#"" class=""close"" data-dismiss=""alert"">×</a>
    <ul>
        <li>
            ویرایش نام نقش‌های ثابت سیستم و یا حذف آن‌ها می‌تواند دسترسی به قسمت‌های از پیش طراحی شده‌ی برنامه را از کار بیندازد
            <span dir=""ltr"">.([Authorize(Roles = RoleNames.Admin)])</span>
            بنابراین هرگونه تغییری در اینجا نیاز است در کدهای برنامه نیز منعکس شود و یا برعکس.
        </li>
        <li>
            در اینجا می‌توان به هر نقش ثابت، دسترسی‌های پویایی را نیز اعطاء کرد. در این حالت کنترلر و یا اکشن متدهایی با پالیسی سطوح دسترسی پویا، لیست شده و قابل انتخاب خواهند بود
            <span dir=""ltr"">.([Authorize(Policy = PolicyNames.DynamicPermission)])</span>
        </li>
        <li>
            در حالت استفاده‌ی از سطوح دسترسی پویا، با تغییر نام و یا حذف نقش‌های ثابت، نیازی به تغییری در کدهای برنامه نخواهد بود.
        </li>
        <li>تمام کاربران منتسب به نقش Admin، به صفحات دارای سطوح دسترسی پویا نیز دسترسی کاملی دارند و نیازی به افزودن آن‌ها به لیست نفرات این نوع نقش‌های پویای خ");
                WriteLiteral(@"اص نیست.</li>
        <li>اگر در یک فیلتر Authorize، نقش جدیدی را بکار گرفته‌اید، می‌توانید آن‌را در اینجا اضافه کنید.</li>
        <li>اگر نام نقش بکار رفته‌ی در فیلترهای Authorize را تغییر داده‌اید، می‌توانید این تغییرات را نیز در اینجا اعمال و ویرایش نمائید.</li>
        <li>اگر از فیلترهای Authorize، نقشی را به طور کامل حذف کرده‌اید، می‌توانید این نقش را در اینجا نیز حذف کنید.</li>
    </ul>
");
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
            BeginContext(1576, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1578, 358, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cfd1c206cb24fb29b8233ead8c0553aee193dc4510866", async() => {
                BeginContext(1601, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(1607, 93, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cfd1c206cb24fb29b8233ead8c0553aee193dc4511253", async() => {
                    BeginContext(1632, 33, true);
                    WriteLiteral("\r\n        <h5 class=\"card-title\">");
                    EndContext();
                    BeginContext(1666, 17, false);
#line 28 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
                          Write(ViewData["Title"]);

#line default
#line hidden
                    EndContext();
                    BeginContext(1683, 11, true);
                    WriteLiteral("</h5>\r\n    ");
                    EndContext();
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
                BeginContext(1700, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(1706, 91, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cfd1c206cb24fb29b8233ead8c0553aee193dc4513056", async() => {
                    BeginContext(1729, 10, true);
                    WriteLiteral("\r\n        ");
                    EndContext();
                    BeginContext(1739, 46, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cfd1c206cb24fb29b8233ead8c0553aee193dc4513468", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#line 31 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

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
                    BeginContext(1785, 6, true);
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
                BeginContext(1797, 133, true);
                WriteLiteral("\r\n    <footer class=\"card-footer\">\r\n        <a class=\"btn btn-success\" href=\"#\" id=\"btnCreate\">ایجاد یک نقش جدید</a>\r\n    </footer>\r\n");
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
            BeginContext(1936, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(1961, 267, true);
                WriteLiteral(@"
    <script type=""text/javascript"">
        $(function () {
            $('#btnCreate').click(function (e) {
                e.preventDefault(); //می‌خواهیم لینک به صورت معمول عمل نکند

                $.bootstrapModalAjaxForm({
                    postUrl: '");
                EndContext();
                BeginContext(2229, 37, false);
#line 45 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
                         Write(Url.Action("AddRole", "RolesManager"));

#line default
#line hidden
                EndContext();
                BeginContext(2266, 52, true);
                WriteLiteral("\',\r\n                    renderModalPartialViewUrl: \'");
                EndContext();
                BeginContext(2319, 40, false);
#line 46 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
                                           Write(Url.Action("RenderRole", "RolesManager"));

#line default
#line hidden
                EndContext();
                BeginContext(2359, 689, true);
                WriteLiteral(@"',
                    renderModalPartialViewData: {},
                    loginUrl: '/identity/Account/login',
                    beforePostHandler: function () {
                    },
                    completeHandler: function () {
                        location.reload();
                    },
                    errorHandler: function () {
                    }
                });
            });

            $(""a[id^='btnEdit']"").click(function (e) {
                e.preventDefault(); //می‌خواهیم لینک به صورت معمول عمل نکند
                var roleId = $(this).data(""edit-id"");

                $.bootstrapModalAjaxForm({
                    postUrl: '");
                EndContext();
                BeginContext(3049, 38, false);
#line 64 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
                         Write(Url.Action("EditRole", "RolesManager"));

#line default
#line hidden
                EndContext();
                BeginContext(3087, 52, true);
                WriteLiteral("\',\r\n                    renderModalPartialViewUrl: \'");
                EndContext();
                BeginContext(3140, 40, false);
#line 65 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
                                           Write(Url.Action("RenderRole", "RolesManager"));

#line default
#line hidden
                EndContext();
                BeginContext(3180, 715, true);
                WriteLiteral(@"',
                    renderModalPartialViewData: JSON.stringify({ ""id"": roleId }),
                    loginUrl: '/identity/login',
                    beforePostHandler: function () {
                    },
                    completeHandler: function () {
                        location.reload();
                    },
                    errorHandler: function () {
                    }
                });
            });

            $(""a[id^='btnDelete']"").click(function (e) {
                e.preventDefault(); //می‌خواهیم لینک به صورت معمول عمل نکند
                var roleId = $(this).data(""delete-id"");

                $.bootstrapModalAjaxForm({
                    postUrl: '");
                EndContext();
                BeginContext(3896, 36, false);
#line 83 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
                         Write(Url.Action("Delete", "RolesManager"));

#line default
#line hidden
                EndContext();
                BeginContext(3932, 52, true);
                WriteLiteral("\',\r\n                    renderModalPartialViewUrl: \'");
                EndContext();
                BeginContext(3985, 46, false);
#line 84 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\RolesManager\Index.cshtml"
                                           Write(Url.Action("RenderDeleteRole", "RolesManager"));

#line default
#line hidden
                EndContext();
                BeginContext(4031, 473, true);
                WriteLiteral(@"',
                    renderModalPartialViewData: JSON.stringify({ ""id"": roleId }),
                    loginUrl: '/identity/login',
                    beforePostHandler: function () {
                    },
                    completeHandler: function () {
                        location.reload();
                    },
                    errorHandler: function () {
                    }
                });
            });
        });
    </script>
");
                EndContext();
            }
            );
            BeginContext(4507, 2, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<RoleAndUsersCountViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
