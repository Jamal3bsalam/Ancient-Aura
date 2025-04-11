using AncientAura.Core.Entities.Community;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.ReactsDto
{
    public class CreateReactDto
    {
        public ReactType ReactType { get; set; }
        public int? CommentId { get; set; }
        public int? PostId { get; set; }
    }
}
