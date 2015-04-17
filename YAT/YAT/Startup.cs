using Microsoft.Owin;
using Owin;
using BusinessLayer;

[assembly: OwinStartupAttribute(typeof(YAT.Startup))]
namespace YAT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // Builder builder = new Builder();
            //builder.putData();
          //  builder.getData();
            ConfigureAuth(app);
            
        }
    }
}
