using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jeton.Admin.Web.Startup))]
namespace Jeton.Admin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
