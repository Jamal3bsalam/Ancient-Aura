using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.ReviewSpecification
{
    public class ReviewSpecificationForArticle : Specification<Reviews,int>
    {
        public ReviewSpecificationForArticle(int id):base(R => R.ArticlesId == id)
        {
            
        }
    }
}
