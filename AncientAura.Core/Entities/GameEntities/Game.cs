using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.GameEntities
{
    public class Game : BaseEntitiy<int>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Level>? Levels { get; set; } = new List<Level>();
    }
}
