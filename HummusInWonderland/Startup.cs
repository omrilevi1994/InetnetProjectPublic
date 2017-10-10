using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HummusInWonderland.Startup))]
namespace HummusInWonderland
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
