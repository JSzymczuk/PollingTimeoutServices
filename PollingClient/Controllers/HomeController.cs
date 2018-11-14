using Hangfire;
using Polly;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PollingClient.Controllers
{
    public static class PersistentState
    {//nie powinno się tak tego robić ale nie chce mi się bawić z bazą i migracjami, a Sesja nie działa przy procesach w tle

        public static List<string> PollingResults = new List<string>();
    }

    public class HomeController : Controller
    {
        string ServiceUrl = ConfigurationManager.AppSettings["TimeoutServiceUrl"];


        string InstantUrl => $"{ServiceUrl}/Instant";
        string TimeoutUrl => $"{ServiceUrl}/Timeout";
        string RetryUrl => $"{ServiceUrl}/Retry";

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
            return Json(new { status = result.StatusCode, content = await result.Content.ReadAsStringAsync() },
                JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> RetryGet()
        {
            var attempt = 1;
            var result = Policy
                .Handle<HttpException>()
                .Or<HttpException>()
                .OrResult<HttpResponseMessage>(r => r.StatusCode != HttpStatusCode.OK)
                .WaitAndRetry(3, (retry) =>
                    {
                        attempt = retry + 1;
                        return new TimeSpan(0, 0, 10);
                    })
                .Execute(() => { return client.GetAsync($"{RetryUrl}/{attempt}").Result; });
            return Json(new { status = result.StatusCode, content = await result.Content.ReadAsStringAsync() },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult StartPolling()
        {
            PersistentState.PollingResults = new List<string>();
            RecurringJob.AddOrUpdate("JobId", () => singlePollingRequest(), Cron.MinuteInterval(1));
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult GetPollingResults()
        {
            return Json(new { status = HttpStatusCode.OK, content = PersistentState.PollingResults }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StopPolling()
        {
            RecurringJob.RemoveIfExists("JobId");
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        public void singlePollingRequest()
        {
            var requestResult = client.GetAsync($"{TimeoutUrl}/{110}").Result;
            var singlePollingResult = $"{DateTime.Now.ToLocalTime()} {requestResult.StatusCode} {requestResult.Content.ReadAsStringAsync().Result}\n";
            PersistentState.PollingResults.Add(singlePollingResult);
        }

    }
}