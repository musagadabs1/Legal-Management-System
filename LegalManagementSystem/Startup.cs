using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LegalManagementSystem.Startup))]
namespace LegalManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
