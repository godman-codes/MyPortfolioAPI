using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constants;

namespace Entities.Exceptions
{
    public class AlreadyExistsException : BadRequestException
    {
        public AlreadyExistsException(string attribute, string model) : base(String.Format(Constants.ResouceWithProperyExist, model, attribute))
        {

        }
    }
}