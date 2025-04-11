using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.ArticlesDto
{
    public class ArticleDto
    {
        public int? Id { get; set; }
        public string? ArticlesPicUrl { get; set; }
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public string? Description { get; set; }
        public string? Contenet { get; set; }
        public int? ViewCount { get; set; }
        public ICollection<ReviewsDto>? Reviews { get; set; } = new List<ReviewsDto>();
    }
}
