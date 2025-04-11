using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.Auth
{
    public class OtpVerificationDto
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Enter a Valid Email !!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Otp Is Required")]
        public string Otp { get; set; }
    }
}
