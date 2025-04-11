using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class Books : BaseEntitiy<int>
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? BookPictureUrl { get; set; }
        public string? Discription { get; set; }
        public string? BookURL { get; set; }
        public ICollection<Reviews>? Reviews { get; set; } = new List<Reviews>();
        public int? ViewCount { get; set; } = 0;

    }
}
