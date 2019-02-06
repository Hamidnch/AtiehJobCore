﻿using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb.Domain.Common;
using AtiehJobCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AtiehJobCore.Web.Areas.Admin.Controllers
{
    public class BaseAdminController : BaseController
    {
        /// <summary>
        /// Save selected TAB index
        /// </summary>
        /// <param name="index">Index to save; null to automatically detect it</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected void SaveSelectedTabIndex(int? index = null, bool persistForTheNextRequest = true)
        {
            //keep this method synchronized with
            //"GetSelectedTabIndex" method of \Grand.Framework\ViewEngines\Razor\WebViewPage.cs
            if (!index.HasValue)
            {
                if (int.TryParse(this.Request.Form["selected-tab-index"], out var tmp))
                {
                    index = tmp;
                }
            }

            if (!index.HasValue)
            {
                return;
            }

            const string dataKey = "Grand.selected-tab-index";
            if (persistForTheNextRequest)
            {
                TempData[dataKey] = index;
            }
            else
            {
                ViewData[dataKey] = index;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a <see cref="T:System.Web.Mvc.JsonResult" /> object that serializes the specified object to JavaScript Object Notation (JSON) format using the content type, content encoding, and the JSON request behavior.
        /// </summary>
        /// <returns>
        /// The result object that serializes the specified object to JSON format.
        /// </returns>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        public override JsonResult Json(object data)
        {
            //use IsoDateFormat on writing JSON text to fix issue with dates in KendoUI grid
            //TODO rename setting
            var useIsoDateTime = EngineContext.Current.Resolve<AdminAreaSettings>().UseIsoDateTimeConverterInJson;
            var serializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = useIsoDateTime ? DateFormatHandling.IsoDateFormat : DateFormatHandling.MicrosoftDateFormat
            };

            return base.Json(data, serializerSettings);
        }
    }
}
