using ClubAdministration.Core.Contracts;
using System.Linq;

namespace ClubAdministration.Persistence
{
  public class MemberRepository : IMemberRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public MemberRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

        public bool IstMitgliedVorhanden(string lastName, string firstName, int id)
            => _dbContext.Members.Select(m => m.LastName.Equals(lastName) && m.FirstName.Equals(firstName) && m.Id == id).FirstOrDefault();
    }
}