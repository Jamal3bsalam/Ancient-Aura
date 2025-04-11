using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.WishLists
{
    public class WishList : BaseEntitiy<int>
    {
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Items>? Items { get; set; } = new List<Items>();
    }
}
