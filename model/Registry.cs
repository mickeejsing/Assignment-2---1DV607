using System;

namespace assignment2 {
    public class Registry {

        public Registry(Member member, Boat boat) {
        }

        public string FirstName { 
            set; 
            get; 
        }

        public string LastName { 
            set; 
            get; 
        }

        public int PersonalNr {
            private set; 
            get;
        }
    }
}
