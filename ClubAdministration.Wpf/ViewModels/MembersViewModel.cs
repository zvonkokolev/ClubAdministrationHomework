using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using ClubAdministration.Persistence;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;

namespace ClubAdministration.Wpf.ViewModels
{
    public class MembersViewModel : BaseViewModel
    {
        private ICommand _cmdSaveMember;
        private Member _member;
        private string _lastName;
        private string _firstName;

        public MembersViewModel(IWindowController windowController, Member member) : base(windowController)
        {
            Member = member;
            LastName = member.LastName;
            FirstName = member.FirstName;
            LoadCommands();
        }

        private void LoadCommands()
        {
        }


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        // commands
        public ICommand CmdSaveMember 
        {
            get
            {
                if(_cmdSaveMember == null)
                {
                    _cmdSaveMember = new RelayCommand(
                        execute: _ =>
                        {
                            IUnitOfWork uow = new UnitOfWork();
                            try
                            {
                                uow.SaveChangesAsync();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    , canExecute: _ => Member != null
                    );
                }
                return _cmdSaveMember;
            }
            set
            {
                _cmdSaveMember = value;
            }
        }

        public Member Member
        {
            get => _member;
            private set
            {
                _member = value;
                OnPropertyChanged(nameof(Member));
                Validate();
            }
        }
        [Required]
        [MinLength(2, ErrorMessage = "Lastname must be at least 2 character long")]
        public string LastName 
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                //Validate();
            } 
        }
        [Required]
        [MinLength(2, ErrorMessage = "Firstname must be at least 2 character long")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                //Validate();
            } 
        }
    }
}
