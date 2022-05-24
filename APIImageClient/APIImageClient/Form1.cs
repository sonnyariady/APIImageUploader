using APIImageClient.Model;
using APIImageClient.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIImageClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("Please select file to be uploaded!");
                return;
            }

            try
            {

                byte[] fileinput = File.ReadAllBytes(txtPath.Text);
                long besarfile = fileinput.LongLength;
                double besarMB = (besarfile / 1024f) / 1024f;

                string urlreq = "Image/UploadImageEnc";

                string namafile = Path.GetFileName(txtPath.Text);
                string base64string = Convert.ToBase64String(fileinput);
                Dictionary<string, object> paramAPI = new Dictionary<string, object>();
                paramAPI.Add("FileName", namafile);
                paramAPI.Add("Base64String", base64string);
                WebAPIRequest req = new WebAPIRequest();
                string jsonstring1 = JsonConvert.SerializeObject(paramAPI);
                AESEncrypt myencrypt = new AESEncrypt();
                string jsonenkrip = myencrypt.Encrypt(jsonstring1);
                paramAPI.Clear();
                paramAPI.Add("input", jsonenkrip);
                string jsonenkriptosend = JsonConvert.SerializeObject(paramAPI);

                ResponseGetViewModel resp = req.RequestPost(urlreq, jsonenkriptosend);
                if (!string.IsNullOrWhiteSpace(resp.ErrorMessage))
                {
                    throw new Exception(resp.ErrorMessage);
                }
                else
                {
                    string strjsonresp = resp.HasilRespon;
                    PostCommandResultModel hasilpost = JsonConvert.DeserializeObject<PostCommandResultModel>(strjsonresp);
                    if (!hasilpost.IsSuccess)
                    {
                        throw new Exception(hasilpost.ErrorMessage);
                    }
                    else
                    {
                        MessageBox.Show("File has been uploaded!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        
        }
    }
}
