using Entities.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enum;

namespace Contracts
{
    public interface IEmailTemplateRepository
    {
        void CreateEmailTemplate(EmailTemplateModel emailTemplate);
        Task<EmailTemplateModel> GetEmailTemplate(EmailTypeEnums emailType, bool trackChanges);
        Task<EmailTemplateModel> GetEmailTemplate(long id, bool trackChanges);
    }
}
