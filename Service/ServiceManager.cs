

using Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<ITechnologyService> _technologyService;
        private readonly Lazy<IWorkExperienceService> _workExperienceService;
        private readonly Lazy<IProjectTechnologiesService> _projectTechnologyService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager, loggerManager));
            _technologyService = new Lazy<ITechnologyService>(() => new TechnologyService(repositoryManager, loggerManager));
            _workExperienceService = new Lazy<IWorkExperienceService>(() => new WorkExperienceService(repositoryManager, loggerManager));
            _projectTechnologyService = new Lazy<IProjectTechnologiesService>(() => new ProjectTechnologiesService(repositoryManager, loggerManager));
        }
        public IProjectService ProjectService => _projectService.Value;

        public ITechnologyService TechnologyService => _technologyService.Value;

        public IWorkExperienceService WorkExperienceService => _workExperienceService.Value;

        public IProjectTechnologiesService ProjectTechnologiesService => _projectTechnologyService.Value;
    }
}
