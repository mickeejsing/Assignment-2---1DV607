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
            if (members.Count== 0)
            {
                _memberView.DisplayErrorNoMembers();
            }
            else 
            {
            members.ForEach(member => _memberView.DisplayMembersCompact(member));
            Member selectedMember = selectMember();

            return selectedMember;
            }
            return null;
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
            _memberView.DisplayMemberOptions();
            return _memberView.GetViewOperation();
        }

        public void HandleRemoveMember(Member member)
        {
            _repository.RemoveMember(member);

        }

        public void HandleDisplayMembers()
        {
            List<Member> members = _repository.GetAllMembers();
            if (members.Count == 0)
            {
                _memberView.DisplayErrorNoMembers();
            }
            else
            {
                _memberView.DisplayGetMemberDisplayFormat();
                input = _memberView.GetViewOperation();
                {
                    switch (input)
                    {
                        case ViewOperations.ShowMembersVerbose:
                            members.ForEach(m => _memberView.DisplayMembersVerbose(m)); break;
                        case ViewOperations.ShowMembersCompact:
                            members.ForEach(m => _memberView.DisplayMembersCompact(m)); break;
                        default: HandleDisplayMembers(); break;
                    }
                }
            }
        }

        public void HandleEditMember(Member member)
        {
            _memberView.DisplayEditMemberOptions();
            input = _memberView.GetViewOperation();
            bool stayInMenu = true;

            switch (input)
            {
                case ViewOperations.EditFirstName:
                    EditFirstName(member); break;
                case ViewOperations.EditLastName:
                    EditLastName(member); break;
                case ViewOperations.SelectMember:
                    stayInMenu = false; break;
            }
            if (stayInMenu)
                HandleEditMember(member);
        }

        public void EditFirstName(Member member)
        {
            string firstName = _memberView.GetFirstName();
            _repository.ChangeFirstName(member, firstName);
        }

        public void EditLastName(Member member)
        {
            string lastName = _memberView.GetLastName();
            _repository.ChangeLastName(member, lastName);
        }

        public void ShowAllBoats()
        {
            List<Boat> boats = _repository.GetAllBoats();
            _memberView.DisplayAllBoats(boats);
        }

        public void AddMember(Member member)
        {
            _repository.AddMember(member);
        }

        public bool MemberIdTaken(Member member)
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

        public Member CreateMember()
        {
            string firstName = _memberView.GetFirstName();
            string lastName = _memberView.GetLastName();
            bool isSocialNumValid = false;
            Member member = new Member();
            List<Member> members = _repository.GetAllMembers();
            string socialSecurityNumber;
            while (!isSocialNumValid && !MemberIdTaken(member))
            {
                try
                {
                    socialSecurityNumber = _memberView.GetSocialSecurityNumber();
                    member.FirstName = firstName;
                    member.LastName = lastName;
                    member.SocialSecurityNum = new Member.SocialSecurityNumber(socialSecurityNumber);

                    if (member.SocialSecurityNum != null)
                    {
                        isSocialNumValid = true;
                    }
                    Console.WriteLine(isSocialNumValid);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Invalid Serial Number")
                    {
                        _memberView.DisplayErrorInvalidSerialNumber();
                    }
                }
            }
            return member;
        }

        public void DisplayMembersVerbose(Member member)
        {
            _memberView.DisplayMembersVerbose(member);
        }

        public Member SelectMember()
        {
            Member member;
            do
            {
                int id = _memberView.GetMemberId();
                if (id == 0)
                {
                    return null;
                }
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
