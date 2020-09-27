using System;

namespace assignment2 {
    public class RegisterView {

        private Register registerModel;

        public RegisterView(Register registerModel) {
            this.registerModel = registerModel;
        }

        public void getMainMenu() {
            
            Console.WriteLine("Boats [1] Members [2]");
            int input = Convert.ToInt32(Console.ReadLine());

            if(!registerModel.isInputOptionValid(input, 1, 2)) {

                Console.WriteLine("The entered value is invalid.");
                this.getMainMenu();

            } else {
                this.routeOptions(input);
            }
        }

        private void routeOptions(int input) {
            if (input == 1) {
                this.displayBoatOptions();
            } else if (input == 2) {
                this.displayMemberOptions();
            }
        }

        public void displayBoatOptions() {
            Console.WriteLine("Welcome to the boat options!");
        }

        public void displayMemberOptions() {
            Console.WriteLine("Welcome to the member options!");
        }
    }
}
