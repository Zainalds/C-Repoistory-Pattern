using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using SmokersTavern.Model;
using SmokersTavern.Data;


//Zain
namespace SmokersTavern
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));




            app.UseFacebookAuthentication(
               appId: "195341641120333",
              appSecret: "e1c374a016febb18fcbbe43046491b6e");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "183905530859-7098hv1q75e3hf4p98vl454h787r89p0.apps.googleusercontent.com",
                ClientSecret = "JQmha625AOQPYGmrVoXmAXhK",
                Provider = new GoogleOAuth2AuthenticationProvider()
            });
        }
    }
}