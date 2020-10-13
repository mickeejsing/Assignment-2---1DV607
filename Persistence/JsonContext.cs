using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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
            members = LoadData();
            boats = LoadBoats();
        }

        private List<Boat> LoadBoats()
        {
            List<Boat> boats = new List<Boat>();
            foreach (Member member in members)
            {
                foreach (Boat boat in member.Boats)
                {
                    boats.Add(boat);
                }
            }
            return boats;
        }

        private List<Member> LoadData()
        {
            List<Member> memberList = new List<Member>();

            bool dataDirExists = Directory.Exists("./data");

            if(!dataDirExists) {
                Console.WriteLine("Creating data directory");
                Directory.CreateDirectory("data");
            }
            try
            {
                StreamReader reader = new StreamReader(dataPath);
                memberList = JsonConvert.DeserializeObject<List<Member>>(reader.ReadToEnd());
                reader.Close();
                return memberList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Creating file");
                FileStream fs = File.Create(dataPath);
                fs.Close();
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
