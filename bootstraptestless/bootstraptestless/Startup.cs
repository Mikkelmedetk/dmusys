using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bootstraptestless.Startup))]
namespace bootstraptestless
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
