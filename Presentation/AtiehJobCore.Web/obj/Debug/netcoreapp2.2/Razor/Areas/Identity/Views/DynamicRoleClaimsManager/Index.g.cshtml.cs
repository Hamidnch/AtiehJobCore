#pragma checksum "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ecb45406000aaf19bc99713017aa2b80d3d61888"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Views_DynamicRoleClaimsManager_Index), @"mvc.1.0.view", @"/Areas/Identity/Views/DynamicRoleClaimsManager/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Identity/Views/DynamicRoleClaimsManager/Index.cshtml", typeof(AspNetCore.Areas_Identity_Views_DynamicRoleClaimsManager_Index))]
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
#line 1 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
using AtiehJobCore.Services.Constants;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ecb45406000aaf19bc99713017aa2b80d3d61888", @"/Areas/Identity/Views/DynamicRoleClaimsManager/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8ce39622f916d9215a96d4e30e917e84a1f76b", @"/Areas/Identity/Views/_ViewImports.cshtml")]
    public class Areas_Identity_Views_DynamicRoleClaimsManager_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DynamicRoleClaimsManagerViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("alert alert-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-header"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card mt-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "DynamicRoleClaimsManager", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-begin", new global::Microsoft.AspNetCore.Html.HtmlString("dataAjaxBegin"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-success", new global::Microsoft.AspNetCore.Html.HtmlString("dataAjaxSuccess"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-failure", new global::Microsoft.AspNetCore.Html.HtmlString("dataAjaxFailure"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("dynamicPermissions"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(80, 34, true);
            WriteLiteral("\n<h2>تنظیم سطوح دسترسی پویای نقش «");
            EndContext();
            BeginContext(115, 32, false);
#line 4 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
                            Write(Model.RoleIncludeRoleClaims.Name);

#line default
#line hidden
            EndContext();
            BeginContext(147, 8, true);
            WriteLiteral("»</h2>\n\n");
            EndContext();
            BeginContext(155, 572, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ecb45406000aaf19bc99713017aa2b80d3d6188811334", async() => {
                BeginContext(185, 536, true);
                WriteLiteral(@"
    <a href=""#"" class=""close"" data-dismiss=""alert"">×</a>
    <ul>
        <li>
            نقش ویژه‌ی Admin به تمام صفحات لیست شده‌ی در اینجا دسترسی کاملی دارد و هرگونه ویرایشی بر روی آن تاثیرگذار نخواهد بود.
        </li>
        <li>
            در اینجا می‌توان به هر نقش ثابت، دسترسی‌های پویایی را نیز اعطاء کرد. در این حالت کنترلر و یا اکشن متدهایی با پالیسی سطوح دسترسی پویا، لیست شده و قابل انتخاب خواهند بود
            <span dir=""ltr"">.([Authorize(Policy = ConstantPolicies.DynamicPermission)])</span>
        </li>
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
            BeginContext(727, 2, true);
            WriteLiteral("\n\n");
            EndContext();
            BeginContext(729, 2471, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ecb45406000aaf19bc99713017aa2b80d3d6188813118", async() => {
                BeginContext(758, 5, true);
                WriteLiteral("\n    ");
                EndContext();
                BeginContext(763, 2430, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ecb45406000aaf19bc99713017aa2b80d3d6188813503", async() => {
                    BeginContext(1047, 29, true);
                    WriteLiteral("\n        <input name=\"RoleId\"");
                    EndContext();
                    BeginWriteAttribute("value", " value=\"", 1076, "\"", 1115, 1);
#line 28 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
WriteAttributeValue("", 1084, Model.RoleIncludeRoleClaims.Id, 1084, 31, false);

#line default
#line hidden
                    EndWriteAttribute();
                    BeginContext(1116, 18, true);
                    WriteLiteral(" type=\"hidden\" />\n");
                    EndContext();
#line 29 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
         foreach (var controller in Model.SecuredControllerActions.OrderBy(x => x.ControllerDisplayName))
        {

#line default
#line hidden
                    BeginContext(1250, 12, true);
                    WriteLiteral("            ");
                    EndContext();
                    BeginContext(1262, 1736, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ecb45406000aaf19bc99713017aa2b80d3d6188814891", async() => {
                        BeginContext(1285, 17, true);
                        WriteLiteral("\n                ");
                        EndContext();
                        BeginContext(1302, 467, false);
                        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ecb45406000aaf19bc99713017aa2b80d3d6188815330", async() => {
                            BeginContext(1327, 45, true);
                            WriteLiteral("\n                    <h5 class=\"card-title\">\n");
                            EndContext();
#line 34 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
                          
                            var displayName = controller.ControllerDisplayName;
                            if(string.IsNullOrWhiteSpace(displayName))
                            {
                                displayName = controller.ToString();
                            }
                        

#line default
#line hidden
                            BeginContext(1705, 24, true);
                            WriteLiteral("                        ");
                            EndContext();
                            BeginContext(1730, 11, false);
#line 41 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
                   Write(displayName);

#line default
#line hidden
                            EndContext();
                            BeginContext(1741, 22, true);
                            WriteLiteral("</h5>\n                ");
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
                        BeginContext(1769, 58, true);
                        WriteLiteral("\n                <ul class=\"list-group list-group-flush\">\n");
                        EndContext();
#line 44 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
                     foreach (var action in controller.MvcActions.OrderBy(x => x.ActionDisplayName))
                    {
                        var isSelected = Model.RoleIncludeRoleClaims.RoleClaims
                            .Any(roleClaim => roleClaim.ClaimType == PolicyNames.DynamicPermissionClaimType &&
                                              roleClaim.ClaimValue == action.ActionId);
                        var selected = isSelected ? "checked" : "";
                        var selectedClass = isSelected ? "list-group-item-success" : "";
                        var actionDisplayName = action.ActionDisplayName;
                        if(string.IsNullOrWhiteSpace(actionDisplayName))
                        {
                            actionDisplayName = $"{action}::{action.ActionId}";
                        }

#line default
#line hidden
                        BeginContext(2665, 27, true);
                        WriteLiteral("                        <li");
                        EndContext();
                        BeginWriteAttribute("class", " class=\"", 2692, "\"", 2730, 2);
                        WriteAttributeValue("", 2700, "list-group-item", 2700, 15, true);
#line 56 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
WriteAttributeValue(" ", 2715, selectedClass, 2716, 14, false);

#line default
#line hidden
                        EndWriteAttribute();
                        BeginContext(2731, 89, true);
                        WriteLiteral(" role=\"presentation\">\n                            <input type=\"checkbox\" name=\"actionIds\"");
                        EndContext();
                        BeginWriteAttribute("value", " value=\"", 2820, "\"", 2844, 1);
#line 57 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
WriteAttributeValue("", 2828, action.ActionId, 2828, 16, false);

#line default
#line hidden
                        EndWriteAttribute();
                        BeginContext(2845, 1, true);
                        WriteLiteral(" ");
                        EndContext();
                        BeginContext(2847, 8, false);
#line 57 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
                                                                                        Write(selected);

#line default
#line hidden
                        EndContext();
                        BeginContext(2855, 32, true);
                        WriteLiteral(" />\n                            ");
                        EndContext();
                        BeginContext(2888, 17, false);
#line 58 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
                       Write(actionDisplayName);

#line default
#line hidden
                        EndContext();
                        BeginContext(2905, 31, true);
                        WriteLiteral("\n                        </li>\n");
                        EndContext();
#line 60 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
                    }

#line default
#line hidden
                        BeginContext(2958, 34, true);
                        WriteLiteral("                </ul>\n            ");
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
                    BeginContext(2998, 1, true);
                    WriteLiteral("\n");
                    EndContext();
#line 63 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\DynamicRoleClaimsManager\Index.cshtml"
        }

#line default
#line hidden
                    BeginContext(3009, 177, true);
                    WriteLiteral("\n        <button type=\"submit\" class=\"btn btn-info top15\">\n            اعمال تغییرات\n            <span aria-hidden=\"true\" class=\"fas fa-thumbs-up\"></span>\n        </button>\n    ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3193, 1, true);
                WriteLiteral("\n");
                EndContext();
            }
            );
            __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper = CreateTagHelper<global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_TagHelpers_VisibilityTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3200, 2, true);
            WriteLiteral("\n\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(3220, 420, true);
                WriteLiteral(@"
    <script type=""text/javascript"">
        $(document).ready(function () {
            $('input[name=""actionIds""]').click(function () {
                if ($(this).is(':checked')) {
                    $(this).parent().addClass('list-group-item-success');
                } else {
                    $(this).parent().removeClass('list-group-item-success');
                }
            });
        });
    </script>
");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DynamicRoleClaimsManagerViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591