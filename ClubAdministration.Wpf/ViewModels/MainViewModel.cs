﻿using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using ClubAdministration.Persistence;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClubAdministration.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MemberSection[] _allmembSect;
        private Member _selectedMember;
        private Section _selectedSection;
        private ICommand _cmdEditMember;
        private MemberSection _selectedMemberSection;
        //private MemberDto _selectedMemberDTO;

        public ObservableCollection<Section> Sections { get; set; }
        public ObservableCollection<Member> Members { get; set; }
        //public ObservableCollection<MemberDto> MembersDTO { get; private set; }
        public ObservableCollection<Member> MemberSections { get; set; }

        public Member SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;
                OnPropertyChanged(nameof(SelectedMember));
            }
        }

        public Section SelectedSection
        {
            get { return _selectedSection; }
            set
            {
                _selectedSection = value;
                OnPropertyChanged(nameof(SelectedSection));
                _ = OnChangeSelectedSection_LoadNewMembersdataAsync(SelectedSection.Id);
            }
        }

        private async Task OnChangeSelectedSection_LoadNewMembersdataAsync(int newSelectedSectionIndex)
        {
            using IUnitOfWork unitOfWork = new UnitOfWork();
            Member[] membersForSection = await unitOfWork.MemberSectionRepository.GetAllMembersCompletBySelectedSectionIdAsync(newSelectedSectionIndex);
            MemberSections = new ObservableCollection<Member>(membersForSection);
            SelectedMember = membersForSection.FirstOrDefault();
        }

        public MemberSection SelectedMemberSection
        {
            get => _selectedMemberSection;
            set
            {
                _selectedMemberSection = value;
                OnPropertyChanged(nameof(SelectedMember));
            }
        }

        public MainViewModel(IWindowController windowController) : base(windowController)
        {
            LoadCommands();
            Validate();
        }

        private void LoadCommands()
        {
        }

        private async Task LoadDataAsync()
        {
            using IUnitOfWork unitOfWork = new UnitOfWork();
            Section[] sects = await unitOfWork.SectionRepository.GetAllSectionsAsync();
            Member[] membs = await unitOfWork.MemberRepository.GetMembersCompletAsync();
            MemberSection[] membSects = await unitOfWork.MemberSectionRepository.GetMembSectCompletAsync();

            _allmembSect = membSects;

            Sections = new ObservableCollection<Section>(sects);
            SelectedSection = Sections.FirstOrDefault();

            Member[] membersForSection = await unitOfWork.MemberSectionRepository.GetAllMembersCompletBySelectedSectionIdAsync(SelectedSection.Id);

            MemberSections = new ObservableCollection<Member>(membersForSection);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Sections == null)
            {
                yield return new ValidationResult($"Datenbak ist fehlerhaft", new string[] { nameof(Sections) });
            }
        }

        public static async Task<MainViewModel> CreateAsync(IWindowController windowController)
        {
            var viewModel = new MainViewModel(windowController);
            await viewModel.LoadDataAsync();
            return viewModel;
        }
        // commands
        public ICommand CmdEditMember
        {
            get
            {
                if (_cmdEditMember == null)
                {
                    _cmdEditMember = new RelayCommand(execute: _ =>
                        {
                            var window = new MembersViewModel(Controller, SelectedMember);
                            window.Controller.ShowWindow(window, true);
                        },
                        canExecute: _ => SelectedMember != null);

                }
                return _cmdEditMember;
            }
            set
            {
                _cmdEditMember = value;
            }
        }
    }
}
