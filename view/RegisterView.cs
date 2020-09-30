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
                    if (boatType.ToLower() == type.ToString().ToLower()) {
                        valid = true;
                    }
                }
                if (!valid) {
                    Console.WriteLine(boatType + " was not found"); 
                }
            } while (!valid);

            return formatString(boatType);
        }

        private String formatString(string str) {
            string returnString = "";
            for(int i=0; i < str.Length; i++) {
                
                if(i == 0) {
                   returnString+= str[i];
                   returnString = returnString.ToUpper();
                }
                else {
                    returnString+= str[i].ToString().ToLower();
                }      
            }
            return returnString;
        }
        public void displayErrorNoBoatFound()
        {
            Console.WriteLine("Sorry, no boats were found...");
        }

        public string getValidBoats(Member member)
        {
            bool isValid = false;
            string boatType;

            if (member.boats.Count != 0)
            {
                Console.Write("Remove one of the following boats: ");
                foreach (Boat boat in member.boats)
                {
                    Console.WriteLine(boat.Type + ",");
                }
                Console.Write(" or press q to quit");
                Console.WriteLine();

                do
                {
                    boatType = Console.ReadLine();
                    foreach (Boat boat in member.boats)
                    {
                        if (boatType.ToLower() == boat.Type.ToLower())
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


        public double getBoatLength()
        {
            Console.WriteLine("How long is the boat?");
            double boatLength = 0;
            Boolean valid = false;


            try
            {
                Console.WriteLine("The boat is: ");
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
            Console.WriteLine("Delete member [dm] Edit member [em] Manage boats [mmb] Show member details [md]");
        }

        public void displaySecretaryManagaeMemberBoats()
        {
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
            Console.WriteLine("===============MEMBER===============");
            Console.WriteLine($"{member.FirstName} {member.LastName} {member.SocialSecurityNum} {member.Id}  ");
            Console.WriteLine("===============BOATS================");
            foreach(Boat boat in member.boats) {
                Console.WriteLine($"{boat.Type} {boat.Length} {boat.Id}");
            }
            Console.WriteLine("====================================");
        }

        public void displayMembersCompact(Member member)
        {
            Console.WriteLine($"{member.FirstName} {member.LastName} {member.Id} Boats: {member.boats.Count}");
        }

        public void displayEditMemberOptions() {
              Console.WriteLine("Edit Member First name [ef] Edit Member Last name [el] Back to secretary options [s] ");
        }

        public void deleteBoat()
        {
            Console.WriteLine("Delete boat");
        }

        public void changeBoat()
        {
            Console.WriteLine("Change boat information");
        }

        public void changeMember()
        {
            Console.WriteLine("Change member information");
        }

        // Nu kanske? Ja

    }
}