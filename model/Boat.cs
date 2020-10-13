using System;

namespace Model
{
    public class Boat
    {
        Random rnd;

        public Boat()
        {
            rnd = new Random();
            Id = rnd.Next(10000, 99999);
        }

        public BoatType BoatType
        {
            set;
            get;
        }
        public int Id
        {
            get; set;
        }
        public double Length
        {
            set;
            get;
        }
    }
    public enum BoatType
    {
        Sailboat,
        Motorsailer,
        Kayak,
        Canoe,
        Other
    }
}
