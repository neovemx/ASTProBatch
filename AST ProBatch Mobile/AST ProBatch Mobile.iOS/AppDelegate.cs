﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace AST_ProBatch_Mobile.iOS
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
            //var attribute = new UITextAttributes();
            //attribute.TextColor = UIColor.Clear;
            //UIBarButtonItem.Appearance.SetTitleTextAttributes(attribute, UIControlState.Normal);
            //UIBarButtonItem.Appearance.SetTitleTextAttributes(attribute, UIControlState.Highlighted);
            //UINavigationBar.Appearance.TintColor = UIColor.White;
            //UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(34, 85, 170);
            //UINavigationBar.Appearance.BackgroundColor = UIColor.FromRGB(34, 85, 170);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.FromRGB(34, 85, 170) });
            //UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(34, 85, 170);
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
