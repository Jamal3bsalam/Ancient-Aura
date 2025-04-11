using AncientAura.Core.Entities.WishLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Repositories.Contracts
{
    public interface IWishListRepository:IGenericRepository<WishList,int>
    {
        Task<WishList> GetWithItemsAsync(string userId);
    }
}
