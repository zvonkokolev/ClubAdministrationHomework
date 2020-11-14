using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Member> GetMemberByIdAsync(int id) =>
            await _dbContext.Members.SingleAsync(m => m.Id == id);

        public Task<Member[]> GetMembersCompletAsync() => _dbContext.Members
            .Include(s => s.MemberSections)
            .OrderByDescending(s => s.LastName)
            .ToArrayAsync()
            ;

        public async Task<MemberDto[]> GetMembersDTOCompletAsync() => await _dbContext.Members
             .Include(s => s.MemberSections)
            .OrderByDescending(s => s.LastName)
            .Select(s => new MemberDto
            {
                Id = s.Id,
                LastName = s.LastName,
                FirstName = s.FirstName,
                CountSections = s.MemberSections.Count()
            })
            .ToArrayAsync()
            ;

        public Task<MemberDto[]> GetMembersDTOCompletBySectionIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public bool IstMitgliedVorhanden(string lastName, string firstName, int id)
            => _dbContext.Members.Select(m => m.LastName.Equals(lastName)
            && m.FirstName.Equals(firstName) && m.Id == id).FirstOrDefault();

    }
}