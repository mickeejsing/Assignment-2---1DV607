using System;

namespace assignment2 {
    public class Boat {

        public Boat(Type type, int length) {
            Type = type;
            Length = length;
        }

        public Type Type { 
            private set; 
            get; 
        }

        public double Length {
            private set; 
            get;
        }

    }
}
