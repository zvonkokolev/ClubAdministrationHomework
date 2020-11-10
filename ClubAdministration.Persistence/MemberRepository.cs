using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Member[]> GetMembersCompletAsync() => _dbContext.Members
            .Include(s => s.MemberSections)
            .OrderByDescending(s => s.LastName)
            .ThenByDescending(s => s.FirstName)
            .ToArrayAsync()
            ;

        public bool IstMitgliedVorhanden(string lastName, string firstName, int id)
            => _dbContext.Members.Select(m => m.LastName.Equals(lastName)
            && m.FirstName.Equals(firstName) && m.Id == id).FirstOrDefault();

    }
}