

using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using EntrustContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<ITechnologyService> _technologyService;
        private readonly Lazy<IWorkExperienceService> _workExperienceService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IEmailService> _emailService;

        //private readonly Lazy<IEmailTemplateService> _emailTemplateService;

        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager,
            UserManager<UserModel> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtConfiguration> configuration,
            IMapper mapper,
            IEntrustManager entrustManager

            )
        {
            _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager, loggerManager, mapper));
            _technologyService = new Lazy<ITechnologyService>(() => new TechnologyService(repositoryManager, loggerManager, mapper));
            _workExperienceService = new Lazy<IWorkExperienceService>(() => new WorkExperienceService(repositoryManager, loggerManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(loggerManager, mapper,userManager,configuration, roleManager, repositoryManager, entrustManager));
            _emailService = new Lazy<IEmailService>(() => new EmailService(repositoryManager));
            //_emailTemplateService = new Lazy<IEmailTemplateService>(() => new EmailTemplateService(repositoryManager));
        }
        public IProjectService ProjectService => _projectService.Value;

        public ITechnologyService TechnologyService => _technologyService.Value;

        public IWorkExperienceService WorkExperienceService => _workExperienceService.Value;


        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IEmailService EmailService => _emailService.Value;

        //public IEmailTemplateService EmailTemplateService => _emailTemplateService.Value;
    }
}
