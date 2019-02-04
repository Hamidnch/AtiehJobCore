//using AtiehJobCore.Common.Infrastructure.MongoDb;
//using AtiehJobCore.Common.MongoDb.Domain;
//using AtiehJobCore.Common.MongoDb.Domain.Users;
//using AtiehJobCore.Services.Common;
//using AtiehJobCore.Web.Framework.Components;
//using Microsoft.AspNetCore.Mvc;
//using System;

//namespace AtiehJobCore.Web.Components
//{
//    public class EuCookieLawViewComponent : BaseViewComponent
//    {
//        private readonly StoreInformationSettings _storeInformationSettings;
//        private readonly IWorkContext _workContext;

//        public EuCookieLawViewComponent(StoreInformationSettings storeInformationSettings,
//            IWorkContext workContext)
//        {
//            this._storeInformationSettings = storeInformationSettings;
//            this._workContext = workContext;
//        }

//        public IViewComponentResult Invoke()
//        {
//            if (!_storeInformationSettings.DisplayEuCookieLawWarning)
//                //disabled
//                return Content("");

//            var user = _workContext.CurrentUser;
//            //ignore search engines because some pages could be indexed with the EU cookie as description
//            if (user.IsSearchEngineAccount())
//                return Content("");

//            if (user.GetAttribute<bool>(SystemUserAttributeNames.EuCookieLawAccepted))
//                //already accepted
//                return Content("");

//            //ignore notification?
//            //right now it's used during logout so popup window is not displayed twice
//            if (TempData["AtiehJob.IgnoreEuCookieLawWarning"] != null && Convert.ToBoolean(TempData["AtiehJob.IgnoreEuCookieLawWarning"]))
//                return Content("");

//            return View();

//        }
//    }
//}
