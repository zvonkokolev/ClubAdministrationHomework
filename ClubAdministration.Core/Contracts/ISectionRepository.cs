using ClubAdministration.Core.Entities;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
    public interface ISectionRepository
    {
        Task<Section[]> GetAllSectionsAsync();
    }
}
