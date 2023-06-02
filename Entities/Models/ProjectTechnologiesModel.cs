using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProjectTechnologiesModel : EntityBase
    {
        public ProjectTechnologiesModel() : base()
        {

        }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("ProjectTechnologyId")]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(ProjectsModel))]
        public Guid ProjectId { get; set; }
        public ProjectsModel Project { get; set; }

    }
}
