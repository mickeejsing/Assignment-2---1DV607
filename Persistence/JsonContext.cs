using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Persistence;
using Model;

namespace Persistence
{
    public class JsonContext : IDbContext
    {

        private List<Boat> boats;
        private List<Member> members;
        protected string dataPath = "Data/MemberData.json";

        public JsonContext()
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


            try
            {
                StreamReader reader = new StreamReader(dataPath);
                memberList = JsonConvert.DeserializeObject<List<Member>>(reader.ReadToEnd());
                return memberList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldnt find data file, creating a new one");
                using (FileStream fs = File.Create(dataPath));
                   StreamWriter sw = new StreamWriter(dataPath);
                   sw.Write("[]");
                   sw.Close();
                }
                return new List<Member>();
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
