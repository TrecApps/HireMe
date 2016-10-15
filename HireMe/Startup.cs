using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HireMe.Startup))]
namespace HireMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }

               
    }

    
}
