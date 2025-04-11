using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Identity
{
    public class Links
    {
        public int Id { get; set; }
        public string? Link { get; set; }
        public AppUser? AppUser { get; set; }
        public string? AppUserId { get; set; }
    }
}
