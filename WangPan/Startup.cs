using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WangPan.Startup))]
namespace WangPan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
