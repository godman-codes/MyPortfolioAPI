using System.ComponentModel.DataAnnotations;


namespace Entities.Models
{
    public class Projects : EntityBase<Guid>
    {
        public Projects() : base()
        {

        }
        public string Name { get; set; }
        public string Description { get; set; }
        public UrlAttribute GitHubLink { get; set; }
        public UrlAttribute ProductionLink { get; set;  }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<ProjectTechnologies> ProjectTechnologies { get; set; }
    }
}
