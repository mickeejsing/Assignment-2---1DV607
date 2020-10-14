using System;
using System.Collections.Generic;
using Model;

namespace View
{
    public abstract class BaseView
    {
        public Enum GetViewOperation()
        {
            string input = GetInput();

            switch (input)
            {
                case ("1"):
                    return ViewOperations.isMember;
                case ("2"):
                    return ViewOperations.isSecretary;
                case ("s"):
                    return ViewOperations.SecretaryOptions;
                case ("b"):
                    return ViewOperations.ShowMainNav;
                case ("mm"):
                    return ViewOperations.ManageMembers;
                case ("mb"):
                    return ViewOperations.ManageBoats;
                case ("cm"):
                    return ViewOperations.CreateMember;
                case ("sm"):
                    return ViewOperations.ShowMembers;
                case ("dm"):
                    return ViewOperations.DeleteMember;
                case ("em"):
                    return ViewOperations.EditMember;
                case ("q"):
                    return ViewOperations.Quit;
                case ("smv"):
                    return ViewOperations.ShowMembersVerbose;
                case ("smc"):
                    return ViewOperations.ShowMembersCompact;
                case ("selm"):
                    return ViewOperations.SelectMember;
                case ("mmb"):
                    return ViewOperations.ManageMemberBoats;
                case ("amb"):
                    return ViewOperations.AddMemberBoat;
                case ("dmb"):
                    return ViewOperations.DeleteMemberBoat;
                case ("emb"):
                    return ViewOperations.EditMemberBoat;
                case ("ef"):
                    return ViewOperations.EditFirstName;
                case ("el"):
                    return ViewOperations.EditLastName;
                case ("md"):
                    return ViewOperations.ShowMemberDetails;
                case ("eb"):
                    return ViewOperations.EditBoat;
                case ("sab"):
                    return ViewOperations.ShowAllBoats;
                case ("sbi"):
                    return ViewOperations.ShowBoatFromId;
                case ("db"):
                    return ViewOperations.DeleteBoat;
                case ("cbt"):
                    return ViewOperations.EditBoatType;
                case ("cbl"):
                    return ViewOperations.EditBoatLength;
                default:
                    break;
            }
            return ViewOperations.Quit;
        }
        public void DisplayMemberBoatInfo(Member member)
        {
            foreach (Boat boat in member.Boats)
            {
                Console.WriteLine($"{boat.BoatType.ToString()} {boat.Length} {boat.Id}");
            }
            Console.WriteLine("====================================");
        }
        public string GetInput()
        {
            try
            {
                string s = Console.ReadLine().ToLower();
                while (s == "\r" || s == "\n")
                {
                    s = Console.ReadLine().ToLower();
                }
                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not get input: " + e);
                return "error";
            }
        }
        public string GetSearchValue()
        {
            Console.Write("Chose the value you want to look for: ");
            string value = Console.ReadLine();
            return value;
        }

        public string GetBoatTypeValue()
        {
            Console.Write("Chose which Boat Type to filter on");
            string value = Console.ReadLine();
            return value;
        }
        public void DisplayAllBoats(List<Boat> boats)
        {
            foreach (Boat boat in boats)
            {
                Console.WriteLine($"{boat.BoatType.ToString()} {boat.Length} {boat.Id}");
            }
        }
        public string GetSearchParam()
        {
            Console.Write("Chose property to filter on: ");
            string param = Console.ReadLine();
            return param;
        }

        public void DisplaySingleBoat(Boat boat)
        {
            Console.WriteLine($"{boat.BoatType.ToString()} {boat.Length} {boat.Id}");
        }
        protected String UppercaseFirstlLetter(string str)
        {
            string returnString = "";
            for (int i = 0; i < str.Length; i++)
            {

                if (i == 0)
                {
                    returnString += str[i];
                    returnString = returnString.ToUpper();
                }
                else
                {
                    returnString += str[i].ToString().ToLower();
                }
            }
            return returnString;
        }

        public void DisplayErrorBoatNotFound()
        {
            Console.WriteLine("Sorry, no boats were found...");
        }

        public double GetBoatLength()
        {
            Console.WriteLine("How many meters long is the boat?");
            double boatLength = 0;
            Boolean valid = false;

            while (!valid)
            {
                try
                {
                    Console.Write("The length of the boat is: ");
                    boatLength = Convert.ToDouble(Console.ReadLine());
                    valid = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception message: " + ex.Message);
                }
            }

            return boatLength;
        }

        public void DisplayMemberNotFound()
        {
            Console.WriteLine("Sorry, the member was not found");
        }
        public string GetValidBoats(Member member)
        {
            bool isValid = false;
            string boatType;

            if (member.Boats.Count != 0)
            {
                Console.Write("Remove one of the following boats: ");
                foreach (Boat boat in member.Boats)
                {
                    Console.WriteLine(boat.BoatType + ",");
                }
                Console.Write(" or press q to quit");
                Console.WriteLine();

                do
                {
                    boatType = Console.ReadLine();
                    foreach (Boat boat in member.Boats)
                    {
                        if (boatType.ToLower() == boat.BoatType.ToString().ToLower())
                        {
                            isValid = true;
                        }
                    }
                    if (boatType.ToLower() == "q")
                    {
                        isValid = true;
                    }
                } while (!isValid);
                return boatType;
            }
            else
            {
                return "error";
            }
        }
        public BoatType GetBoatType()
        {
            Console.WriteLine("What type of boat does the member have?");
            Console.WriteLine("Only the boats bellow are valid");
            string boatType;

            Boolean valid = false;
            var types = Enum.GetValues(typeof(BoatType));
            foreach (var type in types)
            {
                Console.Write(type.ToString() + ", ");
            }
            Console.WriteLine();
            do
            {
                Console.Write("Type in the Boat Type: ");
                boatType = Console.ReadLine();

                foreach (var type in types)
                {
                    if (boatType.ToLower() == type.ToString().ToLower())
                    {
                        valid = true;
                        return (BoatType)type;
                    }
                }
                if (!valid)
                {
                    Console.WriteLine(boatType + " was not found");
                }
            } while (!valid);
            throw new Exception("Something went wrong");
        }
        public int GetBoatId()
        {
            Console.Write("Input Boat Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            //TODO try-catch
            return id;
        }
    }
    public enum ViewOperations
    {
        isSecretary,
        isMember,
        SecretaryOptions,
        ManageMembers,
        ManageBoats,
        Quit,
        CreateMember,
        ShowMainNav,
        ShowMembers,
        DeleteMember,
        EditMember,
        ShowMembersVerbose,
        ShowMembersCompact,
        SelectMember,
        ManageMemberBoats,
        AddMemberBoat,
        DeleteMemberBoat,
        EditMemberBoat,
        EditFirstName,
        EditLastName,
        ShowMemberDetails,
        ShowAllBoats,
        ShowBoatFromId,
        EditBoat,
        EditBoatOptions,
        DeleteBoat,
        EditBoatType,
        EditBoatLength
    }
}