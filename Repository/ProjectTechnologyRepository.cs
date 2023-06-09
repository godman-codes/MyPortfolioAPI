using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class ProjectTechnologyRepository : RepositoryBase<ProjectTechnologiesModel>, IProjectTechnologiesRepository
    {
        public ProjectTechnologyRepository(MyProjectDbContext context) : base(context)
        {

        }
    }
}
