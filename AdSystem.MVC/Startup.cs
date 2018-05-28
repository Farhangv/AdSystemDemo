using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdSystem.MVC.Startup))]
namespace AdSystem.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
