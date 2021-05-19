using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmokersTavern.Startup))]
namespace SmokersTavern
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
