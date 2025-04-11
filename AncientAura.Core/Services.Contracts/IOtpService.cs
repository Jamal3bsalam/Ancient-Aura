using AncientAura.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface IOtpService
    {
        Task<string> OtpGenerateAsync(ForgetPassDto forgetPassDto);
        Task<bool> OtpValidation(string email , string Otp);
       
    }
}
