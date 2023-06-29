using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;


namespace Repository
{
    public class WorkExperienseRepository : RepositoryBase<WorkExperienceModel, Guid>, IWorkExperienceRepository
    {
        
        public WorkExperienseRepository(MyProjectDbContext context) : base(context)
        {
        }

        public void CreateWorkExperience(WorkExperienceModel workExperience)
        {
            Create(workExperience);
        }

        public void DeleteWorkExperienc(WorkExperienceModel workExperience, string userId)
        {
            Delete(workExperience);
        }

        public async Task<IEnumerable<WorkExperienceModel>> GetAllWorkExperienceiesAysnc(bool trackChanges, string userId)
        {
            return await FindByCondition(r => r.OwnerId == userId, trackChanges)
                .OrderBy(w => w.From)
                .ToListAsync();

        }

        public async Task<WorkExperienceModel> GetWorkExperienceAsync(Guid Id, bool trackChanges, string userId) => await FindByCondition(r => r.Id == Id, trackChanges)
                .Where(r => r.OwnerId == userId)
                .SingleOrDefaultAsync();
    }
}
