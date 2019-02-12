using System;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Web.Framework.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Components
{
    public class EuCookieLawViewComponent : BaseViewComponent
    {
        private readonly StoreInformationSettings _storeInformationSettings;
        private readonly IWorkContext _workContext;

        public EuCookieLawViewComponent(StoreInformationSettings storeInformationSettings,
            IWorkContext workContext)
        {
            _storeInformationSettings = storeInformationSettings;
            _workContext = workContext;
        }

        public IViewComponentResult Invoke()
        {
            if (!_storeInformationSettings.DisplayEuCookieLawWarning)
                //disabled
                return Content("");

            var user = _workContext.CurrentUser;
            //ignore search engines because some pages could be indexed with the EU cookie as description
            if (user.IsSearchEngineAccount())
                return Content("");

            if (user.GetAttribute<bool>(SystemUserAttributeNames.EuCookieLawAccepted))
                //already accepted
                return Content("");

            //ignore notification?
            //right now it's used during logout so popup window is not displayed twice
            if (TempData["AtiehJob.IgnoreEuCookieLawWarning"] != null
             && Convert.ToBoolean(TempData["AtiehJob.IgnoreEuCookieLawWarning"]))
                return Content("");

            return View();

        }
    }
}
