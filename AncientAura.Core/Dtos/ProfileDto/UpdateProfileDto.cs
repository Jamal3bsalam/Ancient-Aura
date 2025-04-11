using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.ProfileDto
{
    public class UpdateProfileDto
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Bio { get; set; }
        public ICollection<string>? Links { get; set; }
    }
}
