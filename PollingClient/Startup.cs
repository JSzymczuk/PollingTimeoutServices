using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PollingClient.Startup))]
namespace PollingClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
