using System.Collections.Generic;
using Model;

namespace Persistence
{
    public interface IDbContext
    {
        List<Boat> Boats();
        List<Member> Members();
        void SaveChanges();
    }
}