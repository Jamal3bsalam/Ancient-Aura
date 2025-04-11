using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.CountSpecificaiton
{
    public class AncientSiteCountSpecification:Specification<AncientSites,int>
    {
        public AncientSiteCountSpecification(SpecParameters specParameters) : base(S => (string.IsNullOrEmpty(specParameters.Search)) || S.Name.ToLower().Contains(specParameters.Search))
        {
        }
    }
}
