using System;
using enums;
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

            _boatView.displaySecretaryBoatOptions();
            input = _boatView.getViewOperation();

            if (input.Equals(ViewOperations.ShowAllBoats))
            {
                ShowAllBoats();
                secretaryBoatHandler();
            }
            else if (input.Equals(ViewOperations.ShowBoatFromId))
            {

                _boatView.displaySingleBoat(searchBoat());
                secretaryBoatHandler();
            }
            else if (input.Equals(ViewOperations.EditBoat))
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
                secretaryBoatHandler();
            }
            else
            {
                secretaryBoatHandler();
            }
        }
        public void ShowAllBoats()
        {
            _boatView.displayAllBoats(_dbContext.Boats());
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
            _boatView.displayErrorNoBoatFound();
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
                    double boatLength =_boatView.getBoatLength();
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
                    String boatType =_boatView.getBoatType();
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