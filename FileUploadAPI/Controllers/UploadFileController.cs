using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FileUploadAPI.Controllers
{
    public class UploadFileController : ApiController
    {
        /// <summary>
        /// 使用 Request.Files 來完成上傳任務
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Post()
        {
            var result = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;

            // 如果上傳沒有檔案時的例外處理
            if (httpRequest.Files.Count == 0)
                result = Request.CreateResponse(HttpStatusCode.BadRequest);

            if (httpRequest.Files.Count >= 1)
            {
                var files = new List<string>();
                foreach (string item in httpRequest.Files)
                {
                    var file = httpRequest.Files[item];
                    var filePath = HttpContext.Current.Server.MapPath($"~/{file.FileName}");
                    file.SaveAs(filePath);
                    files.Add(filePath);
                }

                result = Request.CreateResponse(HttpStatusCode.OK, files);
            }

            return result;
        }
    }
}