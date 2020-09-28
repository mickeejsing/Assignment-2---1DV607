using System;

namespace assignment2 {
    class Program {
        static void Main(string[] args) {
            
            MemberView memberView = new MemberView();
            Register register = new Register();
            BoatView boatView = new BoatView();
            RegisterView registerView = new RegisterView();

            Controller controller = new Controller(registerView, memberView, boatView, register);
            controller.mainMenu();

        }
    }
}
