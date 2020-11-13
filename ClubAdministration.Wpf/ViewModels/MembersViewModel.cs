using ClubAdministration.Core.Entities;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubAdministration.Wpf.ViewModels
{
    public class MembersViewModel : BaseViewModel
    {
        public MembersViewModel(IWindowController windowController, Member member) : base(windowController)
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
        }


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
