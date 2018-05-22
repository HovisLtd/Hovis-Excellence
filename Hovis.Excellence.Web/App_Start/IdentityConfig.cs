using Hovis.Excellence.Web.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace Hovis.Excellence.Web
{
    public partial class IdentityConfig
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and role manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);

            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/authentication"),
                LogoutPath = new PathString("/authentication/logout"),
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

             var google = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "********.apps.googleusercontent.com",
                ClientSecret = "********",
                Provider = new GoogleOAuth2AuthenticationProvider()
            };
            google.Scope.Add("email");
            app.UseGoogleAuthentication(google);
        }
    }
}
