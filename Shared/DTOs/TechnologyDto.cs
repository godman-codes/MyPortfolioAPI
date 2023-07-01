using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public abstract record TechnologyDto
    {

        [Required(ErrorMessage = "Technology name is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; init; }
        public int? Percentage { get; init; }
    }
}
