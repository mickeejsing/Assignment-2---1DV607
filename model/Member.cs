using System;
using System.Collections.Generic;

namespace assignment2 {
    public class Member {

        public Member(string firstName, string lastName, int socialSecurityNum, int id) {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNum = socialSecurityNum;
            Id = id;
            boats = new List<Boat>();
        }
        public List<Boat> boats {get; set;}

        public string FirstName { 
            set; 
            get; 
        }

        public string LastName { 
            set; 
            get; 
        }
        public int Id {
            get;
            set;
        }

        public int SocialSecurityNum {
            private set; 
            get;
        }
       

    }
}
