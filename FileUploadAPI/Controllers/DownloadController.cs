using FileUploadAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace FileUploadAPI.Controllers
{
    public class DownloadController : ApiController
    {
        /// <summary>
        /// 使用自定 ActionResult 的方式來下載檔案
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DownloadFile01()
        {
            var fileName = "lab.xls";
            string filePath = HttpContext.Current.Server.MapPath($@"~/Files/{fileName}");
            return new FileResult(filePath);
        }

        /// <summary>
        /// 下載本機的檔案(相對路徑方式)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DownloadFile02()
        {
            var fileName = "lab.xls";
            string filePath = HttpContext.Current.Server.MapPath($@"~/Files/{fileName}"); //取得server的相對路徑
            if (!File.Exists(filePath))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Gone));
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = HttpUtility.UrlPathEncode(fileName);
            response.Content.Headers.ContentLength = fileStream.Length; //告知瀏覽器下載長度
            return ResponseMessage(response);
        }

        /// <summary>
        /// 下載本機的檔案(實體路徑方式)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DownloadFile03()
        {
            var fileName = "lab.xls";
            string path = $@"C:\Users\reco\source\repos\WebAPILab\FileUploadAPI\Files\{fileName}";

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = HttpUtility.UrlPathEncode(Path.GetFileName(path));
            response.Content.Headers.ContentLength = stream.Length; //告知瀏覽器下載長度
            return ResponseMessage(response);
        }

        [HttpGet]
        public IHttpActionResult DownloadFile04()
        {
            var fileName = "lab.xls";
            string filePath = HttpContext.Current.Server.MapPath($@"~/Files/{fileName}");
            var url = "https://localhost:44355/Files/lab.xls";
            HttpClient client = new HttpClient();
            var result = client.GetAsync(url).Result;
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            if (response.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsByteArrayAsync().Result;
                response.Content = new ByteArrayContent(content);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = HttpUtility.UrlPathEncode(Path.GetFileName(url));
                response.Content.Headers.ContentLength = content.Length; //告知瀏覽器下載長度
                return ResponseMessage(response);
            }
            else
            {
                return NotFound();
            }

        }
    }
}