using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace assignment2 {
    public class Register {

        private List<Boat> _boats;
        private List<Member> _members;
        protected string dataPath = "Data/MemberData.json";

        public Register() {
            _members = getMembersFromJson();
            _boats = GetBoats();
        }

        private List<Boat> GetBoats() {

            List<Boat> boats = new List<Boat>();
            foreach(Member member in _members)
            {
                foreach(Boat boat in member.boats)
                {
                boats.Add(boat);
                }
            }
            return boats;
        }

        public Boat searchBoat(string param, string value) {
                if (param == "Length") {
                   return _boats.Find(x => x.Length == Convert.ToDouble(value));
                }          
                else if(param == "Type") {
                    return _boats.Find(x => x.Type == value);
                }
                else  {
                    return _boats.Find(x => x.Id == Convert.ToInt32(value));
                }
            }

        public List<Boat> getBoatsFromRegistery() {
            return _boats;
        }

        public int totalBoats() {
            int totalBoats = _boats.Count;
            return totalBoats;
        }

        public List<Member> getMembers() {
            return _members;
        }

        private int createMemberId() {
            return 1001 +_members.Count;
        }

        private int createBoatId() {
            return 1001 + _boats.Count;
        }

        public Member CreateMember(string firstName,string lastName,int socialSecurityNumber) {
            int id = createMemberId();
            Member member = new Member(firstName,lastName,socialSecurityNumber,id);
            return member;
        }
        public Member UpdateMember(Member member, string firstName, string lastName) {
            member.FirstName = firstName;
            member.LastName = lastName;
            return member;
        }
        public void DeleteBoat(Boat boat) {     
            Console.WriteLine($"Båt med Id: {boat.Id} innuti Delete funktion");
            foreach(Member member in _members)
            {
                foreach(Boat boatInList in member.boats)
                {
                    if(boatInList == boat)
                    member.boats.Remove(boat);
                    _boats.Remove(boat);
                    updateFile();
                    break;
                }
            }
        }

        public Boat UpdateBoat(Boat boat, string type, double length) {
            boat.Type = type;
            boat.Length = length;
            return boat;
        }

            public void CreateBoat(string boatType,double length, Member member) {
            int id = createBoatId();
            id = id * member.Id / member.FirstName.Length;
            Boat boat = new Boat(boatType,length,id);
            _boats.Add(boat);
            member.boats.Add(boat);
        }
        
        public bool isInputOptionValid(int input, int from, int to) {
            
            if(input >= from && input <= to) {
                return true;
            }

            return false;
        }

        public void updateFile() {

            StreamWriter sw = new StreamWriter(dataPath);
            sw.Write(JsonConvert.SerializeObject(_members));
            sw.Close();

        }



        public List<Member> getMembersFromJson() {
            List<Member> memberList = new List<Member>();
             
             using(StreamReader reader = new StreamReader(dataPath)) {

                memberList = JsonConvert.DeserializeObject<List<Member>>(reader.ReadToEnd());
            }
            return memberList;
        }

        public void saveMemberToFile(Member member) {
            var jsonMember = JsonConvert.SerializeObject(member);
            List<object> jsonObjectList = new List<object>();
            object[] jsonArr;

            
            if(!File.Exists(dataPath)) 
            {
                var file = File.Create(dataPath);
            }
           
           
             using(StreamReader reader = new StreamReader(dataPath)) {

                jsonObjectList = JsonConvert.DeserializeObject<List<object>>(reader.ReadToEnd());
                jsonObjectList.Add(JsonConvert.DeserializeObject(jsonMember));
                jsonArr = jsonObjectList.ToArray();
            }

            StreamWriter sw = new StreamWriter(dataPath);
            sw.Write(JsonConvert.SerializeObject(jsonArr));
            sw.Close();
            
            _members.Add(member);
            GetBoats();
        }
    }
}
