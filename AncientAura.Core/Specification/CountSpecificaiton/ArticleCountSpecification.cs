using AncientAura.Core.Entities;
using AncientAura.Core.Specification.ArticlesSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.CountSpecificaiton
{
    public class ArticleCountSpecification:Specification<Articles,int>
    {
        public ArticleCountSpecification(SpecParameters specParameters) : base(S => (string.IsNullOrEmpty(specParameters.Search)) || S.Name.ToLower().Contains(specParameters.Search))
        {
        }
    }
}
