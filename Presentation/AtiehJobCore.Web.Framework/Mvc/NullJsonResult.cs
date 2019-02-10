using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Framework.Mvc
{
    public class NullJsonResult : JsonResult
    {
        public NullJsonResult() : base(null)
        {
        }
    }
}
