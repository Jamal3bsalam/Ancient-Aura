using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification.BooksSpecification
{
    public class BookSpecification : Specification<Books, int>
    {
        public BookSpecification(int id) : base(B => B.Id == id)
        {
            ApplyInclude();
        }

        public BookSpecification(SpecParameters specParameters):base( S => (string.IsNullOrEmpty(specParameters.Search)) || S.Name.ToLower().Contains(specParameters.Search))
        {
            if (!string.IsNullOrEmpty(specParameters.sort))
            {
                switch (specParameters.sort)
                {
                    case "nameAsc":
                        AddOrderBy(B => B.Name);
                        break;
                    case "nameDecs":
                        AddOrderByDesc(B => B.Name);
                        break;
                    default:
                        AddOrderBy(B => B.Name);
                        break;
                }
            }
            else
            {
                OrderBy = P => P.Name;
            }

            ApplyInclude();

            if (specParameters.pageIndex.HasValue && specParameters.pageSize.HasValue)
            {
                ApplyPagination(specParameters.pageSize.Value * (specParameters.pageIndex.Value - 1), specParameters.pageSize.Value);
            }

        }

        public void ApplyInclude()
        {
            Include.Add(B => B.Reviews);
        }


    }
}
