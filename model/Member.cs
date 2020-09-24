using System;

namespace assignment2 {
    public class Member {

        public Member(int personalNr, string firstname, string lastname) {
            FirstName = firstname;
            LastName = lastname;
            PersonalNr = personalNr;
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
