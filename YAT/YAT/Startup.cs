using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YAT.Startup))]
namespace YAT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
