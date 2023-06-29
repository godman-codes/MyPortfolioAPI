using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constants;

namespace Entities.Exceptions
{
    public sealed class WorkExperienceNotFound : NotFoundException
    {
        public WorkExperienceNotFound(Guid id) : base(String.Format(Constants.NotFoundSentence, Constants.WorkExperience, id)) 
        {

        }
    }
}
