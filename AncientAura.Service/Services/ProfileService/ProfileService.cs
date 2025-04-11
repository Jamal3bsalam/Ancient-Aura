using AncientAura.Core.Dtos.Auth;
using AncientAura.Core.Dtos.ProfileDto;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts;
using AncientAura.Repository.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Service.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly AncientAuraDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProfileService(AncientAuraDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }
        public async Task<int> Update(UpdateProfileDto updateProfileDto)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString();
            if (userId == null) return 0;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return 0;
            user.FullName = updateProfileDto.FullName;
            user.UserName = updateProfileDto.UserName;
            user.Bio = updateProfileDto.Bio;
            user.Links = updateProfileDto.Links?.Select(Link => new Links { Link = Link }).ToList();


            _context.Update(user);
            var resutl = _context.SaveChanges();
            return resutl;
        }

        public string Upload(ProfileImageDto profileImageDto)
        {
            // D:\AncientAura\AncientAura\AncientAura.APIs\wwwroot\Images\ProfileImage\
           // string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"\\wwwroot\\Images\\{profileImageDto.FolderName}");
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string folderPath = (CurrentDirectory + $"\\wwwroot\\Images\\ProfileImage");

            string fileName = profileImageDto.File.FileName;

            string filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            profileImageDto.File.CopyTo(fileStream);

            return fileName;
        }
        public void Delete(string fileName)
        {
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string filePath = (CurrentDirectory + $"\\wwwroot\\{fileName}");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

        }
    }
}
