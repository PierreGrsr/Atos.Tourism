using Android.Locations;
using System;

namespace AppTourism
{
    public class Item
    {
        private string name;
        private string detail;
        private string imgSrc;
        private double ticketPrice;
        private TypePlace type;
        private double latitude;
        private double longitude;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Detail
        {
            get
            {
                return detail;
            }

            set
            {
                detail = value;
            }
        }

        public string ImgSrc
        {
            get
            {
                return imgSrc;
            }

            set
            {
                imgSrc = value;
            }
        }

        public double TicketPrice
        {
            get
            {
                return ticketPrice;
            }

            set
            {
                ticketPrice = value;
            }
        }

        public TypePlace Type
        {
            get { return type;}
            set { type = value; }
        }

       

        public double Latitude { get { return latitude; } set { latitude = value; } }
        public double Longitude { get { return longitude; } set { longitude = value; } }

        public Item(string name, string detail, TypePlace type, string imgSrc, double ticketPrice, double latitude, double longitude)
        {
            this.Name = name;
            this.Detail = detail;
            this.Type = type;
            this.ImgSrc = imgSrc;
            this.TicketPrice = ticketPrice;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public override string ToString()
        {
            return Name;
        }


    }
}

