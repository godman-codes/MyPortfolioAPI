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
    public interface IEmailTemplateService
    {
        Task<EmailTemplateResponse> GetEmailTemplateToResponse(EmailTypeEnums emailType, bool trackChanges);
        Task<EmailTemplateModel> GetEmailTemplate(EmailTypeEnums emailType, bool trackChanges);
        Task<EmailTemplateModel> GetEmailTemplate(long id, bool trackChanges);
    }
}
