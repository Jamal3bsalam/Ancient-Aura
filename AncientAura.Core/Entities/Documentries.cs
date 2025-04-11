using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class Documentries : BaseEntitiy<int>
    {
        public string? Name { get; set; }
        public string? ContentCreator { get; set; }
        public string? DocPictureUrl { get; set; }
        public string? DocumentryUrl { get; set; }
        public string? Discription { get; set; }
        public ICollection<Reviews>? Reviews { get; set; } = new List<Reviews>();
        public int? ViewCount { get; set; }

    }
}
