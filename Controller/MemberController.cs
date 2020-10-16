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
        public MemberController(MemberView memberView, Repository repository)
        {
            _memberView = memberView;
            _repository = repository;
        }
        public Member HandleSelectMember()
        {
            List<Member> members = _repository.GetAllMembers();
            if (members.Count == 0)
            {
                _memberView.DisplayErrorNoMembers();
            }
            else
            {
                members.ForEach(member => _memberView.DisplayMembersCompact(member));
                Member selectedMember = SelectMember();

                return selectedMember;
            }
            return null;
        }
        public bool Login()
        {
            return _memberView.Login();
        }

        public Enum DisplayLogin()
        {
            _memberView.DisplayLogin();
            return _memberView.GetViewOperation();
        }
        public Enum DisplayGuestOptions()
        {
            _memberView.DisplayGuestOptions();
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

        // Methods
        public void RemoveMember(Member member)
        {
            _repository.RemoveMember(member);

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

        public void AddMember(Member member)
        {
            _repository.AddMember(member);
        }

        public bool RepositoryIsEmpty()
        {
            return _repository.GetAllMembers().Count == 0;
        }

        public void DisplayMembersVerbose()
        {
            List<Member> members = _repository.GetAllMembers();
            members.ForEach(member => _memberView.DisplayMembersVerbose(member));
        }

        public void DisplayMembersCompact()
        {
            List<Member> members = _repository.GetAllMembers();
            members.ForEach(member => _memberView.DisplayMembersCompact(member));
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
        public void DisplayNoMembers()
        {
            _memberView.DisplayErrorNoMembers();
        }

        public Enum DisplayGetMemberDisplayFormat()
        {
            _memberView.DisplayGetMemberDisplayFormat();
            return _memberView.GetViewOperation();
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
