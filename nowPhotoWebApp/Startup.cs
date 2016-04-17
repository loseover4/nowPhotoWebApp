using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nowPhotoWebApp.Startup))]
namespace nowPhotoWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
