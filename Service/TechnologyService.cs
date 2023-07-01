using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTOs.Request;
using Shared.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TechnologyService : ITechnologyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TechnologyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TechnologyResponseDto> CreateTechnology(TechnologyRequestDto technologyRequest, string userId)
        {
            if (await TechnologyExistByName(technologyRequest.Name, false, userId ))
            {
                throw new ResourceWithPropertyExistException();
            }
            var technologyEntities = _mapper.Map<TechnologiesModel>(technologyRequest, opts => opts.Items["OwnerId"] = userId);
            _repository.Technologies.CreateTechnology(technologyEntities);
            await _repository.Save();
            var technologyToReturn = _mapper.Map<TechnologyResponseDto>(technologyEntities);
            return technologyToReturn;

        }

        public async Task DeleteTechnology(Guid id, string userId, bool trackChanges)
        {
            try
            {

                var technologyToDelete = await TechnologyExistById(id, trackChanges, userId);
                technologyToDelete.ToDeletedEntity();
                await _repository.Save();
            }
            catch (TechnologyNotFound ex)
            {
                
                _logger.LogInfo("Technology to delete not found");
            }
        }

        public async Task<IEnumerable<TechnologyResponseDto>> GetAllTechnologies(string userId, bool trackChanges)
        {
            var technnologies = await _repository.Technologies.GetAllTechnologies(userId, trackChanges);
            var technologiesToReturn = _mapper.Map<IEnumerable<TechnologyResponseDto>>(technnologies);
            return technologiesToReturn;
        }

        public async Task<TechnologyResponseDto> GetTechnology(Guid id, bool trackChanges, string userId)
        {
            var technology = await TechnologyExistById(id, trackChanges, userId);
            var technologyToReturn = _mapper.Map<TechnologyResponseDto>(technology);
            return technologyToReturn;
        }

        public async Task UpdateTechnology(Guid id,TechnologyUpdateDto technologyUpdate, string userId, bool trackChanges)
        {
            var technology = await TechnologyExistById(id, trackChanges, userId);
            var technolologyToUpdate = _mapper.Map(technologyUpdate, technology);
            if (technologyUpdate.Name.ToLower() != technology.Name.ToLower())
            {
                if (await TechnologyExistByName(technologyUpdate.Name, trackChanges, userId))
                {
                    throw new ResourceWithPropertyExistException();
                }

            }
            technolologyToUpdate.ToUpdate();
            await _repository.Save();
        }

        private protected async Task<TechnologiesModel> TechnologyExistById(Guid id, bool trackChanges, string userId)
        {
            var technology = await _repository.Technologies.GetTechnology(id, trackChanges, userId);
            if (technology is null)
            {
                throw new TechnologyNotFound(id);
            }
            return technology;
        }
        private protected async Task<bool> TechnologyExistByName(string name, bool trackChanges, string userId)
        {
            var technology = await _repository.Technologies.GetTechnologyByName(name, trackChanges, userId);
            if (technology is null)
            {
                return false;
            }
            return true;
        }
    }
}
