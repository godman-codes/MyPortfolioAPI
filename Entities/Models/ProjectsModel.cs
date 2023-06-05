using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class ProjectsModel : EntityBase<Guid>
    {
        public ProjectsModel() : base()
        {

        }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string GitHubLink { get; set; }
        public string ProductionLink { get; set; }
        [ForeignKey(nameof(UserModel))]
        public string OwnerId { get; set; }
        public UserModel Owner { get; set; }
        public ICollection<ProjectTechnologiesModel> ProjectTechnologies { get; set; }
    }
}
