using AncientAura.Core.Dtos.Auth;
using AncientAura.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AncientAura.Service.Services.OtpService
{
    public class OtpService : IOtpService
    {
        private readonly Dictionary<string, string> _otpStorage = new();
        private readonly Dictionary<string, DateTime> _otpExpiry = new();
        public async Task<string> OtpGenerateAsync(ForgetPassDto forgetPassDto)
        {
            string otp = new Random().Next(1000, 9999).ToString(); // Generate 4-digit OTP
            _otpStorage[forgetPassDto.Email] = otp;
            _otpExpiry[forgetPassDto.Email] = DateTime.UtcNow.AddMinutes(5); // Set expiration time
            return otp;
        }

        public async Task<bool> OtpValidation(string email , string Otp)
        {
            if (!_otpStorage.ContainsKey(email) || _otpExpiry[email] < DateTime.UtcNow)
                return false;

            return _otpStorage[email] == Otp;
        }
    }
}
