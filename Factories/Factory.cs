using System;
using System.Collections.Generic;
using Model;
using Persistence;
using View;
using Controller;

namespace Factories
{
    public class Factory
    {
        public static JsonContext CreateContext() {
            return new JsonContext();
        }

        public static MockContext mockContext() {
            return new MockContext();
        }
        public static Member CreateMember(string firstName, string lastName, int socialSecurityNumber)
        {
            Random rnd = new Random();
            int id = rnd.Next(10000,99999);
            Member member = new Member{
                FirstName = firstName,
                LastName = lastName,
                SocialSecurityNum = socialSecurityNumber,
                boats = new List<Boat>()
            };
            return member;
        }
        public static Boat CreateBoat(string boatType, double length)
        {
            Random rnd = new Random();
            int id = rnd.Next(10000,99999);

            Boat boat = new Boat() {
                Length = length,
                Type = boatType,
                Id = id
            };
            return boat;
        }
        public static MemberView CreateMemberView() {
            return new MemberView();
        }
        public static BoatView CreateBoatView() {
            return new BoatView();
        }

        public static MemberController CreateMemberController() {
            return new MemberController(CreateMemberView(), CreateContext());
        }

        public static BoatController CreateBoatController() {
           return new BoatController(CreateBoatView(),CreateContext());
        }
    }
}