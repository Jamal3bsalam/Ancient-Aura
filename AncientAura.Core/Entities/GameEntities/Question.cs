using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.GameEntities
{
    public class Question : BaseEntitiy<int>
    {
        public string? QuestionText { get; set; }
        public int? LevelId { get; set; }
        public Level? Level { get; set; }
        public Symbol? Symbol { get; set; }
        public Answer? Answer { get; set; }
    }
}
