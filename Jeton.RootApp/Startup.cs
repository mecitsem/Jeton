using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jeton.RootApp.Startup))]
namespace Jeton.RootApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
