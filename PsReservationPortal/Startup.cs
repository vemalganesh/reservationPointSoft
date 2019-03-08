using Microsoft.Owin;
using Owin;
using PsReservationPortal.Models;

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
