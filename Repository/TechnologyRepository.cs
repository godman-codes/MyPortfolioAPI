using Contracts;
using Entities.Models;
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
    }
}
