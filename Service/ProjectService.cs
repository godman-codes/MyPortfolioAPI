using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTOs.Request;
using Shared.DTOs.Response;
using Utilities.Constants;

namespace Service
{
    internal sealed class ProjectService : IProjectService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProjectService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProjectResponseDto> CreateProject(ProjectRequestDto projectRequest, string userId, bool trackChanges)
        {
            await ProjectExistByName(projectRequest.Name, userId, trackChanges);
            await ProjectExistByLink(projectRequest.GitHubLink, userId, trackChanges);


            
            var projectToCreated = _mapper.Map<ProjectsModel>(projectRequest,opts => opts.Items["OwnerId"] = userId);
            _repository.Projects.CreateProject(projectToCreated);
            await _repository.Save();
            var projectToReturn = _mapper.Map<ProjectResponseDto>(projectToCreated);
            return projectToReturn;
            
        }

        public async Task DeleteProject(Guid id, string userId, bool trackChanges)
        {
            try
            {
                var projectToDelete = await ProjectExistById(id, userId, trackChanges);
                projectToDelete.ToDeletedEntity();
                await _repository.Save();
            }
            catch (ProjectNotFound ex)
            {
                _logger.LogInfo("Project to delete not found");
            }
        }

        public async Task<IEnumerable<ProjectResponseDto>> GetAllProjects(string userId, bool trackChanges)
        {
            var allProjects = _mapper.Map<IEnumerable<ProjectResponseDto>>(await _repository.Projects.GetAllProjects(userId, trackChanges));
            return allProjects;
            
        }

        public async Task<ProjectResponseDto> GetProject(Guid id, string userId, bool trackChanges)
        {
            var projectToReturn = _mapper.Map<ProjectResponseDto>(await ProjectExistById(id, userId, trackChanges));
            return projectToReturn;

        }

        public async Task UpdateProject(Guid id, ProjectToUpdateDto projectRequest, string userId, bool trackChanges)
        {
            var projectToUpdate = await ProjectExistById(id, userId, trackChanges);
            if (projectToUpdate.Name.ToLower() != projectRequest.Name.ToLower())
            {
                await ProjectExistByName(userId, projectRequest.Name, trackChanges);
            }
            if (projectToUpdate.GitHubLink.ToLower() != projectRequest.GitHubLink.ToLower())
            {
                await ProjectExistByLink(userId, projectRequest.GitHubLink, trackChanges);
            }
            var Updator = _mapper.Map(  projectRequest, projectToUpdate);
            Updator.ToUpdate();
            await _repository.Save();
        }
        private protected async Task<ProjectsModel> ProjectExistById(Guid Id, string userId, bool trackChanges)
        {
            var existingProject = await _repository.Projects.GetProject(Id, userId, trackChanges);
            if (existingProject == null)
            {
                throw new ProjectNotFound(Id);
            }
            return existingProject;

        }

        private protected  async Task<bool> ProjectExistByName(string name, string userId, bool trackChanges)
        {
            if (!await _repository.Projects.GetProjectByName(name, userId, trackChanges))
            {
                throw new AlreadyExistsException(Constants.Project, Constants.Name);
            }
            return true;

        }

        private protected  async Task<bool> ProjectExistByLink(string Link, string userId, bool trackChanges)
        {
            if (!await _repository.Projects.GetProjectByGithubLink(Link, userId, trackChanges))
            {
                throw new AlreadyExistsException(Constants.Project, Constants.GithubLink);
            }
            return true;

        }

    }
}
