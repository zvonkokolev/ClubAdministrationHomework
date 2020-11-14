using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
    public interface IMemberRepository
    {
        bool IstMitgliedVorhanden(string lastName, string firstName, int id);
        Task<Member[]> GetMembersCompletAsync();
        Task<MemberDto[]> GetMembersDTOCompletAsync();
        Task<MemberDto[]> GetMembersDTOCompletBySectionIdAsync(int id);
        Task<Member> GetMemberByIdAsync(int id);
    }
}
