using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin; 
using System.Collections.Generic;
using System.Linq;
using System.Web;  
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(sys.Application.Api.Startup))] 

namespace sys.Application.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            //HttpConfiguration config = new HttpConfiguration(); 
            //WebApiConfig.Register(config); 
            //app.UseWebApi(config);


            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app); 
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
