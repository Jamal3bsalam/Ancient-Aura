using AncientAura.Core.Entities.WishLists;
using AncientAura.Core.Repositories.Contracts;
using AncientAura.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Repositories
{
    public class WishListRepository:GenericRepository<WishList,int>,IWishListRepository
    {
        private readonly AncientAuraDbContext _context;

        public WishListRepository(AncientAuraDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<WishList> GetWithItemsAsync(string userId)
        {
            return await _context.WishLists.Where(W => W.AppUserId == userId).Include(W => W.Items).ThenInclude(I => I.AncientSites).FirstOrDefaultAsync();
        }
    }
}
