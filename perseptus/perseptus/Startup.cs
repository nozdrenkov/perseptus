using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(perseptus.Startup))]
namespace perseptus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
