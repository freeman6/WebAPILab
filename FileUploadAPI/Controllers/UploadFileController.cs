using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
                    var filePath = HttpContext.Current.Server.MapPath($"~/App_Data/{file.FileName}");
                    file.SaveAs(filePath);
                    files.Add(filePath);
                }

                result = Request.CreateResponse(HttpStatusCode.OK, files);
            }

            return result;
        }

        /// <summary>
        /// 以官網範例使用 MultipartFormDataStreamProvider 來完成上傳任務
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //取得目錄指定的地方
            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.塞入檔案,上傳文件,但是沒有副檔名，需要更改檔名
                await Request.Content.ReadAsMultipartAsync(provider);

                int i = 0;
                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    //取得含有雙引號的
                    string filename = file.Headers.ContentDisposition.FileName.Trim('"');
                    //取得副檔名
                    string fileExt = filename.Substring(filename.LastIndexOf('.'));

                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file.LocalFileName);
                    //fileinfo.Name 上傳後的文件路徑不含副檔名
                    //修改文件名加上副檔名

                    string newFileName = filename;
                    i++;
                    //最後保存文件路徑
                    string saveUrl = System.IO.Path.Combine(root, filename);
                    fileInfo.MoveTo(saveUrl);


                    //System.Diagnostics.Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    //System.Diagnostics.Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}