using AncientAura.Core.Dtos.Auth;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService,UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn(LoginDto loginDto)
        {
            var user =  await _userService.LogInAsync(loginDto);
            if (user == null) return BadRequest("Invalid Login");
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);
            if (user == null) return BadRequest("User Is Already Exist");
            return Ok(user);
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPassDto forgetPassDto)
        {
            var user = await _userManager.FindByEmailAsync(forgetPassDto.Email);
            if (user == null) return BadRequest("Invalid Opration!!");
            var otp = await _userService.OtpRequest(forgetPassDto);
            return Ok(otp);
        }
        [HttpPost("otpVerification")]
        public async Task<IActionResult> OTPVerification(string email , string Otp)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest("InValid Operation");
            var result = _userService.OtpVerificationn(email,Otp);
            return Ok(result);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) return BadRequest("Invalid Operation");
            var result = await _userService.ResetPassword(resetPasswordDto);
            if (result is not null) return BadRequest("Invalid Operation");
            return Ok(new { Message = "Password Updated successfully" });
        }

        //[HttpGet("CurrentUser")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<ActionResult<UserDto>> GetCurrentUser()
        //{
        //    var userEmail =  User.FindFirstValue(ClaimTypes.Email);

        //    if (userEmail == null) return BadRequest();

        //    var user = await _userManager.FindByEmailAsync(userEmail);
        //    if (user == null) return BadRequest();
        //    return Ok(new CurrentUserDto()
        //    {
        //        FullName = user.FullName,
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        ProfilePicture = user.ProfileImage
        //    });
        //}
    }
}
