using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageValidationAPI.Models
{
    public class PostCommandResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTraceError { get; set; }
        public PostCommandResult()
        {
            this.IsSuccess = true;
            this.ErrorMessage = string.Empty;
            this.StackTraceError = string.Empty;
        }
    }
}