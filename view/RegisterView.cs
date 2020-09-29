using System;
using enums;
using view;

namespace assignment2
{
    public class RegisterView : View
    {


        public void displayLogin()
        {
            Console.WriteLine("Login screen");
            Console.WriteLine("Member [1]");
            Console.WriteLine("Secretary [2]");
        }
        public void displaySecretaryOptions()
        {
            Console.WriteLine("Manage boats [mb]");
            Console.WriteLine("Manage members [mm]");
        }

        public void displayMemberNotFound()
        {
            Console.WriteLine("Sorry, the member was not found");
        }

        public string getBoatType()
        {
            Console.WriteLine("What type of boat do you have?");
            Console.WriteLine("Only the boats bellow are valid");
            string boatType;

            Boolean valid = false;

            var types = Enum.GetValues(typeof(Type));

            foreach (var type in types)
            {
                Console.WriteLine(type.ToString());
            }

            // Get input

            do
            {
                boatType = Console.ReadLine();

                if (Type.Canoe.ToString().ToLower() == boatType.ToLower())
                {
                    return Type.Canoe.ToString();
                }
                else if (Type.Kayak.ToString().ToLower() == boatType.ToLower())
                {
                    return Type.Kayak.ToString();
                }
                else if (Type.Motorsailer.ToString().ToLower() == boatType.ToLower())
                {   return Type.Motorsailer.ToString();

                }
                else if (Type.Other.ToString().ToLower() == boatType.ToLower())
                {
                    return Type.Other.ToString();
                }
                else if (Type.Sailboat.ToString().ToLower() == boatType.ToLower())
                {
                    return Type.Sailboat.ToString();
                }
            } while (!valid);
            return Type.Other.ToString();
        }

        public void displayErrorNoBoatFound() {
            Console.WriteLine("Sorry, no boats were found...");
        }

        public string getValidBoats(Member member) {
            bool isValid = false;
            string boatType;

            if(member.boats.Count != 0) {
            Console.Write("Remove one of the following boats: ");
            foreach(Boat boat in member.boats) {
                Console.Write(boat.Type + ",");
            }
            Console.Write(" or press q to quit");
            Console.WriteLine();

            do {
                boatType = Console.ReadLine();
                foreach(Boat boat in member.boats) {
                    if (boatType.ToLower() == boat.Type.ToLower()) {
                        isValid = true;
                    }
                }
                if(boatType.ToLower() == "q") {
                    isValid = true;
                }
            } while(!isValid);
            return boatType;
            }
            else {
                return "error";
            }
        }
    

        public double getBoatLength()
        {
            Console.WriteLine("How long is the boat?");
            double boatLength = 0;
            Boolean valid = false;


            try
            {
                boatLength = Convert.ToDouble(Console.ReadLine());

            }
            catch (Exception ex)
            {
                while (!valid)
                {
                    Console.WriteLine("Exception message: " + ex.Message);
                    Console.WriteLine("How long is the boat?");
                    boatLength = Convert.ToDouble(Console.ReadLine());
                    valid = true;
                }
            }
            return boatLength;
        }

        public void displaySelectedMemberOptions()
        {
            Console.WriteLine("Delete member [dm] Edit member [em] Manage boats [mmb]");
        }

        public void displaySecretaryManagaeMemberBoats() {
            Console.WriteLine("Add boat [amb] Delete boat [dmb] Edit [emb]");
        }



        public void displaySecretaryMemberOptions()
        {
            Console.WriteLine("Create member [cm] Show members [sm] Select member [selm]");
        }

        // Skapa medlem


        public int getSocialSecurityNumber()
        {
            Console.Write("Type The members social security number: ");
            int socialSecurityNumber = Convert.ToInt32(Console.ReadLine());
            return socialSecurityNumber;
        }
        public string getFirstName()
        {
            Console.Write("Type The members first name: ");
            string firstName = Console.ReadLine();
            return firstName;
        }

        public string getLastName()
        {
            Console.Write("Type The members last name: ");
            string lastName = Console.ReadLine();
            return lastName;
        }

        public void displayGetMemberDisplayFormat()
        {
            Console.WriteLine("Show Members Verbose [smv] Show Members Compact [smc]");
        }

        public void displayBoatOptions()
        {
            Console.WriteLine("Register boat [1] Delete boat [2] Change boat information [3]");
        }

        public void displayMembersVerbose(Member member)
        {
            Console.WriteLine(member.FirstName);

        }

        public void displayMembersCompact(Member member)
        {
            Console.WriteLine(member.LastName);
        }

        public void registerBoat()
        {
            Console.WriteLine("Register boat");
        }

        public void deleteBoat()
        {
            Console.WriteLine("Delete boat");
        }

        public void changeBoat()
        {
            Console.WriteLine("Change boat information");
        }

        public void createMember()
        {
            Console.WriteLine("Create member");
        }

        public void changeMember()
        {
            Console.WriteLine("Change member information");
        }

        public void deleteMember()
        {
            Console.WriteLine("Delete Member");
        }

        public void showMember()
        {
            Console.WriteLine("Show member by entering ID or show members by listing.");
        }

        // Nu kanske? Ja

    }
}