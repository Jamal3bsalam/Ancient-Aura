using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.GameEntities
{
    public class Practice
    {
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? SymbolId { get; set; }
        public Symbol? Symbol { get; set; }
    }
}
