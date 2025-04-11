using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class CommentImages:BaseEntitiy<int>
    {
        public string? ImageUrl { get; set; }
        public int? CommentId { get; set; }
        public Comment? Comment { get; set; }
    }
}
