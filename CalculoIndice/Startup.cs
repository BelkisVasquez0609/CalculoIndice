using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalculoIndice.Startup))]
namespace CalculoIndice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
