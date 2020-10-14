using System;
using Model;
namespace View
{
    public class MemberView : BaseView
    {
        public void DisplayLogin()
        {
            Console.WriteLine("Login screen");
            Console.WriteLine("Member [1]");
            Console.WriteLine("Secretary [2]");
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

        public void DisplaySelectedMemberOptions()
        {
            Console.WriteLine("Delete member [dm] Edit member [em] Manage boats [mmb] Show member details [md] Back to Secretary Options [s]");
        }



        public void DisplayMemberOptions()
        {
            Console.WriteLine("Create member [cm] Show members [sm] Select member [selm] Back to main Options [2]");
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
                catch (Exception ex)
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
            Console.Write("Type The members first name: ");
            string firstName = Console.ReadLine();
            return firstName;
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
                catch (Exception ex)
                {
                    Console.WriteLine("Please only numbers");
                }
            }
            return id;
        }

        public string GetLastName()
        {
            Console.Write("Type The members last name: ");
            string lastName = Console.ReadLine();
            return lastName;
        }

        public void DisplayGetMemberDisplayFormat()
        {
            Console.WriteLine("Show Members Verbose [smv] Show Members Compact [smc]");
        }

        public void DisplayBoatOptions()
        {
            Console.WriteLine("Register boat [1] Delete boat [2] Change boat information [3]");
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
                DisplayMemberBoatInfo(member);
            }
            else
            {
                Console.WriteLine("No boats found");
            }
        }



        public void DisplayMembersCompact(Member member)
        {
            Console.WriteLine($"{member.FirstName} {member.LastName} Id: {member.Id} Boats: {member.Boats.Count}");
        }
    }
}