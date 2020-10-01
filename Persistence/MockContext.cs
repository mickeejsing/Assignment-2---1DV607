using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Persistence;
using Model;

namespace Persistence
{
    public class MockContext : IDbContext
    {

        public List<Boat> boats;
        public List<Member> members;
        protected string dataPath = "Data/MockData.json";

        public MockContext()
        {
            members = loadData();
            boats = LoadBoats();
        }

        private List<Boat> LoadBoats()
        {
            List<Boat> boats = new List<Boat>();
            foreach (Member member in members)
            {
                foreach (Boat boat in member.boats)
                {
                    boats.Add(boat);
                }
            }
            return boats;
        }

        private List<Member> loadData()
        {
            List<Member> memberList = new List<Member>();

            using (StreamReader reader = new StreamReader(dataPath))
            {

                memberList = JsonConvert.DeserializeObject<List<Member>>(reader.ReadToEnd());
            }
            return memberList;
        }

        public List<Boat> Boats()
        {
            return boats;
        }

        public List<Member> Members()
        {
            return members;
        }

        public void SaveChanges()
        {
            StreamWriter sw = new StreamWriter(dataPath);
            sw.Write(JsonConvert.SerializeObject(members));
            sw.Close();
        }
    }
}
