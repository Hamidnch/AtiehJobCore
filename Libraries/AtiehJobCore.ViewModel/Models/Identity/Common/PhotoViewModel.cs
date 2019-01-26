using System.ComponentModel.DataAnnotations;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Http;

namespace AtiehJobCore.ViewModel.Models.Identity.Common
{
    public class PhotoViewModel
    {
        [Required(ErrorMessage = "لطفا یک تصویر انتخاب کنید.")]
        //`FileExtensions` needs to be applied to a string property.
        //It doesn't work on IFormFile properties, and definitely not on IEnumerable<IFormFile> properties.
        //[FileExtensions(Extensions = ".png,.jpg,.jpeg,.gif", ErrorMessage = "Please upload an image file.")]
        [UploadFileExtensions(".png,.jpg,.jpeg,.gif", ErrorMessage = "لطفا فایل تصویر را بدرستی انتخاب کنید.")]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
    }
}
