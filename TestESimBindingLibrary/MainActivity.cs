﻿using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Charter.Esimlibrary;

namespace TestESimBindingLibrary
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnEsimDownloadListener
    {
        private EsimHandler esimHandler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            Button installButton = FindViewById<Button>(Resource.Id.installButton);
            installButton.Click += InstallButtonOnClick;

            esimHandler = new EsimHandler(ApplicationContext);
            esimHandler.Init(this);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        private void InstallButtonOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            esimHandler.DownloadEsim("Hello");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnFailure(string result, Profile profile)
        {
            Log.Info("TAG_ESIM", result);
            Log.Info("TAG_ESIM", profile.Id + " " + profile.Name + " " + profile.Password);
            Toast.MakeText(ApplicationContext, result, ToastLength.Long).Show();
        }

        public void OnSuccess(string result)
        {
            Log.Info("TAG_ESIM", result);
            Toast.MakeText(ApplicationContext, result, ToastLength.Long).Show();
        }
    }
}

