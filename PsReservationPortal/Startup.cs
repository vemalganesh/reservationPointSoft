using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PsReservationPortal.Startup))]
namespace PsReservationPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
