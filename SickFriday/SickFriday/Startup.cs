using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SickFriday.Startup))]
namespace SickFriday
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
