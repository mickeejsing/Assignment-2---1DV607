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

        // Get ViewOperation Enum
        public Enum DisplayManageMemberBoats()
        {
            _boatView.DisplayManageMemberBoats();
            return _boatView.GetViewOperation();
        }

        public Enum DisplayBoatOptions()
        {
            _boatView.DisplayBoatOptions();
            return _boatView.GetViewOperation();
        }
        public Enum DisplaySelectedBoatOptions()
        {
            _boatView.DisplaySelectedBoatOptions();

            return _boatView.GetViewOperation();
        }
        public Enum DisplayMemberSelectedBoatOptions()
        {
            _boatView.DisplaySelectedBoatOptions();
            return _boatView.GetViewOperation();
        }


        // Displays

        public void DisplaySingleBoat(Boat boat)
        {
            if (boat == null)
                _boatView.DisplayErrorBoatNotFound();
            else
                _boatView.DisplaySingleBoat(boat);
        }


        public void DisplayManageMemberBoats(Member member)
        {
            List<Boat> boats = member.Boats;
            boats.ForEach(boat => _boatView.DisplaySingleBoat(boat));
        }
        public void DisplayBoatDeleted()
        {
            _boatView.DisplayBoatDeleted();
        }

        public void DisplayErrorBoatNotFound()
        {
            _boatView.DisplayErrorBoatNotFound();
        }

        // Main menu

        // Find Boats
        public List<Boat> FindBoatsByType()
        {
            string type = _boatView.GetSearchValue();
            return _repository.FindBoatByType(type);
        }
        public Boat FindBoatById()
        {
            int id = _boatView.GetBoatId();
            return _repository.FindBoatById(id);
        }

        public List<Boat> FindBoatsByLength()
        {
            double length = _boatView.GetBoatLength();
            return _repository.FindBoatByLength(length);
        }

        public Boat SelectBoat()
        {
            List<Boat> boats = _repository.GetAllBoats();
            _boatView.DisplayAllBoats(boats);
            Boat boat = FindBoatById();
            if (boat != null)
            {
                return boat;
            }
            else
                _boatView.DisplayErrorBoatNotFound();
                return null;
        }

        // CRUD

        public void DeleteBoat(Boat boat)
        {
            Member member = _repository.GetOwnerOfBoat(boat);
            if (member != null)
                _repository.RemoveBoatFromMember(member, boat);
            else
                _boatView.DisplayMemberNotFound();
        }

        public void EditLBoatLength(Boat boat)
        {

            double boatLength = _boatView.GetBoatLength();
            _repository.EditBoatLength(boat, boatLength);
        }


        public void EditBoatType(Boat boat)
        {
            BoatType boatType = _boatView.GetBoatType();
            _repository.EditBoatType(boat, boatType);
        }

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
        public void AddBoatToMember(Member member, Boat boat)
        {
            _repository.AddBoatToMember(member, boat);
        }

        public bool DeleteBoat(Member selectedMember)
        {
            if (selectedMember.Boats.Count > 0)
            {
                _boatView.DisplayMemberBoatInfo(selectedMember);
                Boat boatFromSearch = FindBoatById();
                if (boatFromSearch == null)
                {
                    _boatView.DisplayNoBoatWithId();
                    return false;
                }
                _repository.RemoveBoatFromMember(selectedMember, boatFromSearch);
                return true;
            }
            else
            {
                _boatView.DisplayErrorBoatNotFound();
                return false;
            }
        }
        public void ShowAllBoats()
        {
            List<Boat> boats = _repository.GetAllBoats();
            if (boats.Count != 0)
                _boatView.DisplayAllBoats(boats);
            else
                _boatView.DisplayErrorBoatNotFound();
        }
        public List<Boat> SearchBoat()
        {
            {
                ShowAllBoats();
                string param = _boatView.GetSearchParam();


                switch (param)
                {
                    case "Type": return FindBoatsByType();
                    case "Length": return FindBoatsByLength();
                    default: return null;
                }
            }
        }


    }
}