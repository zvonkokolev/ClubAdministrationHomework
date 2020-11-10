using ClubAdministration.Core.Contracts;
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

namespace ClubAdministration.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Section[] _allSections;

        public ObservableCollection<Section> Sections { get; set; }
        public ObservableCollection<Member> Members { get; set; }
        public ObservableCollection<MemberSection> MemberSections { get; set; }

        public Member SelectedMember { get; set; }
        public Section SelectedSection { get; set; }

        public MainViewModel(IWindowController windowController) : base(windowController)
        {
            LoadCommands();
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

            _allSections = sects;
            Sections = new ObservableCollection<Section>(sects);
            Members = new ObservableCollection<Member>(membs);
            MemberSections = new ObservableCollection<MemberSection>(membSects);

            SelectedSection = _allSections.OrderByDescending(s => s.Name).FirstOrDefault();
            var membersForSection = sects.Where(s => s.Id == SelectedSection.Id);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        public static async Task<MainViewModel> CreateAsync(IWindowController windowController)
        {
            var viewModel = new MainViewModel(windowController);
            await viewModel.LoadDataAsync();
            return viewModel;
        }
    }
}
