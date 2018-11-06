Install-Package Microsoft.AspNet.WebApi.Cors
Install-Package Microsoft.AspNet.WebApi.WebHost 
Dodać refrencję do System.ServiceModel.

Do App_Start dodać klasę WebApiConfig:

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
        }
    }

Do Global.asax.cs dodać:
	GlobalConfiguration.Configure(WebApiConfig.Register); (po RegisterAllAreas)
	
Dodać usługę:

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TimeoutApiController : ApiController
    {
        [HttpGet]
        [Route("api/Timeout/")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Get.", Encoding.UTF8, "application/json")
            };
        }
    }