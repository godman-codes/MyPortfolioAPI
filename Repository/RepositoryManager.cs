﻿using Contracts;
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
        private readonly Lazy<IProjectTechnologiesRepository> _ProjectTechnologyRepository;

        public RepositoryManager(MyProjectDbContext context)
        {
            _context = context;
            _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(context));
            _technologyRepository = new Lazy<ITechnologiesRepository>(() => new TechnologyRepository(context));
            _workExperienceRepository = new Lazy<IWorkExperienceRepository>(() => new WorkExperienseRepository(context));
            _ProjectTechnologyRepository = new Lazy<IProjectTechnologiesRepository>(() => new ProjectTechnologyRepository(context));
      
        }
        public IProjectRepository Projects => _projectRepository.Value;

        public ITechnologiesRepository Technologies => _technologyRepository.Value;
        public IWorkExperienceRepository WorkExperienceRepository => _workExperienceRepository.Value;

        public IProjectTechnologiesRepository ProjectTechnologiesRepository => _ProjectTechnologyRepository.Value;

        public void save()
        {
            _context.SaveChanges();
        }
    }
}