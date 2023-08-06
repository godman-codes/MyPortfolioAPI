using Entities.Responses;
using Entities.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enum;

namespace Service.Contracts
{
    public interface IEmailService
    {
        
        Task UserEmail(string userId, EmailTypeEnums emailType);
        Task<EmailContent> GetEmailContent(EmailModel pendingEmail);
        Task<EmailContent> ProcessMessage(EmailModel email);
        void CreateEmail(string message, string subject, string email);
        void CreateEmail(string message, string subject, List<string> emails);
        Task<List<EmailModel>> GetPendingEmails(int page, int pageSize);
        Task SendEmail(EmailModel pendingEmail, SMTPSettings sMTP);
    }
}
