using AncientAura.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class Reviews : BaseEntitiy<int>
    {
        public string? UserName { get; set; }
        public int? Rating { get; set; } // 1 - 5 scale
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? AncientSitesId { get; set; }
        [JsonIgnore]
        public AncientSites? AncientSites { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser  { get; set; }
        public int? BooksId { get; set; }
        [JsonIgnore]
        public Books? Books { get; set; }
        public int? DocumentriesId { get; set; }
        [JsonIgnore]
        public Documentries? Documentries { get; set; }
        public int? ArticlesId { get; set; }
        [JsonIgnore]
        public Articles? Articles { get; set; }
    }
}
