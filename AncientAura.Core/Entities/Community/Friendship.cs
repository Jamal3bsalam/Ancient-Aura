using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Entities.Community
{
    public class Friendship : BaseEntitiy<int>
    {
        public string? Status { get; set; }  // حالة الطلب ("Pending", "Accepted", "Rejected")

    }
}
