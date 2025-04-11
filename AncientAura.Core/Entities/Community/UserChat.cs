using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class UserChat
    {
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public int? ChatId { get; set; }
        public Chat? Chat { get; set; }
        public DateTime? JoindAt { get; set; } = DateTime.UtcNow;
    }
}
