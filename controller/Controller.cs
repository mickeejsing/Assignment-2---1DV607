using System;
using System.Collections.Generic;
using System.Linq;
using enums;

namespace assignment2
{
    public class Controller
    {
        private readonly RegisterView _registerView;
        private readonly MemberView _memberView;
        private readonly Register _register;
        private Enum input;

        public Controller(RegisterView registerView, MemberView memberView, Register register)
        {
            _registerView = registerView;
            _memberView = memberView;
            _register = register;
        }


        public void mainNav()
        {
            _registerView.displayLogin();
            input = _registerView.getViewOperation();
            switch (input)
            {
                case ViewOperations.isSecretary:
                    secretaryNav();
                    break;
                case ViewOperations.isMember:
                    memberNav();
                    break;
            }
        }
        public void secretaryNav()
        {
            _registerView.displaySecretaryOptions();
            {
                input = _registerView.getViewOperation();
                if (input.Equals(ViewOperations.ManageMembers))
                {
                    secretaryMemberHandler();
                }
                else if (input.Equals(ViewOperations.ManageBoats))
                {
                    // secretaryBoatHandler
                }
            }
        }

        public void secretaryMemberHandler()
        {
            _registerView.displaySecretaryMemberOptions();
            input = _registerView.getViewOperation();

            if (input.Equals(ViewOperations.CreateMember))
            {
                Member member = createMember();
                Boat boat = createBoat();
                member.boats.Add(boat);
                _register.saveMemberToFile(member);
                secretaryNav();
            }

            // Display members
            if (input.Equals(ViewOperations.ShowMembers))
            {
                displayMembers();
                secretaryNav();
            }

            // Select member
            if (input.Equals(ViewOperations.SelectMember))
            {
                Member member = selectMember();

                _registerView.displaySelectedMemberOptions();
                input = _registerView.getViewOperation();

                if (input.Equals(ViewOperations.DeleteMember))
                {
                    _register.getMembers().Remove(member);
                    _register.updateFile();
                    secretaryNav();

                }

                if (input.Equals(ViewOperations.ManageMemberBoats))
                {
                    _registerView.displaySecretaryManagaeMemberBoats();
                    input = _registerView.getViewOperation();

                    if (input.Equals(ViewOperations.AddMemberBoat))
                    {
                        Boat boat = createBoat();
                        member.boats.Add(boat);
                        _register.updateFile();
                        secretaryNav();
                    }

                    if (input.Equals(ViewOperations.DeleteMemberBoat))
                    {
                        string boatType = _registerView.getValidBoats(member);
                        if (boatType != "error")
                        {
                            Boat boat = member.boats.Where(b => b.Type == boatType).FirstOrDefault();
                            member.boats.Remove(boat);
                            _register.updateFile();
                        }
                        else if (boatType == "q")
                        {
                            secretaryNav();
                        }
                        else
                        {
                            _registerView.displayErrorNoBoatFound();
                        }
                        secretaryNav();
                    }

                    if (input.Equals(ViewOperations.EditMemberBoat))
                    {
                        //välj båt
                        // här behövs det göras mer
                        Boat boat = member.boats.Where(b => b.Type == "sailboat").FirstOrDefault();
                        double boatLenth = _registerView.getBoatLength();
                        string boatType = _registerView.getBoatType();
                        member.boats.Remove(boat);
                        _register.getMembers().Remove(member);
                        _register.updateFile();

                        boat.Type = boatType;
                        boat.Length = boatLenth;
                        member.boats.Add(boat);

                        _register.saveMemberToFile(member);
                        secretaryNav();

                    }
                }

                if (input.Equals(ViewOperations.EditMember))
                {

                    member.LastName = "SuperTest";
                    member.FirstName = "::DDD";
                    _register.getMembers().Remove(member);
                    _register.updateFile();
                    _register.saveMemberToFile(member);
                    secretaryNav();
                }

            }
        }

        public Boat createBoat()
        {
            string boatType = _registerView.getBoatType();
            double boatLength = _registerView.getBoatLength();
            Boat boat = new Boat(boatType, boatLength);
            return boat;
        }

        public Member createMember()
        {
            string firstName = _registerView.getFirstName();
            string lastName = _registerView.getLastName();
            int socialSecurityNumber = _registerView.getSocialSecurityNumber();
            Member member = new Member(firstName, lastName, socialSecurityNumber);

            return member;
        }

        public Member selectMember()
        {
            Member member;
            string parameter;

            do
            {
                parameter = _registerView.getFirstName();
                member = _register.getMembers().Where(m => m.FirstName == parameter).FirstOrDefault();
                if (member == null)
                {
                    _registerView.displayMemberNotFound();
                }
            } while (member == null);
            return member;
        }

        public void displayMembers()
        {
            _registerView.displayGetMemberDisplayFormat();
            input = _registerView.getViewOperation();

            if (input.Equals(ViewOperations.ShowMembersVerbose))
                _register.getMembers().ForEach(m => _registerView.displayMembersVerbose(m));
            if (input.Equals(ViewOperations.ShowMembersCompact))
                _register.getMembers().ForEach(m => _registerView.displayMembersCompact(m));
        }

        public void memberNav()
        {

        }

    }
}
