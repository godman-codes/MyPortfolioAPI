using Contracts;
using Service.Contracts;

namespace Service
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public WorkExperienceService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}