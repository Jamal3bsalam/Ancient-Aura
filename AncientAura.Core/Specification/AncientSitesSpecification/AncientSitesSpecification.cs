using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.AncientSitesSpecification
{
    public class AncientSitesSpecification:Specification<AncientSites,int>
    {
        public AncientSitesSpecification(int id) : base(A => A.Id == id)
        {
            ApplyInclude();
        }

        public AncientSitesSpecification(SpecParameters specParameters) : base(S => (string.IsNullOrEmpty(specParameters.Search)) || S.Name.ToLower().Contains(specParameters.Search))
        {
            if (!string.IsNullOrEmpty(specParameters.sort))
            {
                switch (specParameters.sort)
                {
                    case "nameAsc":
                        AddOrderBy(D => D.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(D => D.Name);
                        break;
                    default:
                        AddOrderBy(D => D.Name);
                        break;
                }
            }
            else
            {
                OrderBy = D => D.Name;
            }
            ApplyInclude();

            if (specParameters.pageIndex.HasValue && specParameters.pageSize.HasValue)
            {
                ApplyPagination(specParameters.pageSize.Value * (specParameters.pageIndex.Value - 1), specParameters.pageSize.Value);
            }
        }

        public void ApplyInclude()
        {
            Include.Add(A => A.ImageURLs);
            Include.Add(A => A.Reviews);
        }
    }
}

