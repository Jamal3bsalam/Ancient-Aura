using AncientAura.Core.Entities;
using AncientAura.Core.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
       IGenericRepository<TEntity,Tkey> Repository<TEntity,Tkey>() where TEntity : BaseEntitiy<Tkey>;
    }
}
