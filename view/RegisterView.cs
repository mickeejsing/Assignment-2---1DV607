using System;

namespace assignment2 {
    public class RegisterView {

        private Register registerModel;

        public RegisterView(Register registerModel) {
            this.registerModel = registerModel;
        }

        public void displayMainNav() {
            
            Console.WriteLine("Boats [1] Members [2]");
            int input = Convert.ToInt32(Console.ReadLine());

            if(!registerModel.isInputOptionValid(input, 1, 2)) {

                Console.WriteLine("The entered value is invalid.");
                this.displayMainNav();

            } else {
                this.routeMainNav(input);
            }
        }

        private void routeMainNav(int input) {
            if (input == 1) {
                this.displayBoatOptions();
            } else if (input == 2) {
                this.displayMemberOptions();
            }
        }

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

            if(!registerModel.isInputOptionValid(input, 1, 3)) {

                Console.WriteLine("The entered value is invalid.");
                this.displayBoatOptions();

            } else {
                this.routeBoatNav(input);
            }

        }

        public void displayMemberOptions() {
            Console.WriteLine("Create member [1] Show members [2] Delete member [3] Change member information [4]");
            int input = Convert.ToInt32(Console.ReadLine());

            if(!registerModel.isInputOptionValid(input, 1, 4)) {

                Console.WriteLine("The entered value is invalid.");
                this.displayMemberOptions();

            } else {
                this.routeMemberNav(input);
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

        // Nu kanske?

    }
}
