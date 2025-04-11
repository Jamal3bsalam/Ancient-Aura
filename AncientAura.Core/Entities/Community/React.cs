using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class React : BaseEntitiy<int>
    {
        public ReactType? Type { get; set; } // "Like", "Love", "Angry", etc.
        public string? UserId { get; set; }
        [JsonIgnore]
        public AppUser? User { get; set; }
        public int? PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
        public int? CommentId { get; set; }
        [JsonIgnore]
        public Comment? Comment { get; set; }
    }
}
