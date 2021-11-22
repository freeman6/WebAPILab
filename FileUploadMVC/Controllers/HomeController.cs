using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUploadMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Download()
        {
            return View();
        }

        /// <summary>
        /// 在 MVC 裡下載檔案的範例
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadByMVC()
        {
            string filePath = Server.MapPath("~/Files/lab.xls");
            string fileName = Path.GetFileName(filePath);
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            return File(stream, "application/vnd.ms-excel", fileName);
        }

        /// <summary>
        /// 在 MVC 裡上傳檔案的範例
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SingleFileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                //可以在這邊加入上傳檔案的上限
                if (file.ContentLength > 0)
                {
                    string fileName = Path.Combine(Server.MapPath("~/UploadedFiles/"), file.FileName);
                    file.SaveAs(fileName);
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 在 MVC 裡上傳檔案的範例
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MultiFileUpload(HttpPostedFileBase[] files)
        {
            if (files.Count() >= 1)
            {
                foreach (var file in files)
                {
                    //可以在這邊加入上傳檔案的上限
                    if (file.ContentLength > 0)
                    {
                        string fileName = Path.Combine(Server.MapPath("~/UploadedFiles/"), file.FileName);
                        file.SaveAs(fileName);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}