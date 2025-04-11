using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class PostImages:BaseEntitiy<int>
    {
        public string? ImageUrl { get; set; }
        public int? PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
    }
}
