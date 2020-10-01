using System;
using Enums;
using Model;
using Persistence;
using View;

namespace Controller
{
    public class BoatController
    {
        private readonly BoatView _boatView;
        private readonly IDbContext _dbContext;
        Enum input;
        public BoatController(BoatView boatView, IDbContext dbContext)
        {
            _boatView = boatView;
            _dbContext = dbContext;
        }

        public void secretaryBoatHandler()
        {

            bool stayInSecretaryMenu = true;
            _boatView.displaySecretaryBoatOptions();
            input = _boatView.getViewOperation();
            if(stayInSecretaryMenu)
            switch (input)
            {
                case ViewOperations.ShowAllBoats:
                    {
                        ShowAllBoats();
                        secretaryBoatHandler();
                        break;
                    }
                case ViewOperations.ShowBoatFromId:
                    {
                        bool detailedSearch = false;
                        Boat boat = searchBoat(detailedSearch);
                        if (boat != null)
                        {
                            _boatView.displaySingleBoat(boat);
                        }
                        _boatView.displayErrorNoBoatFound();
                        break;
                    }
                case ViewOperations.EditBoat:
                    {
                        Boat boat = searchBoat();
                        if (boat != null)
                        {
                            handleEditBoat(boat);
                        }
                        else
                        {
                            _boatView.displayErrorNoBoatFound();
                        }

                        break;
                    }
                case ViewOperations.SecretaryOptions:
                {
                    stayInSecretaryMenu=false;
                    break;
                }
                default:
                {
                    secretaryBoatHandler();
                    break;
                }
            }
        }
        public void ShowAllBoats()
        {
            if (_dbContext.Boats().Count != 0)
            {
                _boatView.displayAllBoats(_dbContext.Boats());
            }
            else
            {
                _boatView.displayErrorNoBoatFound();
            }
        }


        public Boat searchBoat(bool detailed = true)
        {
            if (detailed)
            {

                string param = _boatView.getSearchParam();
                string value = _boatView.getSearchValue();
                if (param == "Type")
                {
                    Boat boat = _dbContext.Boats().Find(b => b.Type == value);
                    return boat;
                }
                else if (param == "Length")
                {
                    Boat boat = _dbContext.Boats().Find(b => b.Length == Convert.ToDouble(value));
                    return boat;
                }
                else if (param == "Id")
                {
                    Boat boat = _dbContext.Boats().Find(b => b.Id == Convert.ToInt32(value));
                    return boat;
                }
            }
            else
            {
                string value = _boatView.getSearchValue();
                return _dbContext.Boats().Find(b => b.Id == Convert.ToInt32(value));
            }
            return null;
        }
        public void handleEditBoat(Boat boat)
        {
            while (!input.Equals(ViewOperations.ManageBoats))
            {
                _boatView.displaySecretarySingleBoatOptions();
                input = _boatView.getViewOperation();

                if (input.Equals(ViewOperations.DeleteBoat))
                {
                    foreach (Member memberInContext in _dbContext.Members())
                    {
                        if (memberInContext.boats.Contains(boat))
                        {
                            _dbContext.Members().Find(m => m == memberInContext).boats.Remove(boat);
                            _dbContext.Boats().Remove(boat);
                            _dbContext.SaveChanges();
                        }
                    }

                    input = ViewOperations.ManageBoats;
                }
                else if (input.Equals(ViewOperations.editBoatLength))
                {
                    // Hämta längd
                    double boatLength = _boatView.getBoatLength();
                    foreach (Member memberInContext in _dbContext.Members())
                    {
                        if (memberInContext.boats.Contains(boat))
                        {
                            _dbContext.Members().Find(m => m == memberInContext).boats.Remove(boat);
                            boat.Length = boatLength;
                            _dbContext.Members().Find(m => m == memberInContext).boats.Add(boat);
                        }
                    }
                    _dbContext.SaveChanges();
                }
                else if (input.Equals(ViewOperations.EditBoatType))
                {
                    String boatType = _boatView.getBoatType();
                    foreach (Member memberInContext in _dbContext.Members())
                    {
                        if (memberInContext.boats.Contains(boat))
                        {
                            _dbContext.Members().Find(m => m == memberInContext).boats.Remove(boat);
                            boat.Type = boatType;
                            _dbContext.Members().Find(m => m == memberInContext).boats.Add(boat);
                        }
                    }
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}