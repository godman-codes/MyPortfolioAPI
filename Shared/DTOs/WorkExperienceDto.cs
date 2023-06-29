using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public abstract record WorkExperienceDto
    {
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Role name is a required field.")]
        public string Role { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
