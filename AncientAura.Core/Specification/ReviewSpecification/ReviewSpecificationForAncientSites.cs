using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.ReviewSpecification
{
    public class ReviewSpecificationForAncientSites:Specification<Reviews,int>
    {
        public ReviewSpecificationForAncientSites(int id):base(A => A.AncientSitesId == id)
        {
            
        }
    }
}
