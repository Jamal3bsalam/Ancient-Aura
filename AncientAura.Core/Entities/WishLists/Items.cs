using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.WishLists
{
    public class Items:BaseEntitiy<int>
    {
        public int? WishListId { get; set; }
        public WishList? WishList { get; set; }
        public int? AncientSitesId { get; set; }
        public AncientSites? AncientSites { get; set; }
    }
}
