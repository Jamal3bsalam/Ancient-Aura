using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.Auth
{
    public class CurrentUserDto
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public ICollection<string>? Links { get; set; }
    }
}
