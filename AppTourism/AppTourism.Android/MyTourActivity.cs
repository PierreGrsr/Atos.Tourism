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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Java.Util;
using System.Threading.Tasks;
using System.Net.Http;

namespace AppTourism.Droid
{
    [Activity(Label = "MyTourActivity")]
    public class MyTourActivity : Activity, IOnMapReadyCallback
    {
        private GoogleMap maps;
        private Location myLocation;
        private ISharedPreferences sp;
        private Geocoder geo;
        private IList<Address> adresses;

        public void OnMapReady(GoogleMap googleMap)
        {
            maps = googleMap;
            string style = "[{\"elementType\": \"geometry\"," + 
                "\"stylers\": [{\"color\": \"#ebe3cd\""+ 
                "}]},{\"elementType\": \"labels.text.fill\"," + 
                "\"stylers\": [{\"color\": \"#523735\"}" + 
                "]},{\"elementType\": \"labels.text.stroke\"," +
                " \"stylers\": [{\"color\": \"#f5f1e6\"}" + 
                "]},{\"featureType\": \"administrative\",\"elementType\":" +
                " \"geometry.stroke\",\"stylers\": [{\"color\": \"#c9b2a6\"" + 
                "}]},{\"featureType\": \"administrative.land_parcel\", " + 
                "\"elementType\": \"geometry.stroke\",\"stylers\": [{\"color\":" + 
                "\"#dcd2be\"}]},{\"featureType\": \"administrative.land_parcel\", " + 
                "\"elementType\": \"labels.text.fill\",\"stylers\": [{\"color\": \"#ae9e90\"" + 
                "}]},{\"featureType\": \"landscape.natural\",\"elementType\": \"geometry\"," + 
                "\"stylers\": [{\"color\": \"#dfd2ae\"}]},{" + 
                "\"featureType\": \"poi\",\"elementType\": \"geometry\",\"stylers\": [" + 
                "{\"color\": \"#dfd2ae\"}]},{\"featureType\":" +
                " \"poi\",\"elementType\": \"labels.text.fill\",\"stylers\": [{" + 
                " \"color\": \"#93817c\"}]},{\"featureType\": \"poi.park\", " +
                "\"elementType\": \"geometry.fill\",\"stylers\": [{ \"color\":" +
                "\"#a5b076\"}]},{\"featureType\": \"poi.park\",  " +
                "\"elementType\": \"labels.text.fill\",\"stylers\": [{\"color\":" +
                "\"#447530\"}]},{\"featureType\": \"road\"," +
                "\"elementType\": \"geometry\",\"stylers\": [{\"color\": \"#f5f1e6\"" +
                "}]},{\"featureType\": \"road.arterial\",\"elementType\": \"geometry\"," +
                "\"stylers\": [{\"color\": \"#fdfcf8\"}]},{\"featureType\":" +
                "\"road.highway\",\"elementType\": \"geometry\",\"stylers\":[{"+ 
                "\"color\": \"#f8c967\"}]},{\"featureType\": \"road.highway\", " +
                "\"elementType\": \"geometry.stroke\",\"stylers\": [{\"color\": \"#e9bc62\" " +
                "}]},{\"featureType\": \"road.highway.controlled_access\",\"elementType\": \"geometry\","+
                "\"stylers\": [{\"color\": \"#e98d58\"}]},{\"featureType\": \"road.highway.controlled_access\"," +
                "\"elementType\": \"geometry.stroke\",\"stylers\": [{\"color\": \"#db8555\"}]}," +
                "{\"featureType\": \"road.local\",\"elementType\": \"labels.text.fill\",\"stylers\": [{ "+
                "\"color\": \"#806b63\"}]},{\"featureType\": \"transit.line\",\"elementType\": \"geometry\"," + 
                "\"stylers\":[{\"color\": \"#dfd2ae\"}]},{\"featureType\": \"transit.line\", "+
                "\"elementType\": \"labels.text.fill\",\"stylers\": [{\"color\": \"#8f7d77\" "+
                "}]},{\"featureType\": \"transit.line\",\"elementType\": \"labels.text.stroke\", "+
                "\"stylers\": [{\"color\": \"#ebe3cd\"}]},{\"featureType\": \"transit.station\","+
                "\"elementType\": \"geometry\",\"stylers\":[{\"color\": \"#dfd2ae\"}]},{ "+
                " \"featureType\": \"water\",\"elementType\": \"geometry.fill\",\"stylers\": [{"+
                "\"color\": \"#b9d3c2\"}]},{\"featureType\": \"water\",\"elementType\": \"labels.text.fill\","+
                "\"stylers\": [{\"color\": \"#92998d\"}]}]";

            myLocation = new Location("");
            geo = new Geocoder(this, Locale.GetDefault(Locale.Category.Display));

            sp = Application.GetSharedPreferences("Pref", FileCreationMode.Private);
            double lat = Convert.ToDouble(sp.GetString("lat", null));
            double longt = Convert.ToDouble(sp.GetString("long", null));
            myLocation.Latitude = lat;
            myLocation.Longitude = longt;

            //Marker 
            var Marker = new MarkerOptions();
            Marker.SetPosition(new LatLng(lat, longt));
            maps.AddMarker(Marker);

            maps.MyLocationEnabled = true;
            maps.SetMapStyle(new MapStyleOptions(style));

            //Marker destination
            var list = sp.GetString("tourList", null);
            if (list != null)
            {
                Items tList = Newtonsoft.Json.JsonConvert.DeserializeObject<Items>(list);
                foreach (Item i in tList.ItemList)
                {
                    var mk = new MarkerOptions();
                    mk.SetPosition(new LatLng(i.Latitude, i.Longitude));
                    adresses = geo.GetFromLocation(i.Latitude, i.Longitude, 1);
                    mk.SetTitle(i.Name);
                    maps.AddMarker(mk);

                }
            }
            LatLng Nice = new LatLng(43.703665, 7.260368);
            if (myLocation != null)
            {
                maps.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(myLocation.Latitude, myLocation.Longitude), 10.5f));
            }
            else
            {
                maps.MoveCamera(CameraUpdateFactory.NewLatLngZoom(Nice, 11f));
            }
            TraceAllRoutes();

        }

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.MyTour);
            setUpMap();


        }

        

        private void setUpMap()
        {
            if (maps == null)
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
        }
     

        public void TraceRoute(GmsDirection gms)
        {
            var list = new PolylineOptions();
            list.InvokeColor(0x660EA90E); 
            if (gms.status == "OK")
            {
                foreach (Route r in gms.routes)
                {
                    foreach (Leg l in r.legs)
                    {
                        foreach (Step st in l.steps)
                        {
                            var listPolyline = Utils.DecodePolylinePoints(st.polyline.points);
                            foreach (Location loc in listPolyline)
                            {
                                list.Add(new LatLng(loc.Latitude, loc.Longitude));
                            }
                        }
                    }
                }
            }
           maps.AddPolyline(list);
        }

        public async Task<GmsDirection> getDirections(LatLng end) 
        {
            var client = new HttpClient();
                        
            string myLocLat = myLocation.Latitude.ToString().Replace("," , ".");
            string myLocLong = myLocation.Longitude.ToString().Replace("," , ".");
            string endLat = end.Latitude.ToString().Replace(",", ".");
            string endLong = end.Longitude.ToString().Replace(",", ".");

            StringBuilder sb = new StringBuilder();
            sb.Append("http://maps.googleapis.com/maps/api/directions/json?origin=");
            sb.Append(myLocLat + ",");
            sb.Append(myLocLong);
            sb.Append("&destination=");
            sb.Append(endLat + ",");
            sb.Append(endLong);
            sb.Append("&mode=driving");

            var uri = new Uri(sb.ToString());
            var response = await client.GetStringAsync(uri);

            Console.WriteLine(response);

            GmsDirection result = Newtonsoft.Json.JsonConvert.DeserializeObject<GmsDirection>(response);
            return result;
        }

        public async Task<List<GmsDirection>> GetAllRoute()
        {
            var list = sp.GetString("tourList", null);
            if(list == null)
            {
                /* AlertDialog dial = new AlertDialog.Builder(this).Create();
                 dial.SetTitle("Warning");
                 dial.SetMessage("Your Tour is empty, please go to Discover and add Item to your list");
                 dial.SetButton("Ok", new IDialogInterfaceOnClickListener()
                 {
                     public void onClick
                 });*/
                Toast.MakeText(this, "Your Tour is empty, please go to Discover and add Item to your list", ToastLength.Long).Show();
                var emptyList = new List<GmsDirection>();
                return emptyList;
            }
            else
            {
                Items myTour = Newtonsoft.Json.JsonConvert.DeserializeObject<Items>(list);
                List<GmsDirection> listRoute = new List<GmsDirection>();
                foreach (Item i in myTour.ItemList)
                {
                    LatLng coord = new LatLng(i.Latitude, i.Longitude);
                    var dir = await getDirections(coord);
                    listRoute.Add(dir);
                }
                return listRoute;
            }
        }
        
        public async void TraceAllRoutes()
        {
            List<GmsDirection> routeList = await GetAllRoute();
            foreach(GmsDirection gms in routeList)
            {
                TraceRoute(gms);
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}








