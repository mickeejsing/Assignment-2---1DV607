
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

        public void MemberHandler()
        {
            input = _memberController.DisplayMemberOptions();
            switch (input)
            {
                case ViewOperations.CreateMember:
                    Member member = _memberController.HandleCreateMember();
                    Boat boat = _boatController.CreateBoat();
                    member.Boats.Add(boat);
                    _memberController.HandleAddMember(member);
                    break;
                case ViewOperations.ShowMembers: _memberController.HandleDisplayMembers(); break;
                case ViewOperations.SelectMember:
                    Member selectedMember = _memberController.HandleSelectMember();
                    if (selectedMember != null)
                        HandleMemberOptions(selectedMember);
                    break;
                case ViewOperations.isSecretary: DisplayOptionsNav(); break;
                default: MemberHandler(); break;
            }
            MemberHandler();
        }
        public void HandleMemberOptions(Member selectedMember)
        {
            input = _memberController.DisplaySelectedMemberOptions();

            switch (input)
            {
                case ViewOperations.DeleteMember:
                    _memberController.HandleRemoveMember(selectedMember);
                    MemberHandler(); break;
                case ViewOperations.ShowMemberDetails: _memberController.DisplayMembersVerbose(selectedMember); break;
                case ViewOperations.SecretaryOptions: DisplayOptionsNav(); break;
                case ViewOperations.EditMember: _memberController.HandleEditMember(selectedMember); break;
                case ViewOperations.ManageMemberBoats: HandleManageBoats(selectedMember); break;
                default: HandleMemberOptions(selectedMember); break;
            }
            HandleMemberOptions(selectedMember);
        }
        public void HandleManageBoats(Member selectedMember)
        {
            input = _boatController.DisplayManageMemberBoats();

            switch (input)
            {
                case ViewOperations.AddMemberBoat:
                    Boat boat = _boatController.CreateBoat();
                    _boatController.AddBoatToMember(selectedMember, boat); break;
                case ViewOperations.DeleteMemberBoat:
                    _boatController.DeleteBoat(selectedMember); break;
                case ViewOperations.EditMemberBoat: break;
                default: HandleManageBoats(selectedMember); break;
            }
        }

        public void HandleEditMember(Member member)
        {
            input = _memberController.DisplayEditMemberOptions();

            if (member != null)
            {
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
            else
            {
                //
            }
        }

        public void BoatHandler()
        {
            input = _boatController.DisplayBoatOptions();
            switch (input)
            {
                case ViewOperations.ShowAllBoats: _boatController.ShowAllBoats(); break;
                case ViewOperations.ShowBoatFromId: _boatController.SearchBoat(); break;
                case ViewOperations.EditBoat:
                    Boat boat = _boatController.FindBoatById();
                    HandleEditBoat(boat); break;
                default: BoatHandler(); break;
            }
        }

        public void HandleEditBoat(Boat boat)
        {
            if (boat != null)
            {
                input = _boatController.DisplaySelectedBoatOptions();

                switch (input)
                {
                    case ViewOperations.EditBoatLength: _boatController.EditLBoatLength(boat); break;
                    case ViewOperations.EditBoatType: _boatController.EditBoatType(boat); break;
                    case ViewOperations.DeleteBoat: _boatController.DeleteBoat(boat); break;
                    case ViewOperations.ManageBoats: BoatHandler(); break;
                    default: HandleEditBoat(boat); break;
                }
            }
            else
                _boatController.DisplayErrorBoatNotFound();
            BoatHandler();
        }
    }
}