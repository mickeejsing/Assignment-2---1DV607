using System;
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
        private string GetInput()
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
    }
}