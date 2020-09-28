using System;
using System.Collections.Generic;
using enums;

namespace assignment2 {
    public class Controller {

        private readonly RegisterView _registerView;
        private readonly MemberView _memberView;
        private readonly BoatView _boatView;
        private readonly Register _register;

        public Controller(RegisterView registerView, MemberView memberView, BoatView boatView, Register register) {
            _registerView = registerView;
            _memberView = memberView;
            _boatView = boatView;
            _register = register;
        }

        public void mainMenu () {

            Register register = new Register();
            register.saveMemberToFile(new Member("Ivan","Lindow",32312311));

            List<Member> members = register.getMembers();

            foreach(Member member in members) {
                Console.WriteLine(member.FirstName);
            }
            

            /*
            _registerView.displayLogin();
            switch(_registerView.getViewOperation()) {
                case ViewOperations.isSecretary:
                    // Sekreterare
                    _registerView.displaySecretaryOptions();
                    Enum e = _registerView.getViewOperation();

                                        
                    if(e.Equals(ViewOperations.SecretaryMemberOptions) )
                    {
                        _registerView.displaySecretaryMemberOptions();       

                        if(_registerView.getViewOperation().Equals(ViewOperations.createMember))       
                        {
                            Member member = _registerView.displayMemberForm();

                            // Add member to database or file


                            // Go back
                        }          
                    }
                




                    break;
                case ViewOperations.isMember:
                    _registerView.displayLogin();

                    // Medlemmar


                    break;
                    
            }
            */
        }
        public void registerMember() {

        }

    }
}
