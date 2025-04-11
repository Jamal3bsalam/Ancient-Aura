using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.PostDtos
{
    public class UpdatePostDto
    {
        public int? postId { get; set; }
        public string? Content { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<IFormFile>? Image { get; set; }
    }
}
