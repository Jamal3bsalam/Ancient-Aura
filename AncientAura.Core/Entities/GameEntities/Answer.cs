using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.GameEntities
{
    public class Answer : BaseEntitiy<int>
    {
        public string? UserAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? QuestionId { get; set; }
        public Question? Question { get; set; }
        public Score? Score { get; set; }
    }
}
