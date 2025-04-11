using AncientAura.Core.Dtos.Auth;
using AncientAura.Core.Dtos.ProfileDto;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts;
using AncientAura.Repository.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Account - Profile")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProfileService _profileService;
        private readonly AncientAuraDbContext _context;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<AppUser> userManager,IProfileService profileService,AncientAuraDbContext context,IConfiguration configuration)
        {
            _userManager = userManager;
            _profileService = profileService;
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("CurrentUser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return BadRequest();

            //var user = await _userManager.FindByEmailAsync(userEmail);
            var user = _context.Users.Include(u => u.Links).FirstOrDefault(u => u.Id == userId);
            if (user == null) return BadRequest();
            return Ok(new CurrentUserDto()
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePicture = _configuration["BASEURL"]+ user.ProfileImage,
                Bio = user.Bio,
                Links = user.Links.Select(Link => Link.Link).ToList()
            });
        }

        [HttpPost("ProfileUpdate")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateProfiel(UpdateProfileDto updateProfileDto)
        {
            //var user = _userManager.FindByEmailAsync(currentUserDto.Email);
            //if (user is null) return BadRequest("Invalid Operation");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null) return BadRequest(); 

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();

           var result = _profileService.Update(updateProfileDto);
            if (result is null) return BadRequest();
            return Ok();

        }
        [HttpPost("UploadProfileImage")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UploadProfileImage(ProfileImageDto profileImageDto)
        {
            if (profileImageDto.File == null || profileImageDto.File.Length == 0) return BadRequest("No File Uploaded");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Invalid Operation");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return BadRequest();

            if (!string.IsNullOrEmpty(user.ProfileImage)) return BadRequest();
            string file =  _profileService.Upload(profileImageDto);
            if (file == null) return BadRequest();

            user.ProfileImage = $"Images\\ProfileImage\\{file}";
            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "Image uploaded successfully" });
        }

        [HttpDelete("DeleteProfileImage")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteProfileImage()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) return NotFound();

            if (string.IsNullOrEmpty(user.ProfileImage)) return BadRequest("No Image Profile");
            _profileService.Delete(user.ProfileImage);

            user.ProfileImage = null;
            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "Image Deleted successfully" });
        }

        [HttpPut("UpdateProfileImage")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateProfileImage(ProfileImageDto profileImageDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(user.ProfileImage)) 
            {
                _profileService.Delete(user.ProfileImage);
            }

            string file = _profileService.Upload(profileImageDto);
            if (file == null) return BadRequest();

            user.ProfileImage = $"Images\\ProfileImage\\{file}";
            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "Image Updated successfully" });
        }

    }
}
