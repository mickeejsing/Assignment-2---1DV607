using System;

namespace assignment2 {
    public class Boat {

        public Boat(string type, double length) {
            Type = type;
            Length = length;
        }

        public string Type { 
            set; 
            get; 
        }

        public double Length {
            set;
            get;
        }

    }
}
