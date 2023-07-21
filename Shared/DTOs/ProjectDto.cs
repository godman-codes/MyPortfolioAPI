using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public abstract record ProjectDto
    {
        [Required(ErrorMessage = "Project name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; init; }
        public string Description { get; init; }
        [RegularExpression(@"^(https?:\/\/)?(www\.)?github\.com\/[A-Za-z0-9_-]+\/[A-Za-z0-9_-]+(\/)?$",
        ErrorMessage = "Invalid GitHub link.")]
        public string GitHubLink { get; init; }
        [RegularExpression("^(?i)(author|contributor)$", ErrorMessage = "Invalid role. Accepted values are 'author' or 'contributor'.")]
        public string Role { get; init; }
        public string ProductionLink { get; init; }
        public string Tools { get; init; }
    }
}
