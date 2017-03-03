using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Test_Stuff.Startup))]
namespace Test_Stuff
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
