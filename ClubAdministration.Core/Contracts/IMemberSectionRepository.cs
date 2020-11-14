using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
    public interface IMemberSectionRepository
    {
        Task AddRangeAsync(IEnumerable<MemberSection> memberSections);
        Task<MemberSection[]> GetMembSectCompletAsync();
        Task<MemberDto[]> GetAllMembersDtoCompletAsync();
        Task<MemberDto[]> GetAllMembersDtoCompletBySelectedSectionIdAsync(int id);
        Task<Member[]> GetAllMembersCompletBySelectedSectionIdAsync(int sectionId);
    }
}
