using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.Auth
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? ProfilePic { get; set; }
        public string Token { get; set; }
    }
}
