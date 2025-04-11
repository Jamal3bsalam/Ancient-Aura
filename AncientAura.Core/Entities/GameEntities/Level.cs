using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.GameEntities
{
    public class Level : BaseEntitiy<int>
    {
        public int LevelNumber { get; set; }
        public int? GameId { get; set; }
        public Game? Game { get; set; }
        public ICollection<Question>? Questions { get; set; } = new List<Question>();

    }
}
