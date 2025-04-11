using AncientAura.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface ITokenService
    {
       Task<string> CreateTokenAsync(AppUser appUser, UserManager<AppUser> userManager);
    }
}
