using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FINALObservatorioNet.Startup))]
namespace FINALObservatorioNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
