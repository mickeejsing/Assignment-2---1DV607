using System;
using System.Linq;
using Enums;
using Persistence;
using View;
using Model;
using Factories;

namespace Controller
{
    public class MemberController
    {
        private readonly MemberView _memberView;
        private readonly IDbContext _dbContext;
        private readonly Factory _factory;
        private Enum input;

        public MemberController(MemberView memberView, IDbContext dbContext, Factory factory)
        {
            _memberView = memberView;
            _dbContext = dbContext;
            _factory = factory;
        }

        public void mainNav()
        {
            do
            {
                _memberView.displayLogin();
                input = _memberView.getViewOperation();
                switch (input)
                {
                    case ViewOperations.isSecretary:
                        {
                            secretaryNav();
                            break;
                        }
                    case ViewOperations.isMember:
                        {
                            memberNav();
                            break;
                        }
                }
                if (input.Equals(ViewOperations.Quit))
                {
                    break;
                }
            } while (true);
        }
        public void secretaryNav()
        {
            _memberView.displaySecretaryOptions();
            {
                input = _memberView.getViewOperation();
                if (input.Equals(ViewOperations.ManageMembers))
                {
                    secretaryMemberHandler();
                }
                else if (input.Equals(ViewOperations.ManageBoats))
                {
                    _factory.CreateBoatController().secretaryBoatHandler();
                }
            }
        }


        public void secretaryMemberHandler()
        {
            _memberView.displaySecretaryMemberOptions();
            input = _memberView.getViewOperation();

            if (input.Equals(ViewOperations.CreateMember))
            {
                string firstName = _memberView.getFirstName();
                string lastName = _memberView.getLastName();
                int socialSecurityNumber = _memberView.getSocialSecurityNumber();
                Member member = _factory.CreateMember(firstName, lastName, socialSecurityNumber);
                Boat boat = HandleCreateBoat();
                member.boats.Add(boat);
                _dbContext.SaveChanges();
                _dbContext.Members().Add(member);
                _dbContext.SaveChanges();
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
                _dbContext.Members().ForEach(m => _memberView.displayMembersCompact(m));
                Member member = selectMember();

                _memberView.displaySelectedMemberOptions();
                input = _memberView.getViewOperation();
                if (input.Equals(ViewOperations.DeleteMember))
                {
                    _dbContext.Members().Remove(member);
                    _dbContext.SaveChanges();
                    secretaryNav();

                }

                if (input.Equals(ViewOperations.ShowMemberDetails))
                {
                    _memberView.displayMembersVerbose(member);
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
                    _memberView.displaySecretaryManagaeMemberBoats();
                    input = _memberView.getViewOperation();

                    if (input.Equals(ViewOperations.AddMemberBoat))
                    {
                        Boat boat = HandleCreateBoat();
                        _dbContext.Members().Find(m => m == member).boats.Add(boat);
                        _dbContext.SaveChanges();
                        secretaryNav();
                    }

                    if (input.Equals(ViewOperations.DeleteMemberBoat))
                    {
                        bool detailedSearch = false;
                        _memberView.displayMemberBoatInfo(member);
                        Boat boat = searchBoat(detailedSearch);
                        _dbContext.Members().Find(m => member == m).boats.Remove(boat);
                        _dbContext.SaveChanges();
                        secretaryNav();
                    }

                    if (input.Equals(ViewOperations.EditMemberBoat))
                    {
                        Boat boat = member.boats.Where(b => b.Type == "sailboat").FirstOrDefault();
                        double boatLenth = _memberView.getBoatLength();
                        string boatType = _memberView.getBoatType();
                        member.boats.Remove(boat);
                        _dbContext.Members().Remove(member);

                        boat.Type = boatType;
                        boat.Length = boatLenth;
                        member.boats.Add(boat);

                        _dbContext.Members().Add(member);
                        secretaryNav();
                        _dbContext.SaveChanges();

                    }
                }
            }
        }


        public void handleEditMember(Member member)
        {
            _memberView.displayEditMemberOptions();
            input = _memberView.getViewOperation();

            if (input.Equals(ViewOperations.EditFirstName))
            {
                string firstName = _memberView.getFirstName();
                _dbContext.Members().Find(m => member == m).FirstName = firstName;
                _dbContext.SaveChanges();
                input = ViewOperations.EditMember;

            }

            else if (input.Equals(ViewOperations.EditLastName))
            {
                string lastName = _memberView.getLastName();
                _dbContext.Members().Find(m => member == m).FirstName = lastName;
                _dbContext.SaveChanges();
                input = ViewOperations.EditMember;
            }
            else if (input.Equals(ViewOperations.SecretaryOptions))
            {
                secretaryNav();
            }
        }

        public void ShowAllBoats()
        {
            _memberView.displayAllBoats(_dbContext.Boats());
        }

        public Boat HandleCreateBoat()
        {
            string boatType = _memberView.getBoatType();
            double boatLength = _memberView.getBoatLength();
            Boat boat = _factory.CreateBoat(boatType, boatLength);
            return boat;
        }
        public Boat searchBoat(bool detailed = true)
        {
            if (detailed)
            {

                string param = _memberView.getSearchParam();
                string value = _memberView.getSearchValue();
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
                string value = _memberView.getSearchValue();
                return _dbContext.Boats().Find(b => b.Id == Convert.ToInt32(value));
            }
            _memberView.displayErrorNoBoatFound();
            return null;
        }

        // kolla på sen
        public Member selectMember()
        {
            Member member;
            string parameter;
            do
            {
                parameter = _memberView.getFirstName();
                member = _dbContext.Members().Where(m => m.FirstName == parameter).FirstOrDefault();
                if (member == null)
                {
                    _memberView.displayMemberNotFound();
                }
            } while (member == null);
            return member;
        }

        public void displayMembers()
        {
            _memberView.displayGetMemberDisplayFormat();
            input = _memberView.getViewOperation();

            if (input.Equals(ViewOperations.ShowMembersVerbose))
                _dbContext.Members().ForEach(m => _memberView.displayMembersVerbose(m));
            if (input.Equals(ViewOperations.ShowMembersCompact))
                _dbContext.Members().ForEach(m => _memberView.displayMembersCompact(m));
        }

        public void memberNav()
        {

        }

    }
}
