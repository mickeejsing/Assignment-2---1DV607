using System.Collections.Generic;
using Model;

namespace Persistence
{
    public interface IDbContext
    {
        List<Boat> GetBoats();
        List<Member> GetMembers();
        void SaveChanges();

    }
}