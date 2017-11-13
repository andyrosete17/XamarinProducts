using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Products.BackEnds.Startup))]
namespace Products.BackEnds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
