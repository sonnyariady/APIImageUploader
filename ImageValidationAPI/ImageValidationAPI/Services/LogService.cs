using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageValidationAPI.Services
{
    public class LogService : BasicService
    {
        public LogService()
        {

        }

        public void WriteLog(string pesan)
        {
            try
            {
               if (!Directory.Exists(this.FolderLogFiles))
                {
                    Directory.CreateDirectory(this.FolderLogFiles);
                }

                string namafilelog = "Log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                string fullpathlog = this.FolderLogFiles + namafilelog;
                using (StreamWriter sw = File.AppendText(fullpathlog))
                {
                    sw.WriteLine(DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + ": " + pesan);
                    
                }

            }
            catch (Exception ex)
            {

             
            }
        }

    }
}