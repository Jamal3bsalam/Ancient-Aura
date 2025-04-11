using AncientAura.Core.Dtos.ReviewDto;
using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.BookDto
{
    public class BookDto
    {
        public int? BookId { get; set; }
        public string? BookPictureUrl { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? BookUrl { get; set; }
        public string? Description { get; set; }
        public int? ViewsCount { get; set; }
        public List<ReviewsDto>? Reviews { get; set; }
    }
}
