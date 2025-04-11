using AncientAura.Core.Dtos.AncientSitesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Dtos.WishlistDto
{
    public class WishlistDto
    {
        public int? Id { get; set; }
        public List<AncientSitesDtos>? AncientSites { get; set; }
    }
}
