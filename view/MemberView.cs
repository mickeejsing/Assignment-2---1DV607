using System;
using Model;
namespace View
{
    public class MemberView : BaseView
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

        public void displayEditMemberOptions()
        {
            Console.WriteLine("Edit Member First name [ef] Edit Member Last name [el] Back to secretary options [s] ");
        }

        public void displaySelectedMemberOptions()
        {
            Console.WriteLine("Delete member [dm] Edit member [em] Manage boats [mmb] Show member details [md] Back to Secretary Options [s]");
        }

        public void displaySecretaryManagaeMemberBoats()
        {
            Console.WriteLine("Add boat [amb] Delete boat [dmb] Edit [emb]");
        }

        public void displaySecretaryMemberOptions()
        {
            Console.WriteLine("Create member [cm] Show members [sm] Select member [selm]");
        }
        public int getSocialSecurityNumber()
        {
            bool isValid = false;
            int socialSecurityNumber = 0;

            while (!isValid)
            {
                try
                {
                    Console.Write("Type The members social security number: ");
                    socialSecurityNumber = Convert.ToInt32(Console.ReadLine());
                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please only write numbers..." + " " +ex);
                }
            }
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
            displayMemberBoatInfo(member);
        }

        public void displayMemberBoatInfo(Member member)
        {
            foreach (Boat boat in member.boats)
            {
                Console.WriteLine($"{boat.Type} {boat.Length} {boat.Id}");
            }
            Console.WriteLine("====================================");
        }

        public void displayMembersCompact(Member member)
        {
            Console.WriteLine($"{member.FirstName} {member.LastName} {member.Id} Boats: {member.boats.Count}");
        }
    }
}