using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TimeoutService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TimeoutApiController : ApiController
    {
        [HttpGet]
        [Route("Instant/")]
        public HttpResponseMessage InstantGet()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Udany natychmiastowy get!", Encoding.UTF8, "application/json")
            };
        }

        [HttpPost]
        [Route("Instant/")]
        public HttpResponseMessage InstantPost()
        {
            var requestContent = Request.Content.ReadAsStringAsync().Result;
            var responseMessage = "Udany natychmiastowy post!";
            if (!string.IsNullOrEmpty(requestContent))
            {
                responseMessage += "\n" + requestContent;
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseMessage, Encoding.UTF8, "application/json")
            };
        }

        [HttpPut]
        [Route("Instant/")]
        public HttpResponseMessage InstantPut()
        {
            var requestContent = Request.Content.ReadAsStringAsync().Result;
            var responseMessage = "Udany natychmiastowy put!";
            if (!string.IsNullOrEmpty(requestContent))
            {
                responseMessage += "\n" + requestContent;
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseMessage, Encoding.UTF8, "application/json")
            };
        }


        [HttpGet]
        [Route("Timeout/{secs}")]
        public HttpResponseMessage Get(int secs)
        {
            Thread.Sleep(secs * 1000);
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent($"Udany get po {secs} sekundach!", Encoding.UTF8, "application/json")
            };
        }

        [HttpPost]
        [Route("Timeout/{secs}")]
        public HttpResponseMessage Post(int secs)
        {
            var requestContent = Request.Content.ReadAsStringAsync().Result;
            var responseMessage = $"Udany post po {secs} sekundach!";
            if (!string.IsNullOrEmpty(requestContent))
            {
                responseMessage += "\n" + requestContent;
            }
            Thread.Sleep(secs * 1000);
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseMessage, Encoding.UTF8, "application/json")
            };
        }
    }
}