using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VS2013OneWebApp.Startup))]
namespace VS2013OneWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
