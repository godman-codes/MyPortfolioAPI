using Contracts;
using Entities.SystemModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository;
using Service.Contracts;

namespace Service.BackgroundServices
{
    public class PortfolioBackgroundService : BackgroundService
    {
        public IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger _logger;
        private const int page = 0;
        private const int pageSize = 100;
        private readonly SMTPSettings sMTPSettings;
        //Microsoft.Extensions.Hosting.IHostEnvironment

        //private readonly IRepositoryManager _repository;
        //private readonly IServiceManager _service;

        public PortfolioBackgroundService(IServiceScopeFactory serviceScopeFactory, 
            ILogger<PortfolioBackgroundService> logger, IOptions<SMTPSettings> _config)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            sMTPSettings = _config.Value;
           
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using IServiceScope scope = _serviceScopeFactory.CreateScope();
                    IServiceProvider servicesProvider = scope.ServiceProvider;
                    MyProjectDbContext context = servicesProvider.GetService<MyProjectDbContext>();

                    await SendEmailsInBatches(context);
                    await Task.Delay(50000, stoppingToken);
                }
            }
            catch (Exception ex )
            {

                _logger.LogError(ex.Message);
            }
        }

        private async Task SendEmailsInBatches(MyProjectDbContext context)
        {
            try
            {
                EmailServiceActor emailService = new EmailServiceActor(context);
                List<EmailModel> pendingEmails = await emailService.GetPendingEmails(page, pageSize);

                await SendEmails(pendingEmails, emailService);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error Logger Initialzed");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex);

                _logger.LogError(ex.Message);
            }
        }
        private async Task SendEmails(List<EmailModel> pendingEmails, IEmailService service)
        {
            foreach (EmailModel pendingEmail in pendingEmails)
            {
                await service.SendEmail(pendingEmail, sMTPSettings);
                
            }
        }

    }
}
