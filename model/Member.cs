using System;

namespace assignment2 {
    public class Member {

        public Member() {
            
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
