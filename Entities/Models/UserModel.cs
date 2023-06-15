using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class UserModel : IdentityUser
    {
        public UserModel() : base()
        {
            Technologies = new HashSet<TechnologiesModel>();
            Projects = new HashSet<ProjectsModel>();
            WorkExperiences = new HashSet<WorkExperienceModel>();

            IsDeleted = false;
            IsActive = true;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<TechnologiesModel> Technologies { get; set; }
        public ICollection<ProjectsModel> Projects { get; set; }
        public ICollection<WorkExperienceModel> WorkExperiences { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
