using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        [Route("Timeout/")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Get, mordy!", Encoding.UTF8, "application/json")
            };
        }

        [HttpPost]
        [Route("Timeout/")]
        public HttpResponseMessage Post()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Post mordy!", Encoding.UTF8, "application/json")
            };
        }

        [HttpPut]
        [Route("Timeout/")]
        public HttpResponseMessage Put()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Zażółć gęślą jaźń, mordy!", Encoding.UTF8, "application/json")
            };
        }
    }
}