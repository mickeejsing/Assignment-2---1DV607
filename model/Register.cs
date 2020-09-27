using System;
using System.Collections.Generic;

namespace assignment2 {
    public class Register {

        private List<Boat> _boats = new List<Boat>();
        private List<Member> _member = new List<Member>();

        public Register() {
            
        }

        public bool isInputOptionValid(int input, int from, int to) {
            
            if(input >= from && input <= to) {
                return true;
            }

            return false;
        }
    }
}
