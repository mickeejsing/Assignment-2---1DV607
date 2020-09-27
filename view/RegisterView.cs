using System;

namespace assignment2 {
    public class RegisterView {

        private Register registerModel;

        public RegisterView(Register registerModel) {
            this.registerModel = registerModel;
        }

        public void getMainMenu() {
            Console.WriteLine("Register boat [1] Delete Boat [2] Uppdate boat [3]");
        }
    }
}
