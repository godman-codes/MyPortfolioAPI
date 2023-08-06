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
    public class EmailRepository : RepositoryBase<EmailModel, Guid>, IEmailRepository 
    {
        public EmailRepository(MyProjectDbContext context) : base(context)
        {

        }

        public void CreateEmailLog(EmailModel email)
        {
            Create(email);
        }

        public async Task<IEnumerable<EmailModel>> GetAllPendingEmails(MessageStatusEnums status, bool trackChanges)
        {
            return await FindByCondition(x => x.Status == status, trackChanges).ToListAsync();
        }
    }
}
