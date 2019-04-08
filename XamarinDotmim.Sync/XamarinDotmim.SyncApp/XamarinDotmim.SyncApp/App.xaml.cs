﻿using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinDotmim.SyncApp
{
    public partial class App : Application
    {


        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

       

        protected override void OnStart()
        {
            // Handle when your app starts


        }
    }
}
