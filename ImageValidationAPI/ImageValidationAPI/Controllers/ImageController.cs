using ImageValidationAPI.Models;
using ImageValidationAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImageValidationAPI.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        public PostCommandResult UploadImage([FromBody] FileUploadModel input)
        {
            ImageService service = new ImageService();
            return service.UploadImage(input);
        }

        [HttpPost]
        public PostCommandResult UploadImageEnc([FromBody] APIParameter param)
        {
            ImageService service = new ImageService();
            LogService logService = new LogService();
            PostCommandResult result = new PostCommandResult();
            try
            {
                AESEncryptService encryptService = new AESEncryptService();
                string teksdekrip = encryptService.Decrypt(param.input);
                //hasilnya diserialize
                FileUploadModel inputfile = JsonConvert.DeserializeObject<FileUploadModel>(teksdekrip);
                result = service.UploadImage(inputfile);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Invalid key";
                result.StackTraceError = ex.StackTrace;
                logService.WriteLog("Failed to be processed because of error Invalid Key!");
            }
            return result;
        }

    }
}
