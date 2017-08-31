using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Util;

namespace AppTourism.Droid
{
	[Activity (Label = "AppTourism.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        private string package_name = "com.atos.PictureRecognition";
		protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var sp = Application.GetSharedPreferences("Pref", FileCreationMode.Private);
            sp.Edit().Clear();
            sp.Edit().Commit();
            

            // Get our button from the layout resource,
            // and attach an event to it
            Button discover_btn = FindViewById<Button>(Resource.Id.discover);
            Button myGuide_btn = FindViewById<Button>(Resource.Id.myGuide);
            Button myTour_btn = FindViewById<Button>(Resource.Id.myTour);

            Typeface customFont = Typeface.CreateFromAsset(Resources.Assets, "Abel-Regular.ttf");
            discover_btn.SetTypeface(customFont, TypefaceStyle.Normal);
            discover_btn.SetTextSize(ComplexUnitType.Px, 200);
            discover_btn.SetShadowLayer(20, 5, 5, Color.Black);

            myGuide_btn.SetTypeface(customFont, TypefaceStyle.Normal);
            myGuide_btn.SetTextSize(ComplexUnitType.Px, 200);
            myGuide_btn.SetShadowLayer(20, 5, 5, Color.Black);

            myTour_btn.SetTypeface(customFont, TypefaceStyle.Normal);
            myTour_btn.SetTextSize(ComplexUnitType.Px, 200);
            myTour_btn.SetShadowLayer(20, 5, 5, Color.Black);


            discover_btn.Click += delegate {
                StartActivity(typeof(DiscoverActivity));
            };

            myGuide_btn.Click += delegate {
                Intent i = PackageManager.GetLaunchIntentForPackage(package_name);
                if (i != null)
                    StartActivity(i);
            };

            myTour_btn.Click += delegate
            {
                StartActivity(typeof(LoadingDataPage));
            };
        }
    }
}


