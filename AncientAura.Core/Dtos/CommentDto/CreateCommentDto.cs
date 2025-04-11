using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.CommentDto
{
    public class CreateCommentDto
    {
        public string? Content { get; set; }
        public IFormFile? File { get; set; }
        public int? PostId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
