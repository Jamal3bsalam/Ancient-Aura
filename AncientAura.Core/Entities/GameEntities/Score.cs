using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.GameEntities
{
    public class Score : BaseEntitiy<int>
    {
        public int TotalScore { get; set; }
        public int? AnswerId { get; set; }
        public Answer? Answer { get; set; }
    }
}
