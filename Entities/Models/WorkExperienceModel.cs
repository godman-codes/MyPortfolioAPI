

namespace Entities.Models
{
    public class WorkExperience : EntityBase<Guid>
    {
        public string Role { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
