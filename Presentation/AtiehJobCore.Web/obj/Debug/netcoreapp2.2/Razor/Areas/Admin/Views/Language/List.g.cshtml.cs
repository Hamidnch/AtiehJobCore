#pragma checksum "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "43baebf91163b9f98cfb034af65423737251235a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Language_List), @"mvc.1.0.view", @"/Areas/Admin/Views/Language/List.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/Language/List.cshtml", typeof(AspNetCore.Areas_Admin_Views_Language_List))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using System.Net;

#line default
#line hidden
#line 2 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Routing;

#line default
#line hidden
#line 3 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#line 4 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using System.Globalization;

#line default
#line hidden
#line 5 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using System.Threading.Tasks;

#line default
#line hidden
#line 6 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using DNTPersianUtils.Core;

#line default
#line hidden
#line 7 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Framework.Models.Admin;

#line default
#line hidden
#line 8 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using AtiehJobCore.Services.Events;

#line default
#line hidden
#line 9 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Framework.Events;

#line default
#line hidden
#line 10 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;

#line default
#line hidden
#line 11 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\_ViewImports.cshtml"
using AtiehJobCore.Web.Framework.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43baebf91163b9f98cfb034af65423737251235a", @"/Areas/Admin/Views/Language/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"575cb792871ec30357c367140b6e04a5df57c40a", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Language_List : AtiehJobCore.Web.Framework.Web.Razor.BaseView<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("caption"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("portlet-title"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("languages-grid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("portlet-body"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-body"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("portlet-body form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("portlet light form-fit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-md-12"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("row"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::AtiehJobCore.Web.Framework.TagHelpers.AntiForgeryTokenTagHelper __AtiehJobCore_Web_Framework_TagHelpers_AntiForgeryTokenTagHelper;
        private global::AtiehJobCore.Web.TagHelpers.VisibilityTagHelper __AtiehJobCore_Web_TagHelpers_VisibilityTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
  
    //page title
    ViewBag.Title = T("Admin.Configuration.Languages").Text;
    Layout = "_AdminLayout";

#line default
#line hidden
            BeginContext(117, 21, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("antiforgery-token", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "43baebf91163b9f98cfb034af65423737251235a9118", async() => {
            }
            );
            __AtiehJobCore_Web_Framework_TagHelpers_AntiForgeryTokenTagHelper = CreateTagHelper<global::AtiehJobCore.Web.Framework.TagHelpers.AntiForgeryTokenTagHelper>();
            __tagHelperExecutionContext.Add(__AtiehJobCore_Web_Framework_TagHelpers_AntiForgeryTokenTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(138, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(140, 1278, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a10216", async() => {
                BeginContext(157, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(163, 1247, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a10603", async() => {
                    BeginContext(186, 10, true);
                    WriteLiteral("\r\n        ");
                    EndContext();
                    BeginContext(196, 947, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a11014", async() => {
                        BeginContext(232, 14, true);
                        WriteLiteral("\r\n            ");
                        EndContext();
                        BeginContext(246, 221, false);
                        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a11449", async() => {
                            BeginContext(273, 18, true);
                            WriteLiteral("\r\n                ");
                            EndContext();
                            BeginContext(291, 154, false);
                            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a11908", async() => {
                                BeginContext(312, 74, true);
                                WriteLiteral("\r\n                    <i class=\"fa fa-language\"></i>\r\n                    ");
                                EndContext();
                                BeginContext(387, 34, false);
#line 13 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
               Write(T("Admin.Configuration.Languages"));

#line default
#line hidden
                                EndContext();
                                BeginContext(421, 18, true);
                                WriteLiteral("\r\n                ");
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
                            BeginContext(445, 16, true);
                            WriteLiteral("\r\n\r\n            ");
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
                        BeginContext(467, 14, true);
                        WriteLiteral("\r\n            ");
                        EndContext();
                        BeginContext(481, 340, false);
                        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a15134", async() => {
                            BeginContext(512, 18, true);
                            WriteLiteral("\r\n                ");
                            EndContext();
                            BeginContext(530, 271, false);
                            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a15593", async() => {
                                BeginContext(559, 22, true);
                                WriteLiteral("\r\n                    ");
                                EndContext();
                                BeginContext(581, 196, false);
                                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a16076", async() => {
                                    BeginContext(604, 26, true);
                                    WriteLiteral("\r\n                        ");
                                    EndContext();
                                    BeginContext(630, 119, false);
                                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a16583", async() => {
                                        BeginContext(656, 30, true);
                                        WriteLiteral("\r\n                            ");
                                        EndContext();
                                        BeginContext(686, 31, false);
                                        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a17113", async() => {
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
                                        BeginContext(717, 26, true);
                                        WriteLiteral("\r\n                        ");
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
                                    BeginContext(749, 22, true);
                                    WriteLiteral("\r\n                    ");
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
                                BeginContext(777, 18, true);
                                WriteLiteral("\r\n                ");
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
                            BeginContext(801, 14, true);
                            WriteLiteral("\r\n            ");
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
                        BeginContext(821, 34, true);
                        WriteLiteral("\r\n            <br />\r\n            ");
                        EndContext();
                        BeginContext(855, 272, false);
                        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43baebf91163b9f98cfb034af65423737251235a23437", async() => {
                            BeginContext(884, 20, true);
                            WriteLiteral("\r\n                <a");
                            EndContext();
                            BeginWriteAttribute("href", " href=\"", 904, "\"", 932, 1);
#line 28 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
WriteAttributeValue("", 911, Url.Action("Create"), 911, 21, false);

#line default
#line hidden
                            EndWriteAttribute();
                            BeginContext(933, 119, true);
                            WriteLiteral(" class=\"btn text-white\">\r\n                    <i class=\"fa fa-plus\"></i>\r\n                    <span class=\"hidden-xs\"> ");
                            EndContext();
                            BeginContext(1053, 24, false);
#line 30 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
                                        Write(T("Admin.Common.AddNew"));

#line default
#line hidden
                            EndContext();
                            BeginContext(1077, 44, true);
                            WriteLiteral(" </span>\r\n                </a>\r\n            ");
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
                        BeginContext(1127, 10, true);
                        WriteLiteral("\r\n        ");
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
                    BeginContext(1143, 2, true);
                    WriteLiteral("\r\n");
                    EndContext();
                    BeginContext(1400, 4, true);
                    WriteLiteral("    ");
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
                BeginContext(1410, 2, true);
                WriteLiteral("\r\n");
                EndContext();
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
            BeginContext(1418, 209, true);
            WriteLiteral("\r\n\r\n<script>\r\n    $(document).ready(function () {\r\n        $(\"#languages-grid\").kendoGrid({\r\n            dataSource: {\r\n                transport: {\r\n                    read: {\r\n                        url: \"");
            EndContext();
            BeginContext(1628, 40, false);
#line 48 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
                         Write(Html.Raw(Url.Action("List", "Language")));

#line default
#line hidden
            EndContext();
            BeginContext(1668, 1086, true);
            WriteLiteral(@""",
                        type: ""POST"",
                        dataType: ""json"",
                        data: addAntiForgeryToken
                    }
                },
                schema: {
                    data: ""Data"",
                    total: ""Total"",
                    errors: ""Errors""
                },
                error: function(e) {
                    display_kendoui_grid_error(e);
                    // Cancel the changes
                    this.cancelChanges();
                },
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            },
            pageable: {
                refresh: true,
                numeric: false,
                previousNext: true,
                info: false
            },
            editable: {
                confirmation: false,
                mode: ""inline""
            },
            scrollable: false,
            columns: [
            {
       ");
            WriteLiteral("         field: \"FlagImageFileName\",\r\n                title: \"");
            EndContext();
            BeginContext(2755, 51, false);
#line 82 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
                   Write(T("Admin.Configuration.Languages.Fields.FlagImage"));

#line default
#line hidden
            EndContext();
            BeginContext(2806, 235, true);
            WriteLiteral("\",\r\n                width: 22,\r\n                attributes: { style: \"text-align:center\" },\r\n                headerAttributes: {\r\n                    style: \"text-align:center\"\r\n                },\r\n                template: \'<img src=\"");
            EndContext();
            BeginContext(3042, 27, false);
#line 88 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
                                Write(Url.Content("~/img/flags/"));

#line default
#line hidden
            EndContext();
            BeginContext(3069, 384, true);
            WriteLiteral(@"#=FlagImageFileName#"" />'
            },
            {
                field: ""Name"",
                width: 150,
    		    headerAttributes: {
					style: ""text-align:center""
                },
                attributes: { style: ""text-align:center"" },
				//attributes: {
				//	""class"": ""table-cell"",
				//style: ""text-align:center""
				//},
                title: """);
            EndContext();
            BeginContext(3454, 46, false);
#line 101 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
                   Write(T("Admin.Configuration.Languages.Fields.Name"));

#line default
#line hidden
            EndContext();
            BeginContext(3500, 346, true);
            WriteLiteral(@""",
                template: '<a class=""k-link"" href=""Edit/#=Id#"">#=Name#</a>',
            },
            {
                field: ""LanguageCulture"",
                headerAttributes: {
                    style: ""text-align:center""
                },
                attributes: { style: ""text-align:center"" },
                title: """);
            EndContext();
            BeginContext(3847, 57, false);
#line 110 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
                   Write(T("Admin.Configuration.Languages.Fields.LanguageCulture"));

#line default
#line hidden
            EndContext();
            BeginContext(3904, 279, true);
            WriteLiteral(@""",
                width: 200
            },{
                field: ""DisplayOrder"",
                headerAttributes: {
                    style: ""text-align:center""
                },
                attributes: { style: ""text-align:center"" },
                title: """);
            EndContext();
            BeginContext(4184, 54, false);
#line 118 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
                   Write(T("Admin.Configuration.Languages.Fields.DisplayOrder"));

#line default
#line hidden
            EndContext();
            BeginContext(4238, 216, true);
            WriteLiteral("\",\r\n                width: 100\r\n            }, {\r\n                field: \"Published\",\r\n                headerAttributes: {\r\n                    style: \"text-align:center\"\r\n                },\r\n                title: \"");
            EndContext();
            BeginContext(4455, 51, false);
#line 125 "D:\Programming New\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Admin\Views\Language\List.cshtml"
                   Write(T("Admin.Configuration.Languages.Fields.Published"));

#line default
#line hidden
            EndContext();
            BeginContext(4506, 406, true);
            WriteLiteral(@""",
                width: 100,
                headerAttributes: { style: ""text-align:center"" },
                attributes: { style: ""text-align:center"" },
                template: '# if(Published) {# <i class=""fa fa-check"" aria-hidden=""true"" style=""color:green""></i> #} else {# <i class=""fa fa-times"" aria-hidden=""true"" style=""color:red""></i> #} #'
            }]
        });
    });
</script>
");
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
