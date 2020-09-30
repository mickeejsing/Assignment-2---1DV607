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
                    secretaryBoatHandler();
                }
            }
        }

        public void secretaryBoatHandler()
        {
            _registerView.displaySecretaryBoatOptions();
            input = _registerView.getViewOperation();

            if (input.Equals(ViewOperations.ShowAllBoats))
            {
                ShowAllBoats();
                secretaryBoatHandler();
            }
            else if (input.Equals(ViewOperations.ShowBoatFromId))
            {
                
                _registerView.displaySingleBoat(searchBoat());
                secretaryBoatHandler();
            }
            else if (input.Equals(ViewOperations.EditBoat))
            {
                Boat boat = searchBoat();
                if (boat != null) {
                handleEditBoat(boat);
                }
                else {
                    // berätta att båten inte hittades
                }
                secretaryBoatHandler();
            }
            else
            {
                secretaryBoatHandler();
            }
        }

        public void secretaryMemberHandler()
        {
            _registerView.displaySecretaryMemberOptions();
            input = _registerView.getViewOperation();

            if (input.Equals(ViewOperations.CreateMember))
            {
                Member member = createMember();
                createBoat(member);
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
                _register.getMembers().ForEach(m => _registerView.displayMembersCompact(m));
                Member member = selectMember();

                _registerView.displaySelectedMemberOptions();
                input = _registerView.getViewOperation();
                if (input.Equals(ViewOperations.DeleteMember))
                {
                    _register.getMembers().Remove(member);
                    _register.updateFile();
                    secretaryNav();

                }

                if (input.Equals(ViewOperations.ShowMemberDetails))
                {
                    _registerView.displayMembersVerbose(member);
                    secretaryNav();
                }

                if (input.Equals(ViewOperations.SecretaryOptions))
                {
                    secretaryNav();
                }

                while (input.Equals(ViewOperations.EditMember))
                {
                    handleEditMember(member);
                }
                if (input.Equals(ViewOperations.ManageMemberBoats))
                {
                    _registerView.displaySecretaryManagaeMemberBoats();
                    input = _registerView.getViewOperation();

                    if (input.Equals(ViewOperations.AddMemberBoat))
                    {
                        createBoat(member);
                        _register.updateFile();
                        secretaryNav();
                    }

                    if (input.Equals(ViewOperations.DeleteMemberBoat))
                    {
                        bool detailedSearch = false;
                        _registerView.displayMemberBoatInfo(member);
                        Boat boat = searchBoat(detailedSearch);
                        _register.DeleteBoat(boat);
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
            }
        }

        public void handleEditBoat(Boat boat)
        {


            while (!input.Equals(ViewOperations.ManageBoats))
            {
                _registerView.displaySecretarySingleBoatOptions();
                input = _registerView.getViewOperation();

                if (input.Equals(ViewOperations.DeleteBoat))
                {
                    Console.WriteLine(boat.Id);
                    _register.DeleteBoat(boat);
                    input = ViewOperations.ManageBoats;
                }
                else if (input.Equals(ViewOperations.editBoatLength))
                {
                    // not implemented

                }
                else if (input.Equals(ViewOperations.EditBoatType))
                {
                    // not implemented
                }
            }
        }

        public void handleEditMember(Member member)
        {
            _registerView.displayEditMemberOptions();
            input = _registerView.getViewOperation();

            if (input.Equals(ViewOperations.EditFirstName))
            {
                string firstName = _registerView.getFirstName();
                _register.UpdateMember(member, firstName, member.LastName);
                _register.getMembers().Remove(member);
                _register.updateFile();
                _register.saveMemberToFile(member);
                input = ViewOperations.EditMember;

            }

            else if (input.Equals(ViewOperations.EditLastName))
            {
                string lastName = _registerView.getLastName();
                _register.UpdateMember(member, member.FirstName, lastName);
                _register.getMembers().Remove(member);
                _register.updateFile();
                _register.saveMemberToFile(member);
                input = ViewOperations.EditMember;
            }
            else if (input.Equals(ViewOperations.SecretaryOptions))
            {
                secretaryNav();
            }
        }

        public void ShowAllBoats()
        {
            _registerView.displayAllBoats(_register.getBoatsFromRegistery());
        }

        public Boat searchBoat(bool detailed = true)
        {
            if (detailed)
                try
                {
                    string param = _registerView.getSearchParam();
                    string value = _registerView.getSearchValue();
                    return _register.searchBoat(param, value);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sorry no boat were found");
                }
            else
            {
                string id = _registerView.getBoatId();
                string param = "Id";
                return _register.searchBoat(param, id);
            }
            return null;
        }
        public void createBoat(Member member)
        {
            string boatType = _registerView.getBoatType();
            double boatLength = _registerView.getBoatLength();
            _register.CreateBoat(boatType, boatLength, member);
        }

        public Member createMember()
        {
            string firstName = _registerView.getFirstName();
            string lastName = _registerView.getLastName();
            int socialSecurityNumber = _registerView.getSocialSecurityNumber();
            return _register.CreateMember(firstName, lastName, socialSecurityNumber);

        }

        // kolla på sen
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
