using AncientAura.Core;
using AncientAura.Core.Entities;
using AncientAura.Core.Repositories.Contracts;
using AncientAura.Repository.Data.Contexts;
using AncientAura.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AncientAuraDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(AncientAuraDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntitiy<Tkey>
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity,Tkey>(_context);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity,Tkey> ;
        }
    }
}
