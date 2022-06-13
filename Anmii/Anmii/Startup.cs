using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Anmii.Startup))]
namespace Anmii
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
