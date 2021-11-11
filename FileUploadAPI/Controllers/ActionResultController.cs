using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FileUploadAPI.Controllers
{
    public class ActionResultController : ApiController
    {
        /// <summary>
        /// 案例一：使用 HttpResponseMessage 來完成回傳狀態
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        public HttpResponseMessage OldAPIResponse()
        {
            var status = new string[] { "value1", "value2" };
            if (status.Count()>0)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// 案例二：使用 HttpResponseMessage 來完成回傳狀態
        /// 此情境適用於上傳及下載檔案情境
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Export()
        {
            var fileName = "lab.xls";
            var strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\" + fileName);

            //輸出到瀏覽器
            try
            {
                var stream = new FileStream(strPath, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }

        /// <summary>
        /// 案例三：使用 IHttpActionResult 來完成回傳狀態
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult NewAPIResponse(int id)
        {
            var status = new string[] { "value1", "value2" };
            if (status.Count() > 0)

            {
                //return new HttpResponseMessage(HttpStatusCode.OK);
                return Ok();
            }
            else
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }
        }

        /// <summary>
        /// 案例四：在使用 IHttpActionResult 為回傳型別的狀況下，強制使用 HttpResponseMessage 來轉型為 IHttpActionResult。
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult NewAPIResponse2()
        {
            IHttpActionResult response;
            //we want a 303 with the ability to set location
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            responseMsg.Headers.Location = new Uri("http://xxxxxx.blah");
            response = ResponseMessage(responseMsg);
            return response;
        }
    }
}