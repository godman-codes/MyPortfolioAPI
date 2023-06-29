using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWorkExperienceRepository
    {
        Task<IEnumerable<WorkExperienceModel>> GetAllWorkExperienceiesAysnc(bool trackChanges, string userId);
        Task<WorkExperienceModel> GetWorkExperienceAsync(Guid Id, bool trackChanges, string userId);
        void CreateWorkExperience(WorkExperienceModel workExperience);
        void DeleteWorkExperienc(WorkExperienceModel workExperience, string userId);
    }
}
