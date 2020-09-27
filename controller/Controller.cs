using System;
using System.Collections.Generic;

namespace assignment2 {
    public class Controller {

        RegisterView registerView;
        MemberView memberView;
        BoatView boatView;

        public Controller(RegisterView registerView, MemberView memberView, BoatView boatView) {
            this.registerView = registerView;
            this.memberView = memberView;
            this.boatView = boatView;
        }

        public void mainMenu () {
            this.registerView.getMainMenu();
        }

    }
}
