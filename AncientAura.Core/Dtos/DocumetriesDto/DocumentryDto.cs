using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.DocumetriesDto
{
    public class DocumentryDto
    {
        public int? Id { get; set; }
        public string? DocPictureUrl { get; set; }
        public string? Name { get; set; }
        public string? ContentCreator { get; set; }
        public string? DocumentryUrl { get; set; }
        public string? Description { get; set; }
        public int? ViewCount { get; set; }
        public ICollection<ReviewsDto>? Reviews { get; set; } = new List<ReviewsDto>();
    }
}
