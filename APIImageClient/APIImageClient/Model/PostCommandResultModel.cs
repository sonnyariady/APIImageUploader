using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIImageClient.Model
{
    public class PostCommandResultModel
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTraceError { get; set; }
        public PostCommandResultModel()
        {
            this.IsSuccess = true;
            this.ErrorMessage = string.Empty;
            this.StackTraceError = string.Empty;
        }
    }
}
