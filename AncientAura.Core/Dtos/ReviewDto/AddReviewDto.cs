using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.ReviewDto
{
    public class AddReviewDto
    {
        public string? Comment { get; set; }
        public int? Rating { get; set; } // 1 - 5 scale
        public int? ArticlesId { get; set; } = null;
        public int? BooksId { get; set; } = null;
        public int? DocumentriesId { get; set; } = null;
        public int? AncientSitesId { get; set; } = null;
    }
}
