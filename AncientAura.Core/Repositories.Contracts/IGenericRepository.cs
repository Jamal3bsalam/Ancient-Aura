using AncientAura.Core.Dtos.AncientSitesDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Entities.Community;
using AncientAura.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Repositories.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntitiy<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllByIdAsync(TKey Id);
        Task<IEnumerable<TEntity>> GetAllRepliesByCommentIdAsync(TKey Id);
        Task<TEntity> GetByIdAsync(TKey Id);
        Task<IEnumerable<TEntity>> GetTheMostViewAsync(int count);
        Task<List<TEntity>> GetRecommendedAsync(List<string> siteNames);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllForSpecificAsync(string userId);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity,TKey> spec);
        Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> spec);
        Task<int> GetCountAsync(ISpecification<TEntity, TKey> spec);
        Task<int> SaveChangesAsync();

    }
}
