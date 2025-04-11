using AncientAura.Core.Entities.Community;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.CommentDto
{
    public class RepliesDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? UserName { get; set; }
        public int? ReactCount { get; set; }
        public int? RepliesCount { get; set; }
        public CommentImagesDto? CommentFile { get; set; }
        public int? ParentCommentId { get; set; }
        //public List<React> Reacts { get; set; } = new List<React>();
        //public List<CommentsDto> Replies { get; set; } = new List<CommentsDto>();
    }
}
