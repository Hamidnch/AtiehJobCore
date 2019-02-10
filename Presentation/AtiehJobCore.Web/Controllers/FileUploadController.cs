using System;
using System.IO;
using System.Threading.Tasks;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Core.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Controllers
{
    public class FileUploadController : BasePublicController
    {
        private readonly FileManager _fileManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileUploadController(FileManager fileManager,
            IHostingEnvironment hostingEnvironment)
        {
            _fileManager = fileManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var uploadSuccess = await _fileManager.UploadFile(file, _hostingEnvironment);
                ViewBag.Message = "File Uploaded Successfully";
                return View("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)] //50MB
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPhoto(PhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var formFile = model.Photo;
                if (formFile == null || formFile.Length == 0)
                {
                    ModelState.AddModelError("", "فایل آپلود شده نال یا خالی هست");
                    return View("Index");
                }

                var uploadRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, "UploadedFiles");
                if (!Directory.Exists(uploadRootFolder))
                    Directory.CreateDirectory(uploadRootFolder);

                var filePath = Path.Combine(uploadRootFolder, formFile.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }

                RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}
