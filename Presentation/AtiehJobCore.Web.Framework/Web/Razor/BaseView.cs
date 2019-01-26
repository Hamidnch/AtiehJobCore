using System.Threading.Tasks;
using AtiehJobCore.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace AtiehJobCore.Web.Framework.Web.Razor
{
    //public abstract class BaseView : RazorPage<object>
    //{
    //}

    public abstract class BaseView<TModel> : RazorPage<TModel>
    {
        public bool IsAuthenticated()
        {
            return Context.User.Identity.IsAuthenticated;
        }

        public bool IsAdminRole()
        {
            return Context.User.IsInRole(AreaNames.Admin);
        }

        [RazorInject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public bool IsRtl()
        {
            var isRtl = HttpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>()
               .RequestCulture.UICulture.TextInfo.IsRightToLeft;
            return isRtl;
        }

#pragma warning disable 1998
        public override Task ExecuteAsync()
        {
            throw new System.NotImplementedException();
        }
#pragma warning disable 1998
    }
}
