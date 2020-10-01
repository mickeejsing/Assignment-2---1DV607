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

        public MockContext mockContext() {
            return new MockContext();
        }
        public Member CreateMember(string firstName, string lastName, int socialSecurityNumber)
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
        public Boat CreateBoat(string boatType, double length)
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
        public MemberView CreateMemberView() {
            return new MemberView();
        }
        public BoatView CreateBoatView() {
            return new BoatView();
        }

        public MemberController CreateMemberController(Factory factory) {
            return new MemberController(CreateMemberView(), CreateContext(), factory);
        }

        public BoatController CreateBoatController() {
           return new BoatController(CreateBoatView(),CreateContext());
        }
    }
}