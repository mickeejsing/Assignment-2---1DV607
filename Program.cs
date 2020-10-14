using System;
using Controller;
using Persistence;
using View;

namespace assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            MemberView memberView = new MemberView();
            IDbContext context = new JsonContext(); 
            Repository repository = new Repository(context);
            BoatController boatController = new BoatController(new BoatView(), repository);
            MemberController memberController = new MemberController(new MemberView(),repository);
            MainController mainController = new MainController(boatController, memberController);

            
            mainController.DisplayMainNav();
        }
    }
}
