using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageValidationAPI.Models
{
    public class FileUploadModel
    {
        public string FileName { get; set; }
        public string Base64String { get; set; }
    }
}