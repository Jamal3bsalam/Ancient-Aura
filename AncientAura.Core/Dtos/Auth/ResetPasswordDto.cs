using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.Auth
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password Is Required !!")]
        [MinLength(6, ErrorMessage = "Password must be at least 4 characters long.")]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password don't match with ConfirmPassword")]
        public string? ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Otp { get; set; }

    }
}
