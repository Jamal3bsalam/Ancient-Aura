using AncientAura.Core.Dtos.Auth;
using AncientAura.Core.Dtos.ProfileDto;
using AncientAura.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface IProfileService
    {
        public Task<int> Update(UpdateProfileDto updateProfileDto);
        public string Upload(ProfileImageDto profileImageDto);
        public void Delete(string fileName);
    }
}
