using AncientAura.Core.Entities.Community;
using AncientAura.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AncientAura.Core.Entities.GameEntities;
using AncientAura.Core.Entities.WishLists;

namespace AncientAura.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }
        public string? Bio { get; set; }
        public WishList? WishList { get; set; }
        public ICollection<Post>? Posts { get; set; } = new List<Post>(); // Navigation Property
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        public ICollection<React>? Reacts { get; set; } = new List<React>();
        public ICollection<UserChat>? UserChats { get; set; } = new List<UserChat>();
        public ICollection<Reviews>? Reviews { get; set; } = new List<Reviews>();
        public ICollection<Game>? Games { get; set; } = new List<Game>();
        public ICollection<Answer>? Answers { get; set; } = new List<Answer>();
        public ICollection<Practice>? Practices { get; set; } = new List<Practice>();
        public ICollection<Links>? Links { get; set; } = new List<Links>();

    }
}
