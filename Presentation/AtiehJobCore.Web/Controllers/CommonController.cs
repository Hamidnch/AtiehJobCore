using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain;
using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Domain.Media;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Services.Media;
using AtiehJobCore.Services.Messages;
using AtiehJobCore.Web.Framework.Filters;
using AtiehJobCore.Web.Framework.Models.Common;
using AtiehJobCore.Web.Framework.Mvc.Captcha;
using AtiehJobCore.Web.Framework.Security;
using AtiehJobCore.Web.Framework.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace AtiehJobCore.Web.Controllers
{
    public partial class CommonController : BasePublicController
    {
        #region Fields
        private readonly ICommonViewModelService _commonViewModelService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IUserActivityService _userActivityService;
        private readonly IContactAttributeService _contactAttributeService;
        private readonly IPopupService _popupService;
        private readonly CommonSettings _commonSettings;
        private readonly CaptchaSettings _captchaSettings;

        #endregion

        #region Constructors

        public CommonController(
            ICommonViewModelService commonViewModelService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IUserActivityService userActivityService,
            CommonSettings commonSettings,
            CaptchaSettings captchaSettings,
            IContactAttributeService contactAttributeService,
            IPopupService popupService)
        {
            this._commonViewModelService = commonViewModelService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._userActivityService = userActivityService;
            this._commonSettings = commonSettings;
            this._captchaSettings = captchaSettings;
            _contactAttributeService = contactAttributeService;
            _popupService = popupService;
        }

        #endregion

        #region Methods

        //page not found
        public virtual IActionResult PageNotFound([FromServices] ILogger logger)
        {
            if (_commonSettings.Log404Errors)
            {
                var statusCodeReExecuteFeature = HttpContext?.Features?.Get<IStatusCodeReExecuteFeature>();
                logger.Error(
                    $"Error 404. The requested page ({statusCodeReExecuteFeature?.OriginalPath}) was not found",
                    user: _workContext.CurrentUser);
            }

            this.Response.StatusCode = 404;
            this.Response.ContentType = "text/html";

            return View();
        }

        //available even when a store is closed
        [CheckAccessClosedSite(true)]
        //available even when navigation is not allowed
        [CheckAccessSite(true)]
        public virtual IActionResult SetLanguage(
            [FromServices] ILanguageService languageService,
            [FromServices] LocalizationSettings localizationSettings,
            string langid, string returnUrl = "")
        {

            var language = languageService.GetLanguageById(langid);
            if (!language?.Published ?? false)
                language = _workContext.WorkingLanguage;

            //home page
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = Url.RouteUrl("HomePage");

            //prevent open redirection attack
            if (!Url.IsLocalUrl(returnUrl))
                returnUrl = Url.RouteUrl("HomePage");

            //language part in URL
            if (localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
            {
                //remove current language code if it's already localized URL
                if (returnUrl.IsLocalizedUrl(this.Request.PathBase, true, out Language _))
                    returnUrl = returnUrl.RemoveLanguageSeoCodeFromUrl(this.Request.PathBase, true);

                //and add code of passed language
                returnUrl = returnUrl.AddLanguageSeoCodeToUrl(this.Request.PathBase, true, language);
            }

            _workContext.WorkingLanguage = language;

            return Redirect(returnUrl);
        }

        //helper method to redirect users.
        public virtual IActionResult InternalRedirect(string url, bool permanentRedirect)
        {
            //ensure it's invoked from our GenericPathRoute class
            if (HttpContext.Items["grand.RedirectFromGenericPathRoute"] == null ||
                !Convert.ToBoolean(HttpContext.Items["grand.RedirectFromGenericPathRoute"]))
            {
                url = Url.RouteUrl("HomePage");
                permanentRedirect = false;
            }

            //home page
            if (string.IsNullOrEmpty(url))
            {
                url = Url.RouteUrl("HomePage");
                permanentRedirect = false;
            }

            //prevent open redirection attack
            if (!Url.IsLocalUrl(url))
            {
                url = Url.RouteUrl("HomePage");
                permanentRedirect = false;
            }

            if (permanentRedirect)
                return RedirectPermanent(url);

            url = Uri.EscapeUriString(WebUtility.UrlDecode(url));

            return Redirect(url);
        }

        //contact us page
        //available even when a store is closed
        [CheckAccessClosedSite(true)]
        public virtual IActionResult ContactUs()
        {
            var model = _commonViewModelService.PrepareContactUs();
            return View(model);
        }

        [HttpPost, ActionName("ContactUs")]
        [PublicAntiForgery]
        [ValidateCaptcha]
        //available even when a store is closed
        [CheckAccessClosedSite(true)]
        public virtual IActionResult ContactUsSend(ContactUsModel model, IFormCollection form, bool captchaValid,
            [FromServices] IContactAttributeFormatter contactAttributeFormatter)
        {
            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnContactUsPage && !captchaValid)
            {
                ModelState.AddModelError("", _captchaSettings.GetWrongCaptchaMessage(_localizationService));
            }

            //parse contact attributes
            var attributeXml = _commonViewModelService.ParseContactAttributes(form);
            var contactAttributeWarnings = _commonViewModelService.GetContactAttributesWarnings(attributeXml);
            if (contactAttributeWarnings.Any())
            {
                foreach (var item in contactAttributeWarnings)
                {
                    ModelState.AddModelError("", item);
                }
            }

            if (ModelState.IsValid)
            {
                model.ContactAttributeXml = attributeXml;
                model.ContactAttributeInfo = contactAttributeFormatter.FormatAttributes(attributeXml, _workContext.CurrentUser);
                model = _commonViewModelService.SendContactUs(model);
                //activity log
                _userActivityService.InsertActivity("Site.ContactUs", "",
                    _localizationService.GetResource("ActivityLog.Site.ContactUs"));
                return View(model);
            }

            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnContactUsPage;
            model.ContactAttributes = _commonViewModelService.PrepareContactAttributeModel(attributeXml);

            return View(model);
        }


        [HttpPost]
        //available even when a store is closed
        [CheckAccessClosedSite(true)]
        //available even when navigation is not allowed
        [CheckAccessSite(true)]
        public virtual IActionResult EuCookieLawAccept([FromServices] StoreInformationSettings storeInformationSettings)
        {
            if (!storeInformationSettings.DisplayEuCookieLawWarning)
                //disabled
                return Json(new { stored = false });

            //save setting
            EngineContext.Current.Resolve<IGenericAttributeService>().SaveAttribute(_workContext.CurrentUser,
                SystemUserAttributeNames.EuCookieLawAccepted, true);
            return Json(new { stored = true });
        }

        public virtual IActionResult GenericUrl()
        {
            //seems that no entity was found
            return InvokeHttp404();
        }

        //site is closed
        //available even when a site is closed
        [CheckAccessClosedSite(true)]
        public virtual IActionResult SiteClosed()
        {
            return View();
        }

        [HttpPost]
        public virtual IActionResult ContactAttributeChange(IFormCollection form,
            [FromServices] IContactAttributeParser contactAttributeParser)
        {
            var attributeXml = _commonViewModelService.ParseContactAttributes(form);

            var enabledAttributeIds = new List<string>();
            var disabledAttributeIds = new List<string>();
            var attributes = _contactAttributeService.GetAllContactAttributes();
            foreach (var attribute in attributes)
            {
                var conditionMet = contactAttributeParser.IsConditionMet(attribute, attributeXml);
                if (!conditionMet.HasValue)
                {
                    continue;
                }

                if (conditionMet.Value)
                    enabledAttributeIds.Add(attribute.Id);
                else
                    disabledAttributeIds.Add(attribute.Id);
            }

            return Json(new
            {
                enabledattributeids = enabledAttributeIds.ToArray(),
                disabledattributeids = disabledAttributeIds.ToArray()
            });
        }

        [HttpPost]
        public virtual IActionResult UploadFileContactAttribute(string attributeId)
        {
            var attribute = _contactAttributeService.GetContactAttributeById(attributeId);
            if (attribute == null || attribute.AttributeControlType != AttributeControlType.FileUpload)
            {
                return Json(new
                {
                    success = false,
                    downloadGuid = Guid.Empty,
                });
            }

            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return Json(new
                {
                    success = false,
                    message = "No file uploaded",
                    downloadGuid = Guid.Empty,
                });
            }

            var fileBinary = httpPostedFile.GetDownloadBits();

            const string qqFileNameParameter = "qqfilename";
            var fileName = httpPostedFile.FileName;
            if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                fileName = Request.Form[qqFileNameParameter].ToString();
            //remove path (passed in IE)
            fileName = Path.GetFileName(fileName);

            var contentType = httpPostedFile.ContentType;

            var fileExtension = Path.GetExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            if (attribute.ValidationFileMaximumSize.HasValue)
            {
                //compare in bytes
                var maxFileSizeBytes = attribute.ValidationFileMaximumSize.Value * 1024;
                if (fileBinary.Length > maxFileSizeBytes)
                {
                    //when returning JSON the mime-type must be set to text/plain
                    //otherwise some browsers will pop-up a "Save As" dialog.
                    return Json(new
                    {
                        success = false,
                        message = string.Format(_localizationService.GetResource("ShoppingCart.MaximumUploadedFileSize"), attribute.ValidationFileMaximumSize.Value),
                        downloadGuid = Guid.Empty,
                    });
                }
            }

            var download = new Download
            {
                DownloadGuid = Guid.NewGuid(),
                UseDownloadUrl = false,
                DownloadUrl = "",
                DownloadBinary = fileBinary,
                ContentType = contentType,
                //we store filename without extension for downloads
                Filename = Path.GetFileNameWithoutExtension(fileName),
                Extension = fileExtension,
                IsNew = true
            };

            EngineContext.Current.Resolve<IDownloadService>().InsertDownload(download);

            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Json(new
            {
                success = true,
                message = _localizationService.GetResource("ShoppingCart.FileUploaded"),
                downloadUrl = Url.Action("GetFileUpload", "Download", new { downloadId = download.DownloadGuid }),
                downloadGuid = download.DownloadGuid,
            });
        }

        //Get banner for user
        [HttpGet]
        public virtual IActionResult GetActivePopup()
        {
            var result = _popupService.GetActivePopupByUserId(_workContext.CurrentUser.Id);
            if (result != null)
            {
                return Json
                    (
                        new { Id = result.Id, Body = result.Body, PopupTypeId = result.PopupTypeId }
                    );
            }
            else
                return Json
                    (
                        new { empty = "" }
                    );
        }

        [HttpPost]
        public virtual IActionResult RemovePopup(string id)
        {
            _popupService.MovePopupToArchive(id, _workContext.CurrentUser.Id);
            return Json("");
        }

        #endregion
    }
}
