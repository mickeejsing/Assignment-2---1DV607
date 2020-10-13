using System;
using Persistence;
using View;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class MemberController
    {
        private readonly MemberView _memberView;
        private readonly Repository _repository;
        private Enum input;
        public MemberController(MemberView memberView, Repository repository)
        {
            _memberView = memberView;
            _repository = repository;
        }

        

        public Member HandleSelectMember()
        {
            List<Member> members = _repository.GetAllMembers();

            members.ForEach(m => _memberView.DisplayMembersCompact(m));
            Member selectedMember = selectMember();

            return selectedMember;
        }

        public Enum DisplayLogin()
        {
            _memberView.DisplayLogin();
            return _memberView.GetViewOperation();
        }

        public Enum DisplayMainOptions()
        {
            _memberView.DisplayMainOptions();
            return _memberView.GetViewOperation();
        }
        public Enum DisplaySelectedMemberOptions()
        {
            _memberView.DisplaySelectedMemberOptions();
            return _memberView.GetViewOperation();
        }

        public Enum DisplayEditMemberOptions()
        {
            _memberView.DisplayEditMemberOptions();
            return _memberView.GetViewOperation();
        }
        public Enum DisplayMemberOptions()
        {
            DisplayMemberOptions();
            return _memberView.GetViewOperation();
        }

        public void HandleRemoveMember(Member member)
        {
            _repository.RemoveMember(member);
        }

        public void HandleDisplayMembers()
        {
            _memberView.DisplayGetMemberDisplayFormat();
            input = _memberView.GetViewOperation();
            List<Member> members = _repository.GetAllMembers();

            switch (input)
            {
                case ViewOperations.ShowMembersVerbose:
                    members.ForEach(m => _memberView.DisplayMembersVerbose(m)); break;
                case ViewOperations.ShowMembersCompact:
                    members.ForEach(m => _memberView.DisplayMembersCompact(m)); break;
                default: HandleDisplayMembers(); break;
            }
        }

        public void HandleEditMember(Member member)
        {
            _memberView.DisplayEditMemberOptions();
            input = _memberView.GetViewOperation();

            switch (input)
            {
                case ViewOperations.EditFirstName:
                    EditFirstName(member);
                    HandleEditMember(member); break;
                case ViewOperations.EditLastName:
                    EditLastName(member);
                    HandleEditMember(member); break;
                default: HandleEditMember(member); break;
            }
        }

        public void EditFirstName(Member member)
        {
            string firstName = _memberView.GetFirstName();
            _repository.ChangeFirstName(member, firstName);
            input = ViewOperations.EditMember;
        }

        public void EditLastName(Member member)
        {
            string lastName = _memberView.GetLastName();
            _repository.ChangeLastName(member, lastName);
            input = ViewOperations.EditMember;
        }

        public void ShowAllBoats()
        {
            List<Boat> boats = _repository.GetAllBoats();
            _memberView.DisplayAllBoats(boats);
        }

        public void HandleAddMember(Member member)
        {
                member = HandleCreateMember();
        }

        public bool memberIdTaken(Member member)
        {
            List<Member> members = _repository.GetAllMembers();
                            foreach (Member memberInCollection in members)
                {
                    if (member.Id == memberInCollection.Id)
                    {
                        return true;
                    }
                }
                return false;
        }

        public Member HandleCreateMember()
        {
            string firstName = _memberView.GetFirstName();
            string lastName = _memberView.GetLastName();
            bool isSocialNumValid = false;
            Member member = new Member();
            List<Member> members = _repository.GetAllMembers();
            string socialSecurityNumber;
            do
            {
                try
                {
                    socialSecurityNumber = _memberView.GetSocialSecurityNumber();
                    member.FirstName = firstName;
                    member.LastName = lastName;
                    member.SocialSecurityNum = new Member.SocialSecurityNumber(socialSecurityNumber);
                    isSocialNumValid = true;

                }
                catch (Exception ex)
                {
                    isSocialNumValid = false;
                    if (ex.Message == "Invalid Serial Number")
                    {
                        _memberView.DisplayErrorInvalidSerialNumber();
                    }
                }

                // FINNS ID I DATABAS?
                if(!memberIdTaken(member))
                {
                    return member;
                }
            }
            while (!isSocialNumValid);
            return null;
        }

        public void DisplayMembersVerbose(Member member)
        {
            _memberView.DisplayMembersVerbose(member);
        }


        public Member selectMember()
        {
            Member member;
            do
            {
                int id = _memberView.GetMemberId();
                member = _repository.SelectMemberById(id);
                if (member == null)
                {
                    _memberView.DisplayMemberNotFound();
                }
            } while (member == null);
            return member;
        }
    }
}
