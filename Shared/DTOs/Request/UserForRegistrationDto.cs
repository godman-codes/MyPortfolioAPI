﻿

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Shared.DTOs.Request
{
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "FirstName is Required!")]
        public string? FirstName { get; init; }
        [Required(ErrorMessage = "LastName is Required!")]
        public string? LastName { get; init; }
        public string? Username { get; init; }
        [Required(ErrorMessage = "Password is Required!")]
        [DataType(DataType.Password)]
        public string? Password { get; init; }
        [Required(ErrorMessage = "Confirm Password is Required!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match!")]

        public string? confirmPassword { get; init; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; init; }
        public ICollection<string> Roles { get; init; }
    }
}
