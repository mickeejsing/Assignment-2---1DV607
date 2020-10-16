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
            return _context.GetBoats();
        }

        public List<Member> GetAllMembers()
        {
            return _context.GetMembers();
        }

        public Member SelectMemberById(int id)
        {
            List<Member> members = GetAllMembers();

            return members.Find(m => m.Id == id);
        }
        public void ChangeFirstName(Member member, string firstName)
        {
            _context.GetMembers().Find(m => member == m).FirstName = firstName;
            _context.SaveChanges();
        }

        public void ChangeLastName(Member member, string lastName)
        {
            _context.GetMembers().Find(m => member == m).LastName = lastName;
            _context.SaveChanges();
        }

        public void RemoveBoatFromMember(Boat boat)
        {
            _context.GetMembers().Find(m => m.Boats.Contains(boat)).Boats.Remove(boat);
            _context.GetBoats().Remove(boat);
            _context.SaveChanges();
        }

        public void AddBoatToMember(Member member, Boat boat)
        {
            _context.GetMembers().Find(m => m == member).Boats.Add(boat);
            _context.SaveChanges();
        }
        public void EditBoatLength(Boat boat, double length)
        {
            _context.GetBoats().Find(b => b == boat).Length = length;
            SaveChanges();
        }

        public void EditBoatType(Boat boat, BoatType boatType)
        {
            _context.GetBoats().Find(b => b == boat).BoatType = boatType;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddMember(Member member)
        {
            _context.GetMembers().Add(member);
            _context.SaveChanges();
        }

        public void RemoveMember(Member member)
        {
            _context.GetMembers().Remove(member);
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
            return _context.GetBoats().Find(b => b.Id == id);
        }
    }
}