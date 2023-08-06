using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constants;

namespace Entities.Exceptions
{
    public class UserNotFound: NotFoundException
    {
        public UserNotFound(string id) : base(string.Format(Constants.NotFoundSentence, Constants.User, id))
        {

        }
    }
}
