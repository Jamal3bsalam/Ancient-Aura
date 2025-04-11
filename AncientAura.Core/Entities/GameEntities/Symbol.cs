using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.GameEntities
{
    public class Symbol : BaseEntitiy<int>
    {
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public string? Meaning { get; set; }
        public ICollection<Practice>? Practices { get; set; } = new List<Practice>();
        public int? QuestionId { get; set; } // FK
        public Question? Question { get; set; }
    }
}
