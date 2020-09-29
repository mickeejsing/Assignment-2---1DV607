﻿using System;

namespace assignment2 {
    class Program {
        static void Main(string[] args) {
            
            MemberView memberView = new MemberView();
            Register register = new Register();
            RegisterView registerView = new RegisterView();

            Controller controller = new Controller(registerView, memberView, register);
            controller.mainNav();
        }
    }
}
