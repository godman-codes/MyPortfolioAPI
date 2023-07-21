﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constants;

namespace Entities.Exceptions
{
    public class ProjectNotFound : NotFoundException
    {
        public ProjectNotFound(Guid id) : base(string.Format(Constants.NotFoundSentence, Constants.Project, id))
        {

        }

    }
}
