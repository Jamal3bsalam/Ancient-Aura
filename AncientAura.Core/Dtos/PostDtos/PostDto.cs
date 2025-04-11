using AncientAura.Core.Dtos.CommentDto;
using AncientAura.Core.Entities.Community;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.PostDtos
{
    public class PostDto
    {
        public int? Id { get; set; }
        public string? AuthorName { get; set; }
        public string? Content { get; set; }
        public int? ReactCount { get; set; }
        public int? CommentCount { get; set; }
        public DateTime CreatedAt { get; set; } 
        public ICollection<PostImagesDto>? Images { get; set; } = new List<PostImagesDto>();
        public ICollection<CommentsDto>? Comments { get; set; } = new List<CommentsDto>();
        //public ICollection<React>? Reacts { get; set; } = new List<React>();
    }
}
