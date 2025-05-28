using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tms2017.MVC.Startup))]
namespace Tms2017.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
