using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
  public class MemberSectionRepository : IMemberSectionRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public MemberSectionRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<MemberSection> memberSections) 
      => await _dbContext.AddRangeAsync(memberSections);

        public async Task<MemberDto[]> GetAllMembersDtoCompletAsync()
            => await _dbContext.MemberSections
            .Select(md => new MemberDto 
            { 
               Id = md.MemberId,
               LastName = md.Member.LastName,
               FirstName = md.Member.FirstName,
               CountSections = md.Member.MemberSections.Count
            })
            .ToArrayAsync();

        public async Task<MemberDto[]> GetAllMembersDtoCompletBySelectedSectionIdAsync(int id)
            => await _dbContext.MemberSections
            .Where (sec => sec.SectionId == id)
            .Select(md => new MemberDto
            {
                Id = md.MemberId,
                LastName = md.Member.LastName,
                FirstName = md.Member.FirstName,
                CountSections = md.Member.MemberSections.Count
            })
            .ToArrayAsync();

        public async Task<MemberSection[]> GetMembSectCompletAsync()
            => await _dbContext.MemberSections
            .Include(s => s.Section)
            .Include(m => m.Member)
            .ToArrayAsync();
    }
}