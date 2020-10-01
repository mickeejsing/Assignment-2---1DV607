using System;
using System.Collections.Generic;

namespace Model {
    public class Member {

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
            set;
            get;
        }
    }
}
