using System;
using Model;

namespace View
{
    public class BoatView : BaseView
    {
        public void DisplaySelectedBoatOptions()
        {
            Console.WriteLine("Delete boat [db] Change type [cbt] Change length [cbl] Manage boats [mb] Back to Secretary Options [s]");
        }
        public void DisplayErrorBoatNotFound()
        {
            Console.WriteLine("Sorry, no boats were found...");
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
            Boolean valid = false;
            int id = 0;
            while (!valid)
            {
                try
                {
                    Console.Write("The length of the boat is: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    valid = true;

                }
                catch
                {
                    Console.WriteLine("Please only use numbers");
                }
            }

            return id;
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
                catch
                {
                    Console.WriteLine("Please only use numbers");
                }
            }

            return boatLength;
        }
        public string GetSearchParam()
        {
            Console.Write("Chose property to filter on: ");
            string param = Console.ReadLine();
            return param;
        }
        public void DisplayBoatUpdated(Boat boat)
        {
            Console.WriteLine($"Boat with Id: {boat.Id} was updated");
        }

        public void DisplaySingleBoat(Boat boat)
        {
            Console.WriteLine($"Boat type: {boat.BoatType.ToString()}, Boat Length: {boat.Length}, Boat Id: {boat.Id}");
        }

        public string GetSearchValue()
        {
            Console.Write("Chose the value you want to look for: ");
            string value = Console.ReadLine();
            return value;
        }
        public void DisplayBoatDeleted()
        {
            Console.WriteLine("The boat was deleted");
        }

        public void DisplayBoatOptions()
        {
            Console.WriteLine("Show all boats [sab] Edit boat [eb] Back to Secretary Options [s]");
        }

        public void DisplayManageMemberBoats()
        {
            Console.WriteLine("Add boat [amb] Delete boat [dmb] Edit member boat [emb]");
        }

        public void DisplayNoBoatWithId()
        {
            Console.WriteLine("No boat with given Id was found");
        }
    }
}