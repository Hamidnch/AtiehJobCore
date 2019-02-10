using Microsoft.AspNetCore.Http;

namespace AtiehJobCore.Core.Securities
{
    public static class ElmahSecurity
    {
        public static bool CheckPermissionAction(HttpContext httpContext)
        {
            // می باشد؟ elamh کاربری جاری سیستم دارای نقش ادمین برای دسترسی به  
             //return ( httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("Admin")) ;

            // در این قسمت ما تنها برای نمایش آزمایشی میگوییم که دسترسی دارند
            return true;
        }
    }
}
