using Microsoft.Owin;
using Owin;
using Hangfire;
using System.Data.Entity;
using TimeoutService.Models;

[assembly: OwinStartupAttribute(typeof(TimeoutService.Startup))]
namespace TimeoutService
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
