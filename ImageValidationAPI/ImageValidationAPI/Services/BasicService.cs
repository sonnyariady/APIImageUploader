using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ImageValidationAPI.Services
{
    public class BasicService
    {
        protected string FolderUpload = WebConfigurationManager.AppSettings["FolderUpload"];
        protected string FolderLogFiles = WebConfigurationManager.AppSettings["FolderLogFiles"];
        protected string MaxFileSize = WebConfigurationManager.AppSettings["MaxFileSize"];
        protected string AllowedExtension = WebConfigurationManager.AppSettings["AllowedExtension"];
        protected string KunciEnkrip = WebConfigurationManager.AppSettings["KunciEnkrip"];
    }
}