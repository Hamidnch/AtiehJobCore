using Microsoft.AspNetCore.Mvc.Razor;

namespace AtiehJobCore.Web.Framework.Helpers
{
    public abstract class BaseView : RazorPage<object>
    {
        public bool IsAuthenticated()
        {
            return Context.User.Identity.IsAuthenticated;
        }
    }

    public abstract class MyBasePage<TModel> : RazorPage<TModel>
    {
        // [RazorInject]
        //public IMyService MyService { get; set; }

        //public MyConfiguration Conf => new MyConfiguration();
    }
}
