using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SushiFactory.Startup))]
namespace SushiFactory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
