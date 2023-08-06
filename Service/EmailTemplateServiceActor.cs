using Entities.Responses;
using Entities.SystemModels;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enum;
using Utilities.Utilities;

namespace Service
{
    public class EmailTemplateServiceActor : RepositoryBase<EmailTemplateModel, long>, IEmailTemplateService
    {
        public EmailTemplateServiceActor(MyProjectDbContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<EmailTemplateModel> GetEmailTemplate(EmailTypeEnums emailType, bool trackChanges)
        {
            EmailTemplateModel emailTemplate = await FindByCondition(r => r.EmailType == emailType, trackChanges).SingleOrDefaultAsync();
            if (emailTemplate == null)
            {
                return new EmailTemplateModel()
                {
                    Template = DefaultTemplates.GetEmailTemplate(emailType),
                    Subject = DefaultTemplates.GetEmailSubject(emailType),
                };
            }


            return emailTemplate;
        }

        public async Task<EmailTemplateModel> GetEmailTemplate(long id, bool trackChanges)
        {
            return await FindByCondition(r => r.Id == id, trackChanges).FirstOrDefaultAsync();
        }

        public async Task<EmailTemplateResponse> GetEmailTemplateToResponse(EmailTypeEnums emailType, bool trackChanges)
        {
            EmailTemplateResponse emailTemplate = await FindByCondition(r => r.EmailType == emailType, trackChanges)
                .Select(r => new EmailTemplateResponse()
                {
                    EmailType = r.EmailType,
                    Id = r.Id,
                    Template = r.Template,
                    Subject = r.Subject
                })
                .FirstOrDefaultAsync();

            return ((emailTemplate == null) ? (new EmailTemplateResponse()
            {
                EmailType = emailType,
                Template = DefaultTemplates.GetEmailTemplate(emailType),
                Subject = DefaultTemplates.GetEmailSubject(emailType),
            }) : (emailTemplate));
        }
    }
}
