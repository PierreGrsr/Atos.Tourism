using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Location;
using Android.Locations;
using Java.Util;

namespace AppTourism.Droid
{
    [Activity(Label = "LoadingDataPage")]
    public class LoadingDataPage : Activity, Android.Locations.ILocationListener
    {
        private LocationManager lm;
        private Location myLocation;
        private ISharedPreferences sp;

        public void OnLocationChanged(Location location)
        {
            Console.WriteLine("Youououou");
            myLocation = location;
            if (myLocation != null)
            {
                Toast.MakeText(this, "Location saved", ToastLength.Long).Show();
                string lat = myLocation.Latitude.ToString();
                string longt = myLocation.Longitude.ToString();
                var editor = sp.Edit();
                editor.PutString("lat", lat);
                editor.PutString("long", longt);
                editor.Commit();
                Intent i = new Intent(this, typeof(MyTourActivity));
                this.Finish();
                StartActivity(i);
                //StartActivity(typeof(MyTourActivity));

            }
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LoadingData);

            sp = Application.GetSharedPreferences("Pref", FileCreationMode.Private);
           

            lm = (LocationManager)GetSystemService(LocationService);
            Location myLastLocation = lm.GetLastKnownLocation(LocationManager.GpsProvider);
            var time = Calendar.Instance.TimeInMillis - 2 * 60 * 1000;
            var myLocTime = myLastLocation.Time;
            var res = myLocTime - time;
            if (myLastLocation != null && myLastLocation.Time - Calendar.Instance.TimeInMillis < 2 * 60 * 1000)
            {
                var editor = sp.Edit();
                string lastLoc = Newtonsoft.Json.JsonConvert.SerializeObject(myLastLocation);
                editor.PutString("myLocation", lastLoc);
                editor.Commit();
                Intent i = new Intent(this, typeof(MyTourActivity));
                this.Finish();
                StartActivity(i);
                //StartActivity(typeof(MyTourActivity));
            }
            else
            {
                lm.RequestSingleUpdate(new Criteria(), this, null);
            }


        }
    }
}