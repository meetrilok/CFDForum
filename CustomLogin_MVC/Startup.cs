using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomLogin_MVC.Startup))]
namespace CustomLogin_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
