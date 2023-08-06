using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IProjectService ProjectService { get; }
        ITechnologyService TechnologyService { get; }
        IWorkExperienceService WorkExperienceService { get; }
        IAuthenticationService AuthenticationService { get; }
        IEmailService EmailService { get;  }
        //IEmailTemplateService EmailTemplateService { get; }
    }
}
