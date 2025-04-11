using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class Comment:BaseEntitiy<int>
    {
        public string? Content { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
        public string? UserId { get; set; }
        [JsonIgnore]
        public AppUser? User { get; set; }
        public int? PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
        public CommentImages? CommentImages { get; set; }
        [JsonIgnore]
        public ICollection<React>? Reacts { get; set; } = new List<React>();
        public int? ParentCommentId { get; set; }
        [JsonIgnore]
        public Comment? ParentComment { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Replies { get; set; } = new List<Comment>();   
    }
}
