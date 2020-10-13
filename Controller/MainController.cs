
using System;
using System.Collections.Generic;
using Model;
using Persistence;
using View;

namespace Controller
{
    public class MainController
    {
        private readonly BoatController _boatController;
        private readonly MemberController _memberController;
        Enum input;

        public MainController(BoatController boatController, MemberController memberController)
        {
            _memberController = memberController;
            _boatController = boatController;
        }
        public void DisplayMainNav()
        {
            input = _memberController.DisplayLogin();
            switch (input)
            {
                case ViewOperations.isSecretary: DisplayOptionsNav(); break;
                case ViewOperations.isMember: DisplayOptionsNav(); break;
                default: DisplayMainNav(); break;
            }
        }
        public void DisplayOptionsNav()
        {
            input = _memberController.DisplayMainOptions();
            switch (input)
            {
                case ViewOperations.ManageMembers: MemberHandler(); break;
                case ViewOperations.ManageBoats: BoatHandler(); break;
                default: DisplayOptionsNav(); break;
            }

        }

        public void BoatHandler()
        {
            input = _boatController.DisplayBoatOptions();
            switch (input)
            {
                case ViewOperations.ShowAllBoats: _boatController.ShowAllBoats(); break;
                case ViewOperations.ShowBoatFromId: _boatController.HandleDisplaySingleBoat(); break;
                case ViewOperations.EditBoat: _boatController.ManageBoat(); break;
                default: BoatHandler(); break;
            }
        }
        public void MemberHandler()
        {
            input = _memberController.DisplayMemberOptions();
            switch (input)
            {
                case ViewOperations.CreateMember:
                    Member member = _memberController.HandleCreateMember();
                    Boat boat = _boatController.HandleCreateBoat();
                    member.Boats.Add(boat);
                    _memberController.HandleAddMember(member);
                    break;
                case ViewOperations.ShowMembers: _memberController.HandleDisplayMembers(); break;
                case ViewOperations.SelectMember:
                    Member selectedMember = _memberController.HandleSelectMember();
                    HandleMemberOptions(selectedMember);
                    break;
                default: MemberHandler(); break;
            }
        }
        public void HandleMemberOptions(Member selectedMember)
        {
            input = _memberController.DisplaySelectedMemberOptions();

            switch (input)
            {
                case ViewOperations.DeleteMember: _memberController.HandleRemoveMember(selectedMember); break;
                case ViewOperations.ShowMemberDetails: _memberController.DisplayMembersVerbose(selectedMember); break;
                case ViewOperations.SecretaryOptions: DisplayOptionsNav(); break;
                case ViewOperations.EditMember: _memberController.HandleEditMember(selectedMember); break;
                case ViewOperations.ManageMemberBoats: HandleManageBoats(selectedMember); break;
                default: HandleMemberOptions(selectedMember); break;
            }
        }
        public void HandleManageBoats(Member selectedMember)
        {
            input = _boatController.DisplayManageMemberBoats();

            switch (input)
            {
                case ViewOperations.AddMemberBoat:
                    Boat boat = _boatController.HandleCreateBoat();
                    _boatController.AddBoatToMember(selectedMember, boat); break;
                case ViewOperations.DeleteMemberBoat:
                    _boatController.HandleDeleteBoat(selectedMember); break;
                case ViewOperations.EditMemberBoat: break;
                default: HandleManageBoats(selectedMember); break;
            }
        }

        public void HandleEditMember(Member member)
        {
            input = _memberController.DisplayEditMemberOptions();

            switch (input)
            {
                case ViewOperations.EditFirstName:
                    _memberController.EditFirstName(member);
                    HandleEditMember(member); break;
                case ViewOperations.EditLastName:
                    _memberController.EditLastName(member);
                    HandleEditMember(member); break;
                case ViewOperations.SecretaryOptions: HandleMemberOptions(member); break;
                default: HandleEditMember(member); break;
            }
        }

        public void HandleEditBoat(Member selectedMember)
        {
            _boatController.ManageBoat();

        }
    }
}