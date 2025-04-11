using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.ReviewDto
{
    public class ReviewsDto
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public int? Rating { get; set; } // 1 - 5 scale
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
