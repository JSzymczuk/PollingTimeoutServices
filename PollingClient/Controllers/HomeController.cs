using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PollingClient.Controllers
{
    public class HomeController : Controller
    {
        const string SERVICE_URL = @"http://localhost:49394";


        string InstantUrl => $"{SERVICE_URL}/Instant";
        string TimeoutUrl => $"{SERVICE_URL}/Timeout";

        private static readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SimpleGet()
        {
            var result = await client.GetAsync(InstantUrl);
            return Json(new { status = result.StatusCode, content = await result.Content.ReadAsStringAsync() }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SimplePost()
        {
            var result = await client.PostAsync(InstantUrl, new StringContent("Zażółć gęślą jaźń"));
            return Json(new { status = result.StatusCode, content = await result.Content.ReadAsStringAsync() }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TimeoutGet(int secs)
        {
            var url = $"{TimeoutUrl}/{secs}";
            var result = await client.GetAsync(url);
            return Json(new { status = result.StatusCode, content = await result.Content.ReadAsStringAsync() }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TimeoutPost(int secs)
        {
            var url = $"{TimeoutUrl}/{secs}";
            var result = await client.PostAsync(url, new StringContent("Zażółć gęślą jaźń"));
            return Json(new { status = result.StatusCode, content = await result.Content.ReadAsStringAsync() }, JsonRequestBehavior.AllowGet);
        }

    }
}