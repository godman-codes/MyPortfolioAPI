using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constants;

namespace Entities.Exceptions
{
    public class TechnologyNotFound : NotFoundException
    {
        public TechnologyNotFound(Guid id) : base(String.Format(Constants.NotFoundSentence, Constants.Technology, id))
        {

        }
    }
}
