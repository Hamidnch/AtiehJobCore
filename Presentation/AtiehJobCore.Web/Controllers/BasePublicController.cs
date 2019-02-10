using AtiehJobCore.Web.Framework.Controllers;
using AtiehJobCore.Web.Framework.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Controllers
{
    [WwwRequirement]
    [CheckAccessSite]
    [CheckAccessClosedSite]
    [CheckLanguageSeoCode]
    public class BasePublicController : BaseController
    {
        protected virtual IActionResult InvokeHttp404()
        {
            Response.StatusCode = 404;
            return new EmptyResult();
        }
    }
}
