using ImageValidationAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageValidationAPI.Services
{
    public class ImageService : BasicService
    {
        private LogService _LogService; 
        public ImageService()
        {
            this._LogService = new LogService();
        }
        public PostCommandResult UploadImage(FileUploadModel input)
        {
            PostCommandResult result = new PostCommandResult();
            string timespanstr = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string namafile = input.FileName;
            string namafileplustimespan = timespanstr + "_" + input.FileName;
            try
            {
                
                if (!IsImageValid(input.Base64String))
                {
                    throw new Exception("File image is not valid. Please enter the correct image, then try again!");
                }

                byte[] bytefile = Convert.FromBase64String(input.Base64String);

                long besarfile = bytefile.LongLength;
                double besarMB = (besarfile / 1024f) / 1024f;

                if (besarMB > Convert.ToDouble(this.MaxFileSize))
                {
                    throw new Exception("File image can't more than " + this.MaxFileSize + " MB!");
                }

                List<string> lstext = this.AllowedExtension.Split(',').ToList();
                List<string> lstsplit = input.FileName.Split('.').ToList();

                string extdarinama = lstsplit[lstsplit.Count() - 1]; //ekstensi file


                bool isValidExt = lstext.Contains(extdarinama.ToLower().Trim());
                

                if (!isValidExt)
                {
                    throw new Exception("File extention must be " + string.Join(",", lstext) + "!");
                }

               

                if (!Directory.Exists(this.FolderUpload))
                {
                    Directory.CreateDirectory(this.FolderUpload);
                }

                string fullpath = this.FolderUpload + namafileplustimespan;
                File.WriteAllBytes(fullpath, bytefile);
                _LogService.WriteLog("Success upload file : " + namafile);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.IsSuccess = false;
                result.StackTraceError = ex.StackTrace;
                _LogService.WriteLog("Error upload file " + namafile + " : " + result.ErrorMessage + ", StackTrace: " + result.StackTraceError);
            }
            return result;
        }

        public bool IsImageValid(string base64string)
        {
            try
            {
                byte[] filebyte = Convert.FromBase64String(base64string);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}