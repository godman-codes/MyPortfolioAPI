using Shared.DTOs.Request;
using Shared.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ITechnologyService
    {
        Task<IEnumerable<TechnologyResponseDto>> GetAllTechnologies(string userId, bool trackChanges);
        Task<TechnologyResponseDto> GetTechnology(Guid id, bool trackChanges, string userId);
        Task<TechnologyResponseDto> CreateTechnology(TechnologyRequestDto technologyRequest, string userId);
        Task UpdateTechnology(Guid id, TechnologyUpdateDto technologyUpdate, string userId, bool trackChanges);
        Task DeleteTechnology(Guid id, string userId, bool trackChanges);
    }
}
