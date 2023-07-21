using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProjectRepository 
    {
        void CreateProject(ProjectsModel project);
        Task<IEnumerable<ProjectsModel>> GetAllProjects(string userId, bool trackChanges);
        Task<ProjectsModel> GetProject(Guid id, string userId, bool trackChanges);
        Task<bool> GetProjectByName(string name, string userId, bool trackChanges);
        Task<bool> GetProjectByGithubLink(string link, string userId, bool trackChanges);

    }
}
