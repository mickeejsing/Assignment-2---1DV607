using System;
using view;

namespace assignment2 {
    public class RegisterView : View  {


        public void displayLogin() {
            Console.WriteLine("Login screen");
            Console.WriteLine("Member [1]");
            Console.WriteLine("Secretary [2]");
        }

        public void getSecretaryOptions() 
        {
            Console.WriteLine("...");
        }

        public void displaySecretaryOptions() 
        {
            Console.WriteLine("Manage boats [1]");
            Console.WriteLine("Manage members [2]");
        }



        // Scenario skapa användare
        public void displaySecretaryMemberOptions() 
        {
            Console.WriteLine("Create member [1] Show members [2] Delete member [3] Change member information [4]");
            int input = Convert.ToInt32(Console.ReadLine());

            if(!isInputOptionValid(input, 1, 4)) {

                Console.WriteLine("The entered value is invalid.");
                this.displaySecretaryMemberOptions();

            } else {
                this.routeMemberNav(input);
            }
        }

        // Skapa medlem
        public Member displayMemberForm() {
            
            string firstName;
            string lastName;
            int socialSecurityNumber;

            
            Console.Write("Type The members first name: ");
            firstName = Console.ReadLine();
            Console.Write("Type The members last name: ");
            lastName = Console.ReadLine();
            Console.Write("Type The members social security number: ");
            socialSecurityNumber = Convert.ToInt32(Console.ReadLine());

            Member member = new Member(firstName, lastName, socialSecurityNumber);
            
            return member;

        }

            /*
            public void displaySecretaryOptions() {
            Console.WriteLine("Create member [1] Show members [2] Delete member [3] Change member information [4]");
            int input = Convert.ToInt32(Console.ReadLine());

            if(!_registerModel.isInputOptionValid(input, 1, 4)) {

                Console.WriteLine("The entered value is invalid.");
                this.displayMemberOptions();

            } else {
                this.routeMemberNav(input);
            }
        }
        */
        
        
        /*
        public void asass() {
            Console.WriteLine("Boats [1] Members [2]");
            int input = Convert.ToInt32(Console.ReadLine());

            if(!_registerModel.isInputOptionValid(input, 1, 2)) {

                Console.WriteLine("The entered value is invalid.");
                this.displayLogin();

            } else {
                this.routeMainNav(input);
            }
        }
        */

/*
        private void routeMainNav(int input) {
            if (input == 1) {
                this.displayBoatOptions();
            } else if (input == 2) {
                this.displayMemberOptions();
            }
        }
*/
        private void routeBoatNav(int input) {
            if (input == 1) {
                this.registerBoat();
            } else if (input == 2) {
                this.deleteBoat();
            } else if (input == 3) {
                this.changeBoat();
            }
        }

        private void routeMemberNav(int input) {
            if (input == 1) {
                this.createMember();
            } else if (input == 2) {
                this.showMember();
            } else if (input == 3) {
                this.deleteMember();
            } else if (input == 4) {
                this.changeMember();
            }
        }

        public void displayBoatOptions() {
            Console.WriteLine("Register boat [1] Delete boat [2] Change boat information [3]");
            int input = Convert.ToInt32(Console.ReadLine());

            if(isInputOptionValid(input, 1, 3)) {

                Console.WriteLine("The entered value is invalid.");
                this.displayBoatOptions();

            } else {
                this.routeBoatNav(input);
            }

        }

        public void registerBoat() {
            Console.WriteLine("Register boat");
        }

        public void deleteBoat() {
            Console.WriteLine("Delete boat");
        }

        public void changeBoat() {
            Console.WriteLine("Change boat information");
        }

        public void createMember() {
            Console.WriteLine("Create member");
        }

        public void changeMember() {
            Console.WriteLine("Change member information");
        }

        public void deleteMember() {
            Console.WriteLine("Delete Member");
        }

        public void showMember() {
            Console.WriteLine("Show member by entering ID or show members by listing.");
        }

        // Nu kanske? Ja

    }
}