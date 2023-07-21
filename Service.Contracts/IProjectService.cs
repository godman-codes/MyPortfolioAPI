using Shared.DTOs.Request;
using Shared.DTOs.Response;
using System.Collections;

namespace Service.Contracts
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponseDto>> GetAllProjects(string userId, bool trackChanges);
        Task<ProjectResponseDto> GetProject(Guid id, string userId, bool trackChanges);
        Task<ProjectResponseDto> CreateProject(ProjectRequestDto projectRequest, string userId, bool trackChanges);
        Task UpdateProject(Guid id, ProjectToUpdateDto projectRequest, string userId, bool trackChanges);
        Task DeleteProject(Guid id, string userId, bool trackChanges);
    }
}