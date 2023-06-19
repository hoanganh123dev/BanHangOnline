using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BanHangOnline.Startup))]
namespace BanHangOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
