#pragma checksum "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "103521d84ff024848ae410f5a6b461a194f1bc02"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Views_UserCard_Index), @"mvc.1.0.view", @"/Areas/Identity/Views/UserCard/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Identity/Views/UserCard/Index.cshtml", typeof(AspNetCore.Areas_Identity_Views_UserCard_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"103521d84ff024848ae410f5a6b461a194f1bc02", @"/Areas/Identity/Views/UserCard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8ce39622f916d9215a96d4e30e917e84a1f76b", @"/Areas/Identity/Views/_ViewImports.cshtml")]
    public class Areas_Identity_Views_UserCard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserCardItemViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_UserCardItem", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(29, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 3 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\Index.cshtml"
  
    ViewData["Title"] = $"برگه مشخصات {Model.User.DisplayName}";

#line default
#line hidden
            BeginContext(100, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(101, 46, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "103521d84ff024848ae410f5a6b461a194f1bc026778", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 7 "H:\Programming\Best Project\AtiehJobCore\Presentation\AtiehJobCore.Web\Areas\Identity\Views\UserCard\Index.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserCardItemViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
