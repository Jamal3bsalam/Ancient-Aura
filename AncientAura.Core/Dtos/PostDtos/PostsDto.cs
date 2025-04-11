using AncientAura.Core.Entities.Community;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.PostDtos
{
    public class PostsDto
    {
        public int? Id { get; set; }
        public string? AuthorName { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } 
        public int? ReactCount { get; set; }
        public int? CommentCount { get; set; }
        public int? ShareCount { get; set; }
        public ICollection<PostImagesDto>? Images { get; set; } = new List<PostImagesDto>();  

    }
}
