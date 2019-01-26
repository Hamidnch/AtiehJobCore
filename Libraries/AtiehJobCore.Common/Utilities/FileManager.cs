using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AtiehJobCore.Common.Utilities
{
    public class FileManager
    {
        public async Task<bool> UploadFile(IFormFile file, IHostingEnvironment environment)
        {
            try
            {
                //1 check if the file length is greater than 0 bytes 

                if (file.Length <= 0) return false;

                var fileName = file.FileName;
                //2 Get the extension of the file
                var extension = Path.GetExtension(fileName);
                //3 check the file extension as png
                if (extension == ".png" || extension == ".jpg")
                {
                    //4 set the path where file will be copied
                    var filePath = Path.Combine(environment.WebRootPath, "UploadedFiles\\");
                    //5 copy the file to the path
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    throw new Exception("File must be either .png or .JPG");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
