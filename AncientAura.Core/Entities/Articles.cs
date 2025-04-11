using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class Articles : BaseEntitiy<int>
    {
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public string? Discription { get; set; }
        public string? ArticlesPicUrl { get; set; }
        public string? Contenet { get; set; }
        public ICollection<Reviews>? Reviews { get; set; } = new List<Reviews>();
        public int? ViewCount { get; set; }

    }
}
