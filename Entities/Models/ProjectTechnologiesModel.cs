using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProjectTechnologiesModel : EntityBase<Guid>
    {
        public ProjectTechnologiesModel() : base()
        {

        }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(ProjectsModel))]
        public Guid ProjectId { get; set; }
        public ProjectsModel Project { get; set; }

    }
}
