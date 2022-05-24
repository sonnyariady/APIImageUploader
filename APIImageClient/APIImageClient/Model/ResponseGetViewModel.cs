using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIImageClient.Model
{
    public class ResponseGetViewModel
    {
        public String HasilRespon { get; set; }
        public String ErrorMessage { get; set; }

        public ResponseGetViewModel()
        {
            this.ErrorMessage = "";
            this.HasilRespon = "";
        }
    }
}
