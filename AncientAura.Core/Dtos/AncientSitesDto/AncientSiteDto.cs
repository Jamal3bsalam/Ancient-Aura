using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.AncientSitesDto
{
    public class AncientSiteDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public string? VoiceUrl { get; set; }
        public int? ViewCount { get; set; }
        public string? Adddress { get; set; }
        public string? OpeningTime { get; set; }
        public string? ClosedTime { get; set; }
        public ICollection<ReviewsDto> Reviews { get; set; } = new List<ReviewsDto>();
        public ICollection<ImagesDto>? ImageURLs { get; set; } = new List<ImagesDto>();
    }
}
