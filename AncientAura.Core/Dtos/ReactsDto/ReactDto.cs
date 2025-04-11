using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.ReactsDto
{
    public class ReactDto
    {
        public string? ReactionType { get; set; }
        public string? UserName { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int? TotalReact { get; set; }
    }
}
