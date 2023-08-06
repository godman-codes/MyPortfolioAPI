using Entities.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enum;

namespace Contracts
{
    public interface IEmailRepository
    {
        void CreateEmailLog(EmailModel email);
        Task<IEnumerable<EmailModel>> GetAllPendingEmails(MessageStatusEnums status, bool trackChanges);
    }
}
