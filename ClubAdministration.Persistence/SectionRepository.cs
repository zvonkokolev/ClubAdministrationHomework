using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SectionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Section[]> GetAllSectionsAsync() 
            => await _dbContext.Sections.ToArrayAsync();
    }
}