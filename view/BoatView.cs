using System;

namespace View
{
    public class BoatView : BaseView
    {
        public void displaySecretarySingleBoatOptions()
        {
            Console.WriteLine("Delete boat [db] Change type [cbt] Change length [cbl] Manage boats [mb] Back to Secretary Options [s]");
        }

        public void displaySecretaryBoatOptions()
        {
            Console.WriteLine("Show all boats [sab] Show boat from id [sbi] Edit boat [eb]");
        }
    }
}