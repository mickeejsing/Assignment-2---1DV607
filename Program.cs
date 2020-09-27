using System;

namespace assignment2 {
    class Program {
        static void Main(string[] args) {
            
            MemberView memberView = new MemberView(new Member());
            BoatView boatView = new BoatView(new Boat());
            RegisterView registerView = new RegisterView(new Register());

            Controller controller = new Controller(registerView, memberView, boatView);
            controller.mainMenu();

        }
    }
}
