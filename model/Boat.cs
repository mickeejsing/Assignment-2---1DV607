using System;

namespace assignment2 {
    public class Boat {

        public Boat(string type, double length, int id) {
            Type = type;
            Length = length;
            Id = id;
        }

        public string Type { 
            set; 
            get; 
        }

        public int Id {
            get; set;
        }

        public double Length {
            set;
            get;
        }

    }
}
