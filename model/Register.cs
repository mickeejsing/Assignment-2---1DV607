using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace assignment2 {
    public class Register {

        private List<Boat> _boats = new List<Boat>();
        private List<Member> _members = new List<Member>();
        protected string dataPath = "Data/MemberData.json";

        public Register() {
            _members = getMembersFromJson();
        }

        public List<Member> getMembers() {
            return _members;
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
        }
    }
}
