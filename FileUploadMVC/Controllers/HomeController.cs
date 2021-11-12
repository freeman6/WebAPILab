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

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                //可以在這邊加入上傳檔案的上線
                if (file.ContentLength > 0) 
                {
                    string fileName = Path.Combine(Server.MapPath("~/"), file.FileName);
                    file.SaveAs(fileName);
                }
            }
            return RedirectToAction("Index");
        }
    }
}