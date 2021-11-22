using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileUploadWebForm
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            var FileName = "lab.xls";
            var FilePath = Server.MapPath($"~/Files/{FileName}");
            if (File.Exists(FilePath) == true)
            {
                try
                {
                    Response.ContentType = "Application/octet-stream";
                    Response.AppendHeader("Content-Disposition", $"attachment; filename={FileName}");
                    Response.TransmitFile(FilePath);
                    Response.Flush();
                    Response.End();
                }
                catch (Exception)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Clear();
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var appPath = Request.PhysicalApplicationPath; //根目錄之實體路徑
            var savePath = Path.Combine(appPath, "Files");
            string fileName = "";
            string filePath = "";
            if (Request.Files.Count > 0)
            {
                // 以 FileUpload Object 處理 files
                foreach (var item in FileUpload.PostedFiles)
                {
                    fileName = item.FileName;
                    filePath = Path.Combine(appPath, "Files", fileName);
                    if (Request.Files[0].ContentLength <= 30000000)
                    {
                        FileUpload.SaveAs(filePath);
                    }
                }

                // 以 Request.Files 處理 files
                //for (int i = 0; i < Request.Files.Count; i++)
                //{
                //    fileName = Request.Files[i].FileName;
                //    filePath = Path.Combine(appPath, "Files", fileName);
                //    if (Request.Files[0].ContentLength <= 30000000)
                //    {
                //        FileUpload.SaveAs(filePath);
                //    }
                //}
            }
        }
    }
}