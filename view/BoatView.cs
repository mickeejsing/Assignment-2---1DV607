using System;

namespace View
{
    public class BoatView : BaseView
    {
        public void DisplaySelectedBoatOptions()
        {
            Console.WriteLine("Delete boat [db] Change type [cbt] Change length [cbl] Manage boats [mb] Back to Secretary Options [s]");
        }

        public void DisplayBoatOptions()
        {
            Console.WriteLine("Show all boats [sab] Show boat from id [sbi] Edit boat [eb]");
        }

        public void DisplayManageMemberBoats()
        {
            Console.WriteLine("Add boat [amb] Delete boat [dmb] Edit [emb]");
        }
    }
}