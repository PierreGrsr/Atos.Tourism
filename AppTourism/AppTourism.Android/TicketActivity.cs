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

namespace AppTourism.Droid
{
    [Activity(Label = "TicketActivity")]
    public class TicketActivity : Activity
    {
        private ISharedPreferences sp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Ticket);

            EditText cardNumber = (EditText)FindViewById(Resource.Id.cardNumber);
            EditText crypto = (EditText)FindViewById(Resource.Id.cryptogramme);
            Button helpBtn = (Button)FindViewById(Resource.Id.button2);
            TextView price = (TextView)FindViewById(Resource.Id.priceTextView);

            sp = Application.GetSharedPreferences("Pref", FileCreationMode.Private);
            var editor = sp.Edit();
            string priceData = sp.GetString("Price", null);
            Console.WriteLine(priceData);
            price.Text += priceData;

            

        }
    }
}