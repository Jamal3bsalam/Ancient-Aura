using AncientAura.Core.Entities;
using AncientAura.Core.Entities.Community;
using AncientAura.Core.Entities.WishLists;
using AncientAura.Core.Repositories.Contracts;
using AncientAura.Core.Specification;
using AncientAura.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntitiy<Tkey>
    {
        private readonly AncientAuraDbContext _context;

        public GenericRepository(AncientAuraDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if(typeof(TEntity) == typeof(Books))
            {
               return (IEnumerable<TEntity>) await _context.Set<Books>().Include(A => A.Reviews).ToListAsync();
            }

            if (typeof(TEntity) == typeof(Articles))
            {
                return (IEnumerable<TEntity>)await _context.Set<Articles>().Include(A => A.Reviews).ToListAsync();
            }

            if (typeof(TEntity) == typeof(Documentries))
            {
                return (IEnumerable<TEntity>)await _context.Set<Documentries>().Include(A => A.Reviews).ToListAsync();
            }

            if (typeof(TEntity) == typeof(Post))
            {
                return (IEnumerable<TEntity>)await _context.Set<Post>().Include(P => P.User).Include(P => P.Images).Include(P => P.Comments).Include(P => P.Reacts).ToListAsync();
            }
            if (typeof(TEntity) == typeof(Videos))
            {
                return (IEnumerable<TEntity>)await _context.Set<Videos>().ToListAsync();
            }


            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllByIdAsync(Tkey Id)
        {
            if (typeof(TEntity) == typeof(Comment))
            {
                return (IEnumerable<TEntity>)await _context.Set<Comment>().Where(C => C.PostId == Id as int?).Include(C => C.Reacts).Include(C => C.CommentImages).ToListAsync();
            }
            if (typeof(TEntity) == typeof(React))
            {
                return (IEnumerable<TEntity>)await _context.Set<React>().Where(C => C.PostId == Id as int? || C.CommentId == Id as int?).Include(R => R.User).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllRepliesByCommentIdAsync(Tkey Id)
        {
            if (typeof(TEntity) == typeof(Comment))
            {
                return (IEnumerable<TEntity>)await _context.Set<Comment>().Where(C => C.ParentCommentId == Id as int?).Include(C => C.Reacts).Include(C => C.CommentImages).Include(C => C.User).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Tkey Id)
        {
            if (typeof(TEntity) == typeof(Post))
            {
                return await _context.Set<Post>().Where(P => P.Id == Id as int?).Include(P => P.User).Include(P => P.Images).Include(P => P.Comments).ThenInclude(C => C.CommentImages).Include(P => P.Reacts).FirstOrDefaultAsync() as TEntity;
            }

            if (typeof(TEntity) == typeof(Comment))
            {
                return await _context.Set<Comment>().Where(C => C.Id == Id as int?).Include(C => C.Reacts).Include(C => C.CommentImages).FirstOrDefaultAsync() as TEntity;
            }

            if (typeof(TEntity) == typeof(React))
            {
                return await _context.Set<React>().Where(R => R.Id == Id as int?).Include(R => R.User).FirstOrDefaultAsync() as TEntity;
            }
            if (typeof(TEntity) == typeof(Videos))
            {
                return await _context.Set<Videos>().Where(v => v.Id == Id as int?).FirstOrDefaultAsync() as TEntity;
            }

            return await _context.Set<TEntity>().FindAsync(Id);
        }

        public async Task AddAsync(TEntity entity)
        {
             await _context.AddAsync(entity);
        }

      
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, Tkey> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, Tkey> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity>ApplySpecification(ISpecification<TEntity,Tkey> spec)
        {
            return SpecificationEvaluator<TEntity,Tkey>.GetQuery(_context.Set<TEntity>(), spec);
        }

        public Task<int> GetCountAsync(ISpecification<TEntity, Tkey> spec)
        {
            return ApplySpecification(spec).CountAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
          return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetTheMostViewAsync(int count)
        {
            if (typeof(TEntity) == typeof(AncientSites))
            {
               return (IEnumerable<TEntity>)await _context.AncientSites.OrderByDescending(A => A.ViewCount).Take(count).Include(A => A.Reviews).ToListAsync();
            }
            if (typeof(TEntity) == typeof(Books))
            {
               return (IEnumerable<TEntity>)await _context.Books.OrderByDescending(B => B.ViewCount).Take(count).Include(B => B.Reviews).ToListAsync();
            }
            if (typeof(TEntity) == typeof(Articles))
            {
               return (IEnumerable<TEntity>)await _context.Articles.OrderByDescending(A => A.ViewCount).Take(count).Include(A => A.Reviews).ToListAsync();
            }
            if (typeof(TEntity) == typeof(Documentries))
            {
               return (IEnumerable<TEntity>)await _context.Documentries.OrderByDescending(D => D.ViewCount).Take(count).Include(D => D.Reviews).ToListAsync();
            }

            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetRecommendedAsync(List<string> siteNames)
        {
            if (typeof(TEntity) == typeof(AncientSites))
            {
                return await _context.AncientSites.Where(site => siteNames.Any(name => site.Name.ToLower().Contains(name.ToLower()))).Cast<TEntity>().ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllForSpecificAsync(string userId)
        {
            if (typeof(TEntity) == typeof(Post))
            {
                return (IEnumerable<TEntity>)await _context.Set<Post>().Where(P => P.UserId == userId).Include(P => P.User).Include(P => P.Images).Include(P => P.Comments).Include(P => P.Reacts).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        
    }
}
