using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeoutService.Startup))]
namespace TimeoutService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
