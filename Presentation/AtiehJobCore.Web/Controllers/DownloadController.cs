using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Media;
using AtiehJobCore.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AtiehJobCore.Web.Controllers
{
    public partial class DownloadController : BasePublicController
    {
        private readonly IDownloadService _downloadService;
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;
        private readonly UserSettings _userSettings;

        public DownloadController(IDownloadService downloadService,
            IWorkContext workContext,
            ILocalizationService localizationService,
            UserSettings userSettings)
        {
            this._downloadService = downloadService;
            this._workContext = workContext;
            this._localizationService = localizationService;
            this._userSettings = userSettings;
        }

        public virtual IActionResult GetFileUpload(Guid downloadId)
        {
            var download = _downloadService.GetDownloadByGuid(downloadId);
            if (download == null)
                return Content("Download is not available any more.");

            if (download.UseDownloadUrl)
                return new RedirectResult(download.DownloadUrl);

            //binary download
            if (download.DownloadBinary == null)
                return Content("Download data is not available any more.");

            //return result
            var fileName = !string.IsNullOrWhiteSpace(download.Filename) ? download.Filename : downloadId.ToString();
            var contentType = !string.IsNullOrWhiteSpace(download.ContentType) ? download.ContentType : "application/octet-stream";
            return new FileContentResult(download.DownloadBinary, contentType) { FileDownloadName = fileName + download.Extension };
        }


        public virtual IActionResult GetUserNoteFile(string userNoteId,
            [FromServices] IUserService userService)
        {
            if (string.IsNullOrEmpty(userNoteId))
                return Content("Download is not available.");

            var userNote = userService.GetUserNote(userNoteId);
            if (userNote == null)
                return InvokeHttp404();

            if (_workContext.CurrentUser == null || userNote.UserId != _workContext.CurrentUser.Id)
                return Challenge();

            var download = _downloadService.GetDownloadById(userNote.DownloadId);
            if (download == null)
                return Content("Download is not available any more.");

            if (download.UseDownloadUrl)
                return new RedirectResult(download.DownloadUrl);

            //binary download
            if (download.DownloadBinary == null)
                return Content("Download data is not available any more.");

            //return result
            var fileName = !string.IsNullOrWhiteSpace(download.Filename) ? download.Filename : userNote.Id.ToString();
            var contentType = !string.IsNullOrWhiteSpace(download.ContentType) ? download.ContentType : "application/octet-stream";
            return new FileContentResult(download.DownloadBinary, contentType) { FileDownloadName = fileName + download.Extension };
        }


    }
}
