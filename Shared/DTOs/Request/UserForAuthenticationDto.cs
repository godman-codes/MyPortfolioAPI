using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Request
{
    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email")]
        public string? Email { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

    }
}
