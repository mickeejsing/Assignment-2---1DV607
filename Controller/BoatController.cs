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
        Enum input;
        public BoatController(BoatView boatView, Repository repository)
        {
            _boatView = boatView;
            _repository = repository;
        }
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
        public void ManageBoat()
        {
            Boat boat = SearchBoatById();
            if (boat != null)
            {
                HandleEditBoat(boat);
            }
            else
                _boatView.DisplayErrorBoatNotFound();

        }

        public void HandleDisplaySingleBoat()
        {
            var boats = SearchBoat().GetEnumerator();

            if (boats.Current == null)
                _boatView.DisplayErrorBoatNotFound();
            else
                while(boats.Current != null)
                {
                    _boatView.DisplaySingleBoat(boats.Current);
                    boats.MoveNext();
                }
        }

        public void HandleEditBoat(Boat boat)
        {
            if (boat != null)
            {
                _boatView.DisplaySelectedBoatOptions();
                input = _boatView.GetViewOperation();

                switch (input)
                {
                    case ViewOperations.EditBoatLength: HandleEditLBoatLength(boat); break;
                    case ViewOperations.EditBoatType: HandleEditBoatType(boat); break;
                    case ViewOperations.DeleteBoat: HandleDeleteBoat(boat); break;
                    default: HandleEditBoat(boat); break;
                }

            }
            else
                _boatView.DisplayErrorBoatNotFound();
        }

        public void ShowAllBoats()
        {
            List<Boat> boats = _repository.GetAllBoats();
            if (boats.Count != 0)
                _boatView.DisplayAllBoats(boats);
            else
                _boatView.DisplayErrorBoatNotFound();
        }


        public IEnumerable<Boat> SearchBoat()
        {
            {
                ShowAllBoats();
                string param = _boatView.GetSearchParam();
                

                switch (param)
                {
                    case "Type": return HandleFindBoatByType();
                    case "Length": return HandleFindBoatByLength(); 
                    default: return null;
                }
            }
        }
        public Boat SearchBoatById()
        {
            return HandleFindBoatById();
        }
        public IEnumerable<Boat> HandleFindBoatByType()
        {
            string type = _boatView.GetSearchValue();
            return _repository.FindBoatByType(type);
        }
        public Boat HandleFindBoatById()
        {
            int id = _boatView.GetBoatId();
            return _repository.FindBoatById(id);
        }

        public IEnumerable<Boat> HandleFindBoatByLength()
        {
            double length = _boatView.GetBoatLength();
            return _repository.FindBoatByLength(length);
        }

        public void HandleDeleteBoat(Boat boat)
        {
            Member member = _repository.GetOwnerOfBoat(boat);
            if (member != null)
                _repository.RemoveBoatFromMember(member, boat);
            else
                _boatView.DisplayMemberNotFound();
        }

        public void HandleEditLBoatLength(Boat boat)
        {

            double boatLength = _boatView.GetBoatLength();

            Member member = _repository.GetOwnerOfBoat(boat);
            {
                _repository.RemoveBoatFromMember(member, boat);
                boat.Length = boatLength;
                _repository.AddBoatToMember(member, boat);
            }

        }

        public void HandleEditBoatType(Boat boat)
        {
            BoatType boatType = _boatView.GetBoatType();
            Member member = _repository.GetOwnerOfBoat(boat);
            {
                if (member != null)
                {
                    _repository.RemoveBoatFromMember(member, boat);
                    boat.BoatType = boatType;
                    _repository.AddBoatToMember(member, boat);
                }
            }
        }

        public Boat HandleCreateBoat()
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

        public void HandleDeleteBoat(Member selectedMember)
        {
            _boatView.DisplayMemberBoatInfo(selectedMember);
            Boat boatFromSearch = SearchBoatById();
            _repository.RemoveBoatFromMember(selectedMember, boatFromSearch);
        }
    }
}