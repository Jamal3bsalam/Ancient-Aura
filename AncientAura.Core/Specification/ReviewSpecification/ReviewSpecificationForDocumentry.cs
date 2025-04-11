using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.ReviewSpecification
{
    public class ReviewSpecificationForDocumentry:Specification<Reviews,int>
    {
        public ReviewSpecificationForDocumentry(int id):base(R => R.DocumentriesId == id)
        {
            
        }
    }
}
