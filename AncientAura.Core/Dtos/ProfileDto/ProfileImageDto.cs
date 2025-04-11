using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.ProfileDto
{
    public class ProfileImageDto
    {
        public IFormFile? File { get; set; }
    }
}
