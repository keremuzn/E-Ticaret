using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(And.ticaret.APi.Startup))]
namespace And.ticaret.APi
{
   
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.Configuration(app);
        }

        public void ConfigurationOAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new LoginProvider()
            });
            
        }
    }
}