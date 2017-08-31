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
using Android.Support.Design.Widget;

namespace AppTourism.Droid
{
    [Activity(Label = "DiscoverActivity")]
    public class DiscoverActivity : Activity
    {
        private ListView list;
        private Items _items, all, museum, gallery, other;
        private ISharedPreferences sp;
        private TabLayout tb;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Discover);

            string mMatisseDetail = "Présentée dans les salles de la Villa des Arènes, demeure gênoise du XVIIe siècle qui abrite le musée Matisse depuis 1963," +
                " la collection permanente du musée compte aujourd'hui 68 peintures et gouaches découpées, 236 dessins," +
                " 218 gravures, 57 sculptures, soit la quasi-totalité de l'oeuvre sculpté, 14 livres illustrés et aussi 95 photographies," +
                " 187 objets ayant appartenu au peintre, que complètent sérigraphies, tapisseries, céramiques, vitraux et documents";

            Button clear = (Button)FindViewById(Resource.Id.clearBtn);
            clear.Click += Clear_Click;


            tb = (TabLayout)FindViewById(Resource.Id.tbLayout);
            tb.SetupWithViewPager(new Android.Support.V4.View.ViewPager(this));

            list = (ListView)FindViewById(Resource.Id.list);
            list.Selected = true;

            TypePlace museumType = new TypePlace().Museum();
            TypePlace galleryType = new TypePlace().Gallery();

            all = new Items();
            museum = new Items();
            gallery = new Items();

            _items = new Items();
            Item i1 = new Item("Musée Matisee", mMatisseDetail, museumType, "src", 12.50, 43.7193065, 7.2762088);
            Item i2 = new Item("Musée Marc Chagall", "Marc Chagall", museumType, "src", 11.00, 43.70913, 7.2694598);
            Item i3 = new Item("Fondation Maeght", "Fondation Maeght", galleryType, "src", 11.00, 43.7007732, 7.1141347);
            _items.ItemList.Add(i1);
            _items.ItemList.Add(i2);
            _items.ItemList.Add(i3);


            
            foreach (Item i in _items)
            {
                all.ItemList.Add(i);
                if (i.Type.Equals(museumType))
                {
                    museum.ItemList.Add(i);
                }
                else if (i.Type.Equals(galleryType))
                {
                    gallery.ItemList.Add(i);
                }
                
            }
            string[] allString = all.convertToString();
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.List, allString);
            list.Adapter = adapter;
            list.ItemClick += List_ItemClick;
            tb.TabSelected += Tb_TabSelected;
                        

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            sp = Application.GetSharedPreferences("Pref", FileCreationMode.Private);
            var editor = sp.Edit();
            editor.Remove("tourList");
            editor.Commit();

        }

        private void Tb_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            if(e.Tab.Text == "All")
            {
                string[] allString = all.convertToString();
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.List, allString);
                list.Adapter = adapter;
            }
            else if(e.Tab.Text == "Museum")
            {
                string[] museumString = museum.convertToString();
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.List, museumString);
                list.Adapter = adapter;
            }
            else if(e.Tab.Text == "Gallery")
            {
                string[] galleryString = gallery.convertToString();
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.List, galleryString);
                list.Adapter = adapter;
            }
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            sp = Application.GetSharedPreferences("Pref", FileCreationMode.Private);
            var editor = sp.Edit();

            Item iSelected = all.ItemList[e.Position];

            var str = Newtonsoft.Json.JsonConvert.SerializeObject(iSelected);

            editor.PutString("itemSelected", str);
            editor.Commit();



            StartActivity(typeof(ItemCardActivity));
            
        }


        
    }
}