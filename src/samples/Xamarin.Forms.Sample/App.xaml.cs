﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xamarin.Forms.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new TabbedPageDemo();
            //MainPage = new MainPage();
            //MainPage = new CarouselPageDemo();
            //MainPage = new NavigationPage(new CarouselPageDemo());
            MainPage = new MasterDetailPageDemo();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
