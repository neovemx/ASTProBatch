﻿using AST_ProBatch_Mobile.Views;

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AST_ProBatch_Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
