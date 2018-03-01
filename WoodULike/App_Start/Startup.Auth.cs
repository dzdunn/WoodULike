using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WoodULike.DAL;

namespace WoodULike
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
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            // clientId: "",
            // clientSecret: "");

            app.UseTwitterAuthentication(
             consumerKey: "0Ew7inM2I4SHaqkahWlOyXtve",
             consumerSecret: "5sLvhhdDyfdTtKGuethWEuk8aLEvaAW8kcFlDxBAwCVXie6oCV");

            app.UseFacebookAuthentication(
             appId: "269671706902571",
             appSecret: "38040da7cb13ebd3e38be3bfee85cf0c");

            app.UseGoogleAuthentication(
                clientId: "187974553373-ir1jjn7di2o90t8ld4kor5f81f2akv6o.apps.googleusercontent.com",
                clientSecret: "2Ot8udHbshLVyS6zdBLVNy-L"
                );
        }
    }
}