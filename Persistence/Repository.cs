using System;
using System.Collections.Generic;
using Model;

namespace Persistence
{
    public class Repository
    {
        private readonly IDbContext _context;
        public Repository(IDbContext context)
        {
            _context = context;
        }

        public List<Boat> GetAllBoats()
        {
            return _context.Boats();
        }

        public List<Member> GetAllMembers()
        {
            return _context.Members();
        }

        public Member SelectMemberById(int id)
        {
            List<Member> members = GetAllMembers();

            return members.Find(m => m.Id == id);
        }
        public void ChangeFirstName(Member member, string firstName)
        {
                _context.Members().Find(m => member == m).FirstName = firstName;
                _context.SaveChanges();
        }

                public void ChangeLastName(Member member, string lastName)
        {
                _context.Members().Find(m => member == m).FirstName = lastName;
                _context.SaveChanges();
        }

        public void RemoveBoatFromMember(Member member, Boat boat)
        {
            _context.Members().Find(m => m == member).Boats.Remove(boat);
            _context.Boats().Remove(boat);
            _context.SaveChanges();
        }

        public void AddBoatToMember(Member member, Boat boat)
        {
            _context.Members().Find(m => m == member).Boats.Add(boat);
            _context.SaveChanges();
        }

        public void AddMember(Member member)
        {
            _context.Members().Add(member);
            _context.SaveChanges();
        }

        public void RemoveMember(Member member)
        {
            _context.Members().Remove(member);
            _context.SaveChanges();
        }

        public Member GetOwnerOfBoat(Boat boat)
        {
            List<Member> members = GetAllMembers();

            foreach (Member member in members)
            {
                if (member.Boats.Contains(boat))
                {
                    return member;
                }
            }
            return null;
        }

        public Boat FindBoatById(int id)
        {
            return _context.Boats().Find(b => b.Id == id);
        }

        public IEnumerable<Boat> FindBoatByType(string type)
        {
            return _context.Boats().FindAll(b => b.BoatType.ToString() == type);
        }

        public IEnumerable<Boat> FindBoatByLength(double length)
        {
            return _context.Boats().FindAll(b => b.Length == length);
        }


    }
}