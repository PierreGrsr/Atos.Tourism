using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Java.Lang;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppTourism.Droid
{
    [Activity(Label = "ItemCardActivity")]
    public class ItemCardActivity : Activity
    {
        
        ISharedPreferences sp;
        Items myTour;
        Item savedItem;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ItemCard);

            Button ticket = (Button)FindViewById(Resource.Id.ticket);
            Button add = (Button)FindViewById(Resource.Id.add);
            ImageView itemImage = (ImageView)FindViewById(Resource.Id.itemImage);
            TextView tv = (TextView)FindViewById(Resource.Id.detail);

            add.Click += Add_Click;
            ticket.Click += Ticket_Click;

            sp = Application.GetSharedPreferences("Pref", FileCreationMode.Private);
            var editor = sp.Edit();
            /*editor.Clear();
            editor.Commit();*/

    

            var str = sp.GetString("itemSelected", null);
            if(str != null)
            {
                savedItem = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(str);
                tv.Text = savedItem.Detail;
                editor.PutString("Price", savedItem.TicketPrice.ToString());
                editor.Commit();

            }

            var list = sp.GetString("tourList", null);
            if (list != null)
            {
                myTour = Newtonsoft.Json.JsonConvert.DeserializeObject<Items>(list);               
            }
            else
            {
                Console.WriteLine("null");
                myTour = new Items();               
            }



        }

        private void Ticket_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(TicketActivity));
        }

        private void Add_Click(object sender, EventArgs e)
        {
            //Check ID préférable mais pas de BD pour l'instant 
            bool isIn = false;
            Console.WriteLine(myTour.ItemList.Count);
            if (myTour.ItemList.Count > 0)
            {
                foreach (Item i in myTour.ItemList)
                {
                    if (i.Name == savedItem.Name)
                    {
                        if (i.Detail == savedItem.Detail)
                        {
                            Toast.MakeText(this, "You've already add this to your Tour", ToastLength.Long).Show();
                            isIn = true;
                        }
                    }
                }
                if (isIn == false)
                {
                    myTour.ItemList.Add(savedItem);
                }
            }
            else
            {
                myTour.ItemList.Add(savedItem);
            }
        }

        public override void OnBackPressed()
        {
            var updateList = Newtonsoft.Json.JsonConvert.SerializeObject(myTour);
            Console.WriteLine(updateList);
            var editor = sp.Edit();
            editor.PutString("tourList", updateList);
            editor.Commit();
            base.OnBackPressed();
        }
    }
}