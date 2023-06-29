using Contracts;
using Entities.Models;
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
    }
}
