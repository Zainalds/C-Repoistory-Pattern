using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmokersTavernStore.Startup))]
namespace SmokersTavernStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
