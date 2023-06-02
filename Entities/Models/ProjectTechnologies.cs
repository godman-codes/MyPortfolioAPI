using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProjectTechnologies : EntityBase<Guid>
    {
        public string Name { get; set; }  
        public Guid ProjectId { get; set; }
        public Projects Project { get; set; }

    }
}
