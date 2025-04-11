using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class Post : BaseEntitiy<int>
    {
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UserId { get; set; }
        [JsonIgnore]
        public AppUser? User { get; set; }
        public int? ShareCount { get; set; } = 0;
        public ICollection<PostImages>? Images { get; set; } = new List<PostImages>();
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        [JsonIgnore]
        public ICollection<React>? Reacts { get; set; } = new List<React>();
    }
}
