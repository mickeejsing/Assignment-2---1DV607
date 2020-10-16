using System;

namespace View
{
    public class BoatView : BaseView
    {
        public void DisplaySelectedBoatOptions()
        {
            Console.WriteLine("Delete boat [db] Change type [cbt] Change length [cbl] Manage boats [mb] Back to Secretary Options [s]");
        }
        public void DisplayMemberSelectedBoatOptions()
        {
            Console.WriteLine("Change length [cbl] Change Type [cbt] Back to Secretary Options [s]");
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