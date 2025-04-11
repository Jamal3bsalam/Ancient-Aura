using AncientAura.Core.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface IUserService
    {
       Task<UserDto>LogInAsync(LoginDto loginDto);
       Task<UserDto>RegisterAsync(RegisterDto registerDto);
       Task<bool>CheckEmailExistAsync(string email);
       Task<string> OtpRequest(ForgetPassDto forgetPassDto);
       Task<string> OtpVerificationn(string email , string Otp);
       Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
