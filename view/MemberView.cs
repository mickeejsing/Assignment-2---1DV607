using System;
using Model;
namespace View
{
    public class MemberView : BaseView
    {
        public void DisplayLogin()
        {
            Console.WriteLine("Login screen");
            Console.WriteLine("Continue as guest [1]");
            Console.WriteLine("Login [2]");
        }
        public bool Login()
        {
            Console.WriteLine("=====LOGIN=====");
            Console.WriteLine("Password is 123456");
            Console.Write("Input password: ");
            string password = Console.ReadLine();

            if(password == "123456")
                return true;
            return false;

        }
        public void DisplayMainOptions()
        {
            Console.WriteLine("Manage boats [mb]");
            Console.WriteLine("Manage members [mm]");
        }

        public void DisplayEditMemberOptions()
        {
            Console.WriteLine("Edit Member First name [ef] Edit Member Last name [el] Back [selm]");
        }
        public void DisplayGuestOptions()
        {
            Console.WriteLine("Display all users [sm], Display all boats [sab], Go to login [2], Search [X]");
        }

        public void DisplaySelectedMemberOptions()
        {
            Console.WriteLine("Delete member [dm] Edit member [em] Manage boats [mmb] Show member details [md] Back to Secretary Options [s]");
        }

        public void DisplayMemberOptions()
        {
            Console.WriteLine("Create member [cm] Show members [sm] Select member [selm] Back to main Options [2]");
        }
        public void DisplayMemberUpdated(Member member)
        {
            Console.WriteLine($"Member with Id: {member.Id} was updated");
        }
        public void DisplayMemberNotFound()
        {
            Console.WriteLine("Sorry, the member was not found");
        }

        public void DisplayMemberDeleted()
        {
            Console.WriteLine("The Member was deleted");

        }
        public string GetSocialSecurityNumber()
        {
            bool isValid = false;
            long socialSecurityNumber = 0;

            while (!isValid)
            {
                try
                {
                    Console.Write("Type In the Social Security number of the Member");
                    Console.WriteLine("Accepted format [yyddmmnnnn] :");
                    socialSecurityNumber = Convert.ToInt64(Console.ReadLine());
                    string socialSecurityNumberStr = socialSecurityNumber.ToString();

                    if (socialSecurityNumberStr.Length == 10)
                    {
                        Console.WriteLine(isValid);
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("The social security number has to be 10 numbers yyddmmnnnn: ");
                    }
                }
                catch
                {
                    Console.WriteLine("Please only numbers");
                }
            }
            return socialSecurityNumber.ToString();
        }

        public void DisplayErrorInvalidSerialNumber()
        {
            Console.WriteLine("The social number given was not valid. Did you make a mistake?");
        }
        public string GetFirstName()
        {
            bool containsToSymbols = false;
            string firstName = "";
            while (!containsToSymbols)
            {
                Console.Write("Type The members first name: ");
                firstName = Console.ReadLine();
                containsToSymbols = ValidateOnlyAlphabetical(firstName);
            }
            return firstName;
        }

        public string GetLastName()
        {
            bool containsToSymbols = false;
            string lastName = "";
            while (!containsToSymbols)
            {
                Console.Write("Type The members Last name: ");
                lastName = Console.ReadLine();
                containsToSymbols = ValidateOnlyAlphabetical(lastName);
            }
            return lastName;
        }
        private bool ValidateOnlyAlphabetical(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsLetter(str[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public void DisplayErrorNoMembers()
        {
            Console.WriteLine("Sorry no memberse were found...");
        }

        public int GetMemberId()
        {
            int id = 0;
            bool isString = true;
            while (isString)
            {
                Console.Write("Type the member id you want to select or press [ 0 ] to go back: ");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    isString = false;
                }
                catch
                {
                    Console.WriteLine("Please only numbers");
                }
            }
            return id;
        }



        public void DisplayGetMemberDisplayFormat()
        {
            Console.WriteLine("Show Members Verbose [smv] Show Members Compact [smc]");
        }

        public void DisplayMembersVerbose(Member member)
        {
            Console.WriteLine("===============MEMBER===============");
            Console.WriteLine($"Name: {member.FirstName} {member.LastName}");
            Console.WriteLine($"Date of Birth: {member.SocialSecurityNum} ");
            Console.WriteLine($"Member Id: {member.Id} ");

            Console.WriteLine("===============BOATS================");
            if (member.Boats.Count > 0)
            {
                foreach (Boat boat in member.Boats)
                {
                    Console.WriteLine($"Boat type: {boat.BoatType.ToString()}, Boat Length: {boat.Length}, Boat Id: {boat.Id}");
                }
            }
            else
            {
                Console.WriteLine("No boats found");
            }
        }
        public void DisplayMembersCompact(Member member)
        {
            Console.WriteLine($"Name: {member.FirstName} {member.LastName}, Id: {member.Id}, Number of boats: {member.Boats.Count}");
        }
    }
}