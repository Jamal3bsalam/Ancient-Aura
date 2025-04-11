using AncientAura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Specification
{
    public class Specification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntitiy<Tkey>
    {
        public List<Expression<Func<TEntity, object>>> Include { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDescending { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }

        public Specification(Expression<Func<TEntity,bool>> expression)
        {
            Criteria = expression;
        }

        public Specification()
        {

        }

        public void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }

        public void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDescending = expression;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }

    }
}
