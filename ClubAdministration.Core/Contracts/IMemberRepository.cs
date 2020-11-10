using ClubAdministration.Core.Entities;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
    public interface IMemberRepository
    {
        bool IstMitgliedVorhanden(string lastName, string firstName, int id);
        Task<Member[]> GetMembersCompletAsync();
    }
}
