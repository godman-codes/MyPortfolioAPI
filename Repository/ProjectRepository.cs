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
    public class ProjectRepository : RepositoryBase<ProjectsModel, Guid>, IProjectRepository
    {
        public ProjectRepository(MyProjectDbContext context) : base(context) 
        {

        }

        public void CreateProject(ProjectsModel project)
        {
            Create(project);
        }

        public async Task<IEnumerable<ProjectsModel>> GetAllProjects(string userId, bool trackChanges)
        {
            return await FindByCondition(x => x.OwnerId == userId, trackChanges)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ProjectsModel> GetProject(Guid id, string userId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == id, trackChanges)
                .Where(x => x.OwnerId == userId)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> GetProjectByGithubLink(string link, string userId, bool trackChanges)
        {
            var project = await FindByCondition(x => x.OwnerId == userId, trackChanges)
                .Where(x => x.GitHubLink.ToLower() == link.ToLower())
                .SingleOrDefaultAsync();

            return project == null;
        }

        public async Task<bool> GetProjectByName(string name, string userId, bool trackChanges)
        {
            var project = await FindByCondition(x => x.OwnerId == userId, trackChanges)
               .Where(x => x.Name.ToLower() == name.ToLower())
               .SingleOrDefaultAsync();

            return project == null;
        }
    }
}
