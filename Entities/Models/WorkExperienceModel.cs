

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class WorkExperienceModel : EntityBase
    {
        public WorkExperienceModel() : base()
        {

        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("WorkExperienceId")]
        public Guid Id { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
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
