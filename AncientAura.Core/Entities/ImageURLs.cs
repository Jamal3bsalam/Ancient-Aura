using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class ImageURLs : BaseEntitiy<int>
    {
        public string? ImageUrl { get; set; }
        public int? AncientSitesId { get; set; }
        public AncientSites? AncientSites { get; set; }
    }
}
