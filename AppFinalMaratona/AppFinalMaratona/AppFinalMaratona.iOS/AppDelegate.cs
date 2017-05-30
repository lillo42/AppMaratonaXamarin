using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Newtonsoft.Json.Linq;

namespace AppFinalMaratona.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            // Register for push notifications.
            var settings = UIUserNotificationSettings.GetSettingsForTypes(
                                         UIUserNotificationType.Alert
                                         | UIUserNotificationType.Badge
                                         | UIUserNotificationType.Sound,
                                         new NSSet());
            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        //public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    const string templateBodyAPNS =
        //   "{\"aps\":{\"alert\":\"$(messageParam)\"}}";
        //    var templates = new JObject();
        //    templates["genericMessage"] = new JObject
        //    {
        //        { "body", templateBodyAPNS}
        //    };
        //    // Register for push with your mobile app
        //    var client = new MobileServiceClient(“URL DO SEU APP”);
        //    var push = client.GetPush();
        //    push.RegisterAsync(deviceToken, templates);
        //}

        //public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, 
        //                                                Action<UIBackgroundFetchResult>completionHandler)
        //{
        //    NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as
        //   NSDictionary;
        //    string alert = string.Empty;
        //    if (aps.ContainsKey(new NSString("alert")))
        //        alert = (aps[new NSString("alert")] as NSString).ToString();
        //    //show alert
        //    if (!string.IsNullOrEmpty(alert))
        //    {
        //        var avAlert = new UIAlertView("Notification", alert, null, "OK", null);
        //        avAlert.Show();
        //    }
        //}
    }
}
