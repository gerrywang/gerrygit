using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GerryBlog.Startup))]
namespace GerryBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
