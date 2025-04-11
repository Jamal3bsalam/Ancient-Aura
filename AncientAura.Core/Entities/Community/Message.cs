using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class Message : BaseEntitiy<int>
    {
        public string? Content { get; set; }
        public DateTime SendAt { get; set; } = DateTime.UtcNow;
        public int? ChatId { get; set; }
        public Chat? Chat { get; set; }
    }
}
