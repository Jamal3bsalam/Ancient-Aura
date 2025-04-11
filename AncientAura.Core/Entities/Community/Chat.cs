using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class Chat : BaseEntitiy<int>
    {
        public string? Name { get; set; }
        public ICollection<Message>? Messages { get; set; } = new List<Message>();
        public ICollection<UserChat>? UserChats { get; set; } = new List<UserChat>();

    }
}
