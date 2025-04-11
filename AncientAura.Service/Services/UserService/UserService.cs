using AncientAura.Core.Dtos.Auth;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts;
using AncientAura.Repository.Data.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AncientAura.Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly AncientAuraDbContext _context;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager ,
            ITokenService tokenService,
            AncientAuraDbContext context,
            IOtpService otpService,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
            _otpService = otpService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<UserDto> LogInAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) return null; // Not Found
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return null;

            return new UserDto()
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user,_userManager)
            };
           
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await CheckEmailExistAsync(registerDto.Email)) return null;

            var user = new AppUser()
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return null;
            return new UserDto()
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }



        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<string> OtpRequest(ForgetPassDto forgetPassDto)
        {
           
            var otp = await _otpService.OtpGenerateAsync(forgetPassDto);
            var result = await _emailService.SendEmailAsync(forgetPassDto.Email, "Reset Password OTP", $"Your Reset Passord OTP Is : {otp}");
            if (result is null) return null;

            return "Your OTP Send Succefully";
        }

        public async Task<string> OtpVerificationn(string email , string Otp)
        {
            bool isValid = await _otpService.OtpValidation(email,Otp);
            if (!isValid) return "Invalid or expired OTP";
            return "OTP verified successfully.";
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            bool isValid = await _otpService.OtpValidation(resetPasswordDto.Email,resetPasswordDto.Otp);
            if (!isValid) return null;

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user is null) return null;

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (string.IsNullOrEmpty(resetToken)) return null;

            var result = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDto.Password);
            if (!result.Succeeded) return null;

            return null;
        }
    }
}
