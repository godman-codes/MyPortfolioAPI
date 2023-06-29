using Entities.Models;
using Shared.DTOs.Request;
using Shared.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IWorkExperienceService
    {
        Task<IEnumerable<WorkExperienceResponseDto>> GetAllWorkExperience(bool trackChanges, string UserId);
        Task<WorkExperienceResponseDto> GetWorkExperience(Guid id, bool trackchanges, string UserId);
        Task<WorkExperienceResponseDto> CreateWorkExperience(WorkExperienceRequestDto workExperience, string userId);
        Task DeletWorkExperience(Guid id, bool trackChanges);
        Task UpdateWorkxperience(Guid id, WorkExperienceToUpdateDto workExperienceToUpdate, bool trackChanges);
    }
}
