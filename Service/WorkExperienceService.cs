using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTOs.Request;
using Shared.DTOs.Response;

namespace Service
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public WorkExperienceService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<WorkExperienceResponseDto> CreateWorkExperience(WorkExperienceRequestDto workExperience, string  userId)
        {
            var workExperienceEntity = _mapper.Map<WorkExperienceModel>(
                workExperience, opts => opts.Items["OwnerId"] = userId);

            _repository.WorkExperience.CreateWorkExperience(workExperienceEntity);
            await _repository.Save();
            var workexperienceToReturn = _mapper.Map<WorkExperienceResponseDto>(workExperienceEntity);
            return workexperienceToReturn;

        }

        public Task DeletWorkExperience(Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkExperienceResponseDto>> GetAllWorkExperience(bool trackChanges, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkExperienceResponseDto> GetWorkExperience(Guid id, bool trackchanges, string  userId)
        {
            var workExperience = await WorkExperienceExists(id, trackchanges, userId);
            var experienceDto = _mapper.Map<WorkExperienceResponseDto>(workExperience);
            return experienceDto;

        }

        public Task UpdateWorkxperience(Guid id, WorkExperienceToUpdateDto workExperienceToUpdate, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        private async Task<WorkExperienceModel> WorkExperienceExists(Guid id, bool trackChanges, string  userId)
        {
            var workExperience = await _repository.WorkExperience.GetWorkExperienceAsync(id, trackChanges, userId);
            if (workExperience is null)
            {
                throw new WorkExperienceNotFound(id);
            }
            return workExperience;
        }
    }
}