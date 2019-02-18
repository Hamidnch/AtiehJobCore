using AtiehJobCore.Web.Framework.Localization;
using AtiehJobCore.Web.Framework.Mvc.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace AtiehJobCore.Web.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //areas
            routeBuilder.MapRoute(name: "areaRoute", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            //home page
            routeBuilder.MapLocalizedRoute("HomePage", "", new { controller = "Home", action = "Index" });

            //login
            routeBuilder.MapLocalizedRoute("Login",
                            "account/user/login/",
                            new { controller = "User", action = "Login" });
            //register jobseeker
            routeBuilder.MapLocalizedRoute("RegisterJobseeker",
                            "account/user/registerjobseeker/",
                            new { controller = "User", action = "RegisterJobseeker" });
            //register result page
            routeBuilder.MapLocalizedRoute("RegisterResult",
                "account/user/registerresult/{resultId}",
                new { controller = "User", action = "RegisterResult" });

            //logout
            routeBuilder.MapLocalizedRoute("Logout",
                            "account/user/logout/",
                            new { controller = "User", action = "Logout" });

            //contact us
            routeBuilder.MapLocalizedRoute("ContactUs",
                            "contactus",
                            new { controller = "Common", action = "ContactUs" });
            //sitemap
            routeBuilder.MapLocalizedRoute("Sitemap",
                            "sitemap",
                            new { controller = "Common", action = "Sitemap" });
            routeBuilder.MapLocalizedRoute("sitemap-indexed.xml", "sitemap-{Id:min(0)}.xml",
                new { controller = "Common", action = "SitemapXml" });



            //change language (AJAX link)
            routeBuilder.MapLocalizedRoute("ChangeLanguage",
                            "changelanguage/{langid}",
                            new { controller = "Common", action = "SetLanguage" });


            //check username availability
            routeBuilder.MapLocalizedRoute("CheckUsernameAvailability",
                            "user/checkusernameavailability",
                            new { controller = "User", action = "CheckUsernameAvailability" });

            //passwordrecovery
            routeBuilder.MapLocalizedRoute("PasswordRecovery",
                            "passwordrecovery",
                            new { controller = "User", action = "PasswordRecovery" });
            //password recovery confirmation
            routeBuilder.MapLocalizedRoute("PasswordRecoveryConfirm",
                            "passwordrecovery/confirm",
                            new { controller = "User", action = "PasswordRecoveryConfirm" });


            routeBuilder.MapLocalizedRoute("UserChangePassword",
                            "user/changepassword",
                            new { controller = "User", action = "ChangePassword" });

            routeBuilder.MapLocalizedRoute("UserDeleteAccount",
                            "user/deleteaccount",
                            new { controller = "User", action = "DeleteAccount" });
            routeBuilder.MapLocalizedRoute("UserAvatar",
                            "user/avatar",
                            new { controller = "User", action = "Avatar" });

            routeBuilder.MapLocalizedRoute("AccountActivation",
                            "user/activation",
                            new { controller = "User", action = "AccountActivation" });

            //user profile page
            routeBuilder.MapLocalizedRoute("UserProfile",
                            "profile/{id}",
                            new { controller = "Profile", action = "Index" });

            routeBuilder.MapLocalizedRoute("UserProfilePaged",
                            "profile/{id}/page/{pageNumber}",
                            new { controller = "Profile", action = "Index" });


            //site closed
            routeBuilder.MapLocalizedRoute("SiteClosed", "siteclosed",
                new { controller = "Common", action = "SiteClosed" });

            ////EU Cookie law accept button handler (AJAX link)
            //routeBuilder.MapRoute("EuCookieLawAccept",
            //                "eucookielawaccept",
            //                new { controller = "Common", action = "EuCookieLawAccept" });

            //activate newsletters
            routeBuilder.MapLocalizedRoute("NewsletterActivation",
                            "newsletter/subscriptionactivation/{token:guid}/{active}",
                            new { controller = "Newsletter", action = "SubscriptionActivation" });

            //robots.txt
            routeBuilder.MapRoute("robots.txt", "robots.txt",
                            new { controller = "Common", action = "RobotsTextFile" });

            //sitemap (XML)
            routeBuilder.MapLocalizedRoute("sitemap.xml", "sitemap.xml",
                            new { controller = "Common", action = "SitemapXml" });

            //install
            routeBuilder.MapRoute("Installation", "install",
                            new { controller = "Install", action = "Index" });
            //upgrade
            routeBuilder.MapRoute("Upgrade", "upgrade",
                            new { controller = "Upgrade", action = "Index" });

            //page not found
            routeBuilder.MapLocalizedRoute("PageNotFound", "page-not-found",
                            new { controller = "Common", action = "PageNotFound" });

            //push notifications
            routeBuilder.MapRoute(
               "PushNotifications.Send",
               "Admin/PushNotifications/Send",
            new { controller = "PushNotifications", action = "Send" });

            routeBuilder.MapRoute(
                "PushNotifications.Messages",
                "Admin/PushNotifications/Messages",
            new { controller = "PushNotifications", action = "Messages" });

            routeBuilder.MapRoute(
               "PushNotifications.Receivers",
               "Admin/PushNotifications/Receivers",
            new { controller = "PushNotifications", action = "Receivers" });

            routeBuilder.MapRoute(
                "PushNotifications.DeleteReceiver",
                "Admin/PushNotifications/DeleteReceiver",
                new { controller = "PushNotifications", action = "DeleteReceiver" });

            routeBuilder.MapRoute(
                "PushNotifications.Configure",
                "Admin/PushNotifications/Configure",
                new { controller = "PushNotifications", action = "Configure" });

            routeBuilder.MapRoute(
                "PushNotifications.PushMessagesList",
                "Admin/PushNotifications/PushMessagesList",
            new { controller = "PushNotifications", action = "PushMessagesList" });

            routeBuilder.MapRoute(
                "PushNotifications.PushReceiversList",
                "Admin/PushNotifications/PushReceiversList",
            new { controller = "PushNotifications", action = "PushReceiversList" });
        }

        public int Priority => 0;
    }
}
