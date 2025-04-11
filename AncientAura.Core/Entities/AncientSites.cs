using AncientAura.Core.Entities.WishLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class AncientSites : BaseEntitiy<int>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public string? VoiceUrl { get; set; }
        public int? ViewCount { get; set; }
        public string? Adddress { get; set; }
        public string? OpeningTime { get; set; }
        public string? ClosedTime { get; set;}
        public ICollection<Items>? Items { get; set; } = new List<Items>();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
        public ICollection<ImageURLs>? ImageURLs { get; set; } = new List<ImageURLs>();
    }
}
