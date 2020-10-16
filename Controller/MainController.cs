
using System;
using System.Collections.Generic;
using Model;
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
        #region Main Navigation
        /// <summary>DisplayMainNav <c>DeleteBoat</c> Shows the user the main nav, here the user can log in or join as a guest</summary>
        public void DisplayMainNav()
        {
            input = _memberController.DisplayLogin();
            switch (input)
            {
                case ViewOperations.isSecretary: Login(); break;
                case ViewOperations.isMember: GuestHandler(); break;
                default: DisplayMainNav(); break;
            }
        }
        public void Login()
        {
            bool loginSuccess = _memberController.Login();

            if(loginSuccess)
                DisplayOptionsNav();
            else
                Login();
        }
        /// <summary>Method <c>DisplayOptionsNav</c> Displays options for a logged in user to either Manage Members or Manage Boats</summary>
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
        #endregion
        #region Members

        /// <summary>Method <c>MemberHandler</c> Handles use case Create Member and sends to other handlers to handle the use cases for:
        /// Displaying Members and Editing Members, also has an option to go back to the Logged in user Menu</summary>
        public void MemberHandler()
        {
            input = _memberController.DisplayMemberOptions();
            switch (input)
            {
                case ViewOperations.CreateMember:
                    Member member = _memberController.CreateMember();
                    Boat boat = _boatController.CreateBoat();
                    member.Boats.Add(boat);
                    _memberController.AddMember(member);
                    break;
                case ViewOperations.ShowMembers: DisplayMembers(); break;
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

        /// <summary>Method <c>HandleMemberOptions</c> Handles use case Show member details and sends to other handlers to handle the use cases for:
        ///  Editing the member, editing the boat of the member also has an option to go back to the Logged in user Menu</summary>
        public void HandleMemberOptions(Member selectedMember)
        {
            input = _memberController.DisplaySelectedMemberOptions();

            switch (input)
            {
                case ViewOperations.DeleteMember:
                    _memberController.RemoveMember(selectedMember);
                    MemberHandler(); break;
                case ViewOperations.ShowMemberDetails: _memberController.DisplayMembersVerbose(selectedMember); break;
                case ViewOperations.SecretaryOptions: DisplayOptionsNav(); break;
                case ViewOperations.EditMember: HandleEditMember(selectedMember); break;
                case ViewOperations.ManageMemberBoats: HandleManageBoats(selectedMember); break;
                default: HandleMemberOptions(selectedMember); break;
            }
            HandleMemberOptions(selectedMember);
        }

        /// <summary>Method <c>DisplayMembers</c> Handles use case Show members Verbose and Show members Compact</summary>
        public void DisplayMembers()
        {
            if (_memberController.RepositoryIsEmpty())
            {
                _memberController.DisplayNoMembers();
            }
            else
            {
                input = _memberController.DisplayGetMemberDisplayFormat();
                {
                    switch (input)
                    {
                        case ViewOperations.ShowMembersVerbose: _memberController.DisplayMembersVerbose(); break;
                        case ViewOperations.ShowMembersCompact: _memberController.DisplayMembersCompact(); break;
                        default: DisplayMembers(); break;
                    }
                }
            }
        }

        /// <summary>Method <c>HandleEditMember</c> Handles use case for Editing user details</summary>
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
                    case ViewOperations.SelectMember: HandleMemberOptions(member); break;
                    case ViewOperations.SecretaryOptions: DisplayMainNav(); break;
                    default: HandleEditMember(member); break;
                }
            }
            else
            {
                //
            }
        }
        #endregion
        #region Guests
        public void GuestHandler()
        {
            input = _memberController.DisplayGuestOptions();

            switch (input)
            {
                case ViewOperations.ShowMembers: _memberController.DisplayMembersCompact(); break;
                case ViewOperations.ShowAllBoats: _boatController.DisplayAllBoats(); break;
                case ViewOperations.isSecretary: DisplayMainNav(); break;
                default:  GuestHandler(); break;
            }
            GuestHandler();
        }
        #endregion
        #region Boats
        /// <summary>Method <c>HandleManageBoats</c> Handles use cases for adding a boat to a member, deleting a boat from a member and sending over to the 
        /// EditBoatHandler</summary>
        public void HandleManageBoats(Member selectedMember)
        {
            input = _boatController.DisplayManageMemberBoats();

            switch (input)
            {
                case ViewOperations.AddMemberBoat:
                    Boat boat = _boatController.CreateBoat();
                    _boatController.AddBoatToMember(selectedMember, boat); break;
                case ViewOperations.DeleteMemberBoat:
                    if (selectedMember.Boats.Count > 0)
                    {
                        List<Boat> boats = selectedMember.Boats;
                        bool boatDeleted = _boatController.DeleteBoat(boats);
                        if (boatDeleted)
                            _boatController.DisplayBoatDeleted();
                    }
                    else
                        _boatController.DisplayErrorBoatNotFound();
                    break;
                case ViewOperations.EditMemberBoat: EditBoatHandler(selectedMember); break;
                default: HandleManageBoats(selectedMember); break;
            }
        }
        /// <summary>Method <c>HandleManageBoats</c> Handles use cases for adding a boat to a member, deleting a boat from a member in the context of editing a users boats and sending over to the 
        /// EditBoatHandler </summary>
        public void EditBoatHandler(Member member)
        {
            List<Boat> boats = member.Boats;
            boats.ForEach(boat => _boatController.DisplaySingleBoat(boat));
            Boat boatFromSearch = _boatController.FindBoatById();
            if (boatFromSearch != null)
            {
                input = _boatController.DisplaySelectedBoatOptions();
                switch (input)
                {
                    case ViewOperations.EditBoatLength: _boatController.EditLBoatLength(boatFromSearch); break;
                    case ViewOperations.EditBoatType: _boatController.EditBoatType(boatFromSearch); break;
                    case ViewOperations.SecretaryOptions: DisplayMainNav(); break;
                    default: EditBoatHandler(member); break;
                }
            }
        }

        /// <summary>Method <c>BoatHandler</c> Handles use cases for display all boats and sending over to the HandleEditBoat handler that handles boats without a concern for members</summary>
        public void BoatHandler()
        {
            input = _boatController.DisplayBoatOptions();
            switch (input)
            {
                case ViewOperations.ShowAllBoats: _boatController.DisplayAllBoats(); break;
                case ViewOperations.EditBoat:
                    _boatController.DisplayAllBoats();
                    Boat boat = _boatController.FindBoatById();
                    HandleEditBoat(boat); break;
                case ViewOperations.SecretaryOptions: DisplayOptionsNav(); break;
                default: BoatHandler(); break;
            }
            BoatHandler();
        }
        /// <summary>Method <c>HandleEditBoat</c> Handles use cases for changing the length and type of a boat, deleting a boat. Also has options for returning to secretary options or back to manage boats</summary>
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
                    case ViewOperations.SecretaryOptions: DisplayOptionsNav(); break;
                    default: HandleEditBoat(boat); break;
                }
            }
            else
                _boatController.DisplayErrorBoatNotFound();
            BoatHandler();
        }
    }
    #endregion
}