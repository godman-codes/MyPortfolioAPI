using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IProjectRepository Projects { get; }
        ITechnologiesRepository Technologies { get; }
        IWorkExperienceRepository WorkExperienceRepository { get; }
        IProjectTechnologiesRepository ProjectTechnologiesRepository { get; }
        void save();
    }
}
