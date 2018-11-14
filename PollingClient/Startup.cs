using Microsoft.Owin;
using Owin;
using Hangfire;
using System.Data.Entity;
using PollingClient.Models;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(PollingClient.Startup))]
namespace PollingClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            new ApplicationDbContext().Database.CreateIfNotExists();
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireServer();
        }
    }
}
