

using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<ITechnologyService> _technologyService;
        private readonly Lazy<IWorkExperienceService> _workExperienceService;
        private readonly Lazy<IProjectTechnologiesService> _projectTechnologyService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager,
            UserManager<UserModel> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMapper mapper

            )
        {
            _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager, loggerManager));
            _technologyService = new Lazy<ITechnologyService>(() => new TechnologyService(repositoryManager, loggerManager));
            _workExperienceService = new Lazy<IWorkExperienceService>(() => new WorkExperienceService(repositoryManager, loggerManager));
            _projectTechnologyService = new Lazy<IProjectTechnologiesService>(() => new ProjectTechnologiesService(repositoryManager, loggerManager));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(loggerManager, mapper,userManager,configuration, roleManager));
        }
        public IProjectService ProjectService => _projectService.Value;

        public ITechnologyService TechnologyService => _technologyService.Value;

        public IWorkExperienceService WorkExperienceService => _workExperienceService.Value;

        public IProjectTechnologiesService ProjectTechnologiesService => _projectTechnologyService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
