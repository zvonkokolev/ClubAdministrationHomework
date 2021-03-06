﻿using ClubAdministration.Core.Contracts;
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
            if(LastName.Length < 2)
            {
                yield return new ValidationResult("Lastname must be at least 2 character long", new string[] { nameof(LastName)});
            }
            if (FirstName.Length < 2)
            {
                yield return new ValidationResult("Firstname must be at least 2 character long", new string[] { nameof(FirstName) });
            }
        }

        // commands
        public ICommand CmdSaveMember 
        {
            get
            {
                if (_cmdSaveMember == null)
                {
                    _cmdSaveMember = new RelayCommand(
                        execute: async _ =>
                        {
                            using IUnitOfWork uow = new UnitOfWork();
                            Member memberFromDB = await uow.MemberRepository.GetMemberByIdAsync(_member.Id);
                            memberFromDB.LastName = LastName;
                            memberFromDB.FirstName = FirstName;
                            try
                            {
                                await uow.SaveChangesAsync();
                                Controller.CloseWindow(this);
                            }
                            catch (ValidationException e)
                            {
                                if(e.Value is IEnumerable<string> prop)
                                {
                                    foreach (var item in prop)
                                    {
                                        AddErrorsToProperty(item, new List<string> { e.ValidationResult.ErrorMessage });
                                    }
                                }
                                else
                                {
                                    DbError = e.ValidationResult.ErrorMessage;
                                }
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
                Validate();
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
                Validate();
            } 
        }
    }
}
