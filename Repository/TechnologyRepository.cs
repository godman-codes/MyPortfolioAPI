using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TechnologyRepository : RepositoryBase<TechnologiesModel, Guid>, ITechnologiesRepository
    {
        public TechnologyRepository(MyProjectDbContext context) : base(context)
        {

        }

        public void CreateTechnology(TechnologiesModel technology)
           => Create(technology);

        public void DeleteTechnology(TechnologiesModel technologies)
            => Delete(technologies);

        public async Task<IEnumerable<TechnologiesModel>> GetAllTechnologies(string userId, bool trackChanges)
            => await FindByCondition(x => x.OwnerId == userId, trackChanges)
                .OrderBy(x => x.Percentage)
                .ToListAsync();


        public async Task<TechnologiesModel> GetTechnology(Guid id, bool trackChanges, string userId)
            => await FindByCondition(x => x.Id == id, trackChanges)
            .Where(x => x.OwnerId == userId)
            .SingleOrDefaultAsync();

        public async Task<TechnologiesModel> GetTechnologyByName(string name, bool trackChanges, string userId)
            => await FindByCondition(x => x.Name == name, trackChanges)
            .Where(x => x.OwnerId == userId)
            .SingleOrDefaultAsync();
        
    }
}
