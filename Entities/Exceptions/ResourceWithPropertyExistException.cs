using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constants;

namespace Entities.Exceptions
{
    public class ResourceWithPropertyExistException: BadRequestException
    {
        public ResourceWithPropertyExistException(): base(String.Format(Constants.ResouceWithProperyExist, Constants.Technology, Constants.Name))
        {

        }
    }
}
