using System;
using enums;

namespace view
{   public abstract class View
    {
        public Enum getViewOperation() {

            int input = getInput();
            Console.WriteLine(input);

            switch (input) {
                case (1):
                    return ViewOperations.isSecretary;
                case (2):
                    return ViewOperations.isMember;
                case (3):
                    return ViewOperations.SecretaryMemberOptions;
                default:
                    break;
            }
            return ViewOperations.quit;
        }
        public int getInput() {

            int input = Convert.ToInt32(Console.ReadLine());
            return input;
        }

        public bool isInputOptionValid(int input, int from, int to) {    
            if(input >= from && input <= to) {
                return true;
            }
            return false;
        }
    }
}