using AncientAura.Core.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.ReviewSpecification
{
    public class ReviewSpecification:Specification<Reviews,int> 
    {
        public ReviewSpecification(int id) : base(entity => entity.BooksId == id)
        {
            
        }
    }
}
