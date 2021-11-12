using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using FileUploadAPI.Models;

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
        [HttpGet]
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
        [HttpGet]
        public IHttpActionResult NewAPIResponse(int id)
        {
            //var status = new string[] { "value1", "value2" };
            var status = new string[] { };
            if (status.Count() >0)

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
        [HttpGet]
        public IHttpActionResult NewAPIResponse2()
        {
            IHttpActionResult response;
            //we want a 303 with the ability to set location
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            responseMsg.Headers.Location = new Uri("https://mail.google.com/");
            response = ResponseMessage(responseMsg);
            return response;
        }

        /// <summary>
        /// 案例五：無回傳值(void)
        /// 其返回的 HttpStatus Code 為 (204 No Content)
        /// </summary>
        [HttpGet]
        public void Get()
        {
            int a = 1;
            int b = 2;
            int c = a + b;
        }

        /// <summary>
        /// 案例六：回傳 Json Result
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetJson()
        {
            var result = new List<UserInfo>();
            result.Add(new UserInfo { Name = "reco", age = 22 });
            result.Add(new UserInfo { Name = "freeman6", age = 23 });
            return Json<List<UserInfo>>(result);
        }

        /// <summary>
        /// 案例七：使用 Redirect 作為回傳狀態
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRedirect()
        {
            return Redirect("https://localhost:44355/API/ActionResult/GetRedirect2");
        }
        [HttpGet]
        public IHttpActionResult GetRedirect2()
        {
            string result = "請求成功！";
            return Ok(result);
        }

        /// <summary>
        /// 案例八：使用自定義型別來回傳，其 HttpStatus Code 統一預設為 (200 OK)
        /// </summary>
        /// <returns></returns>
        public object GetCustomerTypeAction()
        {
            var result = new List<UserInfo>();
            result.Add(new UserInfo { Name = "reco", age = 22 });
            result.Add(new UserInfo { Name = "freeman6", age = 23 });
            return result;
        }

        /// <summary>
        /// 回傳 BadRequest (400) 的錯誤
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetBadRequest()
        { 
            return BadRequest();
        }

        /// <summary>
        /// 回傳 NotFound (404) 的錯誤
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetNotFound()
        { 
            return NotFound();
        }

        /// <summary>
        /// 回傳 HttpStatus Code (OK 200) 及回傳值。
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetContent()
        {
            return Content<string>(HttpStatusCode.OK, "OK");
        }
    }
}