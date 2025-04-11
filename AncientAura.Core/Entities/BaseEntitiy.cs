using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities
{
    public class BaseEntitiy<Tkey>
    {
        public Tkey Id { get; set; }
    }
}
