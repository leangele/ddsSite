using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ddcSite.Startup))]
namespace ddcSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
