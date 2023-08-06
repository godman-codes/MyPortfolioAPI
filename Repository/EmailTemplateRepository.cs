using Contracts;
using Entities.SystemModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enum;

namespace Repository
{
    public class EmailTemplateRepository: RepositoryBase<EmailTemplateModel, long>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(MyProjectDbContext context): base(context)
        {

        }

        public void CreateEmailTemplate(EmailTemplateModel emailTemplate)
        {
            Create(emailTemplate);
        }

        public async Task<EmailTemplateModel> GetEmailTemplate(EmailTypeEnums emailType, bool trackChanges)
        {
            return await FindByCondition(x => x.EmailType == emailType, trackChanges).FirstOrDefaultAsync();
        }

        public async Task<EmailTemplateModel> GetEmailTemplate(long id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id == id, trackChanges).FirstOrDefaultAsync();
        }
    }
}
