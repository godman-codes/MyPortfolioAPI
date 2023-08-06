using Contracts;
using Entities.Models;
using Entities.SystemModels;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System.Net;
using System.Net.Mail;
using Utilities.Constants;
using Utilities.Enum;
using Utilities.Utilities;

namespace Service
{
    public class EmailService : IEmailService
    {
        private readonly IRepositoryManager _repository;
    
        public EmailService(IRepositoryManager repository)
        {
            _repository = repository;
           
        }

        public void CreateEmail(string message, string subject, string email)
        {
            _repository.EmailRepository.CreateEmailLog(new EmailModel()
            {
                Emailaddresses = email,
                UpdatedDate = DateTime.Now,
                Message = message,
                Status = MessageStatusEnums.Pending,
                Subject = subject
            });
        }

        public void CreateEmail(string message, string subject, List<string> emails)
        {
            foreach (string email in emails)
            {
                CreateEmail(message, subject, email);
            }
        }

        public Task<EmailContent> GetEmailContent(EmailModel pendingEmail)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmailModel>> GetPendingEmails(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<EmailContent> ProcessMessage(EmailModel email)
        {
            throw new NotImplementedException();
        }

        public Task SendEmail(EmailModel pendingEmail, SMTPSettings sMTP)
        {
            throw new NotImplementedException();
        }

        public Task UserEmail(string userId, EmailTypeEnums emailType)
        {
            throw new NotImplementedException();
        }
    }
}
