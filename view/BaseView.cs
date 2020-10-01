using System;
using System.Collections.Generic;
using Model;
using enums;

namespace View
{
    public abstract class BaseView
    {
        public Enum getViewOperation()
        {

            string input = getInput();

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
                    return ViewOperations.editBoatLength;
                default:
                    break;
            }
            return ViewOperations.Quit;
        }
        public string getInput()
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
                Console.WriteLine("Something went wrong: " + e);
                return "error";
            }
        }
        public string getSearchValue()
        {
            Console.Write("Chose the value you want to look for: ");
            string value = Console.ReadLine();
            return value;
        }

        public void displayAllBoats(List<Boat> boats)
        {
            foreach (Boat boat in boats)
            {
                Console.WriteLine($"{boat.Type} {boat.Length} {boat.Id}");

            }
        }
        public string getSearchParam()
        {
            Console.Write("Chose property to filter on: ");
            string param = Console.ReadLine();
            return param;
        }

        public void displaySingleBoat(Boat boat)
        {
            Console.WriteLine($"{boat.Type} {boat.Length} {boat.Id}");
        }
        protected String formatString(string str)
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

        public void displayErrorNoBoatFound()
        {
            Console.WriteLine("Sorry, no boats were found...");
        }

        public double getBoatLength()
        {
            Console.WriteLine("How long is the boat?");
            double boatLength = 0;
            Boolean valid = false;

            while (!valid)
            {
                try
                {
                    Console.Write("The length of the boat is: : ");
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

        public string getBoatType()
        {
            Console.WriteLine("What type of boat does the member have?");
            Console.WriteLine("Only the boats bellow are valid");
            string boatType;

            Boolean valid = false;

            var types = Enum.GetValues(typeof(Type));

            foreach (var type in types)
            {
                Console.Write(type.ToString() + ", ");
            }
            Console.WriteLine();
            do
            {
                Console.Write("Type in the boat: ");
                boatType = Console.ReadLine();

                foreach (var type in types)
                {
                    if (boatType.ToLower() == type.ToString().ToLower())
                    {
                        valid = true;
                    }
                }
                if (!valid)
                {
                    Console.WriteLine(boatType + " was not found");
                }
            } while (!valid);

            return formatString(boatType);
        }
    }


}