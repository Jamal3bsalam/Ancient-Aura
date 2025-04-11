using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class Videos : BaseEntitiy<int>  
    {
        public string? Name { get; set; }
        public string? VideoUrl { get; set; }
    }
}
