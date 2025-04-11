using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.CountSpecificaiton
{
    public class DocumntriesCountSpecification:Specification<Documentries,int>
    {
        public DocumntriesCountSpecification(SpecParameters specParameters) : base(S => (string.IsNullOrEmpty(specParameters.Search)) || S.Name.ToLower().Contains(specParameters.Search))
        {
        }
    }
}
