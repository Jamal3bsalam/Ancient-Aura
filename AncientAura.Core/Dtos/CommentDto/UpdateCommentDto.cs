using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.CommentDto
{
    public class UpdateCommentDto
    {
        public int? CommentId { get; set; }
        public string? Content { get; set; }
        public IFormFile? File { get; set; }
    }
}
