using System;
using System.Collections.Generic;
using Model;
using Persistence;
using View;

namespace Controller
{
    public class BoatController
    {
        private readonly BoatView _boatView;
        private readonly Repository _repository;
        public BoatController(BoatView boatView, Repository repository)
        {
            _boatView = boatView;
            _repository = repository;
        }

        #region Enum methods
        /// <summary>Method <c>DisplayManageMemberBoats</c> Displays Options for managing the boats of a Member
        /// And returns the Option the user chooses</summary>
        public Enum DisplayManageMemberBoats()
        {
            _boatView.DisplayManageMemberBoats();
            return _boatView.GetViewOperation();
        }
        /// <summary>Method <c>DisplayBoatOptions</c> Displays Options for chosing what ViewOperation the user wants to do.
        /// These choices are: <c>Show All Boats</c>, <c>Edit Boat</c> and <c>Back to Secretary Options</c></summary>
        public Enum DisplayBoatOptions()
        {
            _boatView.DisplayBoatOptions();
            return _boatView.GetViewOperation();
        }
        /// <summary>Method <c>DisplaySelectedBoatOptions</c> Displays the options the user can take when a boat is chosen.
        /// The user may: <c>Delete Boat</c>, <c>Change the type of boat</c>, <c>Change the Length of the boat</c>, <c>Go back to manage boats</c>, <c>Back to Secretary Options</c></summary>
        public Enum DisplaySelectedBoatOptions()
        {
            _boatView.DisplaySelectedBoatOptions();

            return _boatView.GetViewOperation();
        }


        #endregion

        #region Display methods
        /// <summary>Method <c>DisplaySingleBoat</c> Uses the View method to print out the boat passed in</summary>
        public void DisplaySingleBoat(Boat boat)
        {
            _boatView.DisplaySingleBoat(boat);
        }
        /// <summary>Method <c>DisplayBoatDeleted</c> Uses the View method to print that a boat has been deleted</summary>
        public void DisplayBoatDeleted()
        {
            _boatView.DisplayBoatDeleted();
        }
        /// <summary>Method <c>DisplayErrorBoatNotFound</c> Uses the View method to tell the user that a boat wasnt found</summary>
        public void DisplayErrorBoatNotFound()
        {
            _boatView.DisplayErrorBoatNotFound();
        }

        /// <summary>Method <c>DisplayAllBoats</c> Gets all the boats from the repository and then itterates through the list calling the 
        /// view method to display the current boat. If no boat is found tell the user there was not boats.</summary>
        public void DisplayAllBoats()
        {
            List<Boat> boats = _repository.GetAllBoats();
            if (boats.Count != 0)
                boats.ForEach(boat => _boatView.DisplaySingleBoat(boat));
            else
                _boatView.DisplayErrorBoatNotFound();
        }

        #endregion

        #region CRUD
        /// <summary>Method <c>DeleteBoat</c> Uses the repository to remove a boat from the member that owns the boat</summary>
        public void DeleteBoat(Boat boat)
        {
            _repository.RemoveBoatFromMember(boat);
        }
        /// <summary>Method <c>EditBoatLength</c> Updates the Length of the boat passed in</summary>
        public void EditLBoatLength(Boat boat)
        {

            double boatLength = _boatView.GetBoatLength();
            _repository.EditBoatLength(boat, boatLength);
        }

        /// <summary>Method <c>EditBoatType</c> Updates the Type of the boat passed in</summary>
        public void EditBoatType(Boat boat)
        {
            BoatType boatType = _boatView.GetBoatType();
            _repository.EditBoatType(boat, boatType);
        }
        /// <summary>Method <c>CreateBoat</c> Gets the Boat type and Boat length from the User, then creates a Boat object</summary>
        public Boat CreateBoat()
        {
            BoatType boatType = _boatView.GetBoatType();
            double boatLength = _boatView.GetBoatLength();
            Boat boat = new Boat
            {
                BoatType = boatType,
                Length = boatLength
            };
            return boat;
        }
        /// <summary>Method <c>DeleteBoat</c> Shows all boats from a given user and then deletes the boat the user choses to delete. Returns true if the boat was removed</summary>
        #endregion
        public bool DeleteBoat(List<Boat> boats)
        {
            boats.ForEach(boat => _boatView.DisplaySingleBoat(boat));
            Boat boatFromSearch = FindBoatById();
            if (boatFromSearch == null)
            {
                _boatView.DisplayNoBoatWithId();
                return false;
            }
            _repository.RemoveBoatFromMember(boatFromSearch);
            return true;
        }

        #region 
        public void AddBoatToMember(Member member, Boat boat)
        {
            _repository.AddBoatToMember(member, boat);
        }
        public Boat FindBoatById()
        {
            int id = _boatView.GetBoatId();
            return _repository.FindBoatById(id);
        }
        #endregion
    }
}