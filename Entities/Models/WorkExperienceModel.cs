

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class WorkExperienceModel : EntityBase<Guid>
    {
        public WorkExperienceModel() : base()
        {

        }
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Role name is a required field.")]
        public string Role { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        [ForeignKey(nameof(UserModel))]
        public string? OwnerId { get; set; }
        public UserModel Owner { get; set; }
    }
}
