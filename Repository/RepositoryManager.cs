using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly MyProjectDbContext _context;
        private readonly Lazy<IProjectRepository> _projectRepository;
        private readonly Lazy<ITechnologiesRepository> _technologyRepository;
        private readonly Lazy<IWorkExperienceRepository> _workExperienceRepository;
        private readonly Lazy<IEmailRepository> _emailRepository;
        private readonly Lazy<IEmailTemplateRepository> _emailTemplateRepository;

        public RepositoryManager(MyProjectDbContext context)
        {
            _context = context;
            _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(context));
            _technologyRepository = new Lazy<ITechnologiesRepository>(() => new TechnologyRepository(context));
            _workExperienceRepository = new Lazy<IWorkExperienceRepository>(() => new WorkExperienseRepository(context));
            _emailRepository = new Lazy<IEmailRepository>(() => new EmailRepository(context));
            _emailTemplateRepository = new Lazy<IEmailTemplateRepository>(() => new EmailTemplateRepository(context));
        }
        public IProjectRepository Projects => _projectRepository.Value;

        public ITechnologiesRepository Technologies => _technologyRepository.Value;
        public IWorkExperienceRepository WorkExperience => _workExperienceRepository.Value;

        public IEmailRepository EmailRepository => _emailRepository.Value;

        public IEmailTemplateRepository EmailTemplateRepository => _emailTemplateRepository.Value;

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
