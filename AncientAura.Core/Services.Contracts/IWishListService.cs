using AncientAura.Core.Dtos.WishlistDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Core.Services.Contracts
{
    public interface IWishListService
    {
        Task<WishlistDto> GetWishlistAsync(string userId);
        Task<bool> AddToWishlistAsync(string userId, int? ancientSiteId);
        Task<bool> RemoveFromWishlistAsync(string userId, int ItemId);
        Task<bool> ClearWishlistAsync(string userId);

    }
}
