using AncientAura.Core;
using AncientAura.Core.Dtos.AncientSitesDto;
using AncientAura.Core.Dtos.WishlistDto;
using AncientAura.Core.Entities;
using AncientAura.Core.Entities.WishLists;
using AncientAura.Core.Repositories.Contracts;
using AncientAura.Core.Services.Contracts;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Service.Services.WishListService
{
    public class WishListService : IWishListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWishListRepository _wishListRepository;
        private readonly IConfiguration _configuration;

        public WishListService(IUnitOfWork unitOfWork,IMapper mapper,IWishListRepository wishListRepository,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wishListRepository = wishListRepository;
            _configuration = configuration;
        }

        public async Task<WishlistDto> GetWishlistAsync(string userId)
        {
            var wishList = await _wishListRepository.GetWithItemsAsync(userId);
            if (wishList.AppUserId != userId) return null;
            //var wishListDto = _mapper.Map<WishlistDto>(wishList);
            var wishListDto = new WishlistDto()
            {
                Id = wishList.Id,
                AncientSites = wishList.Items.Select(I => new AncientSitesDtos()
                {
                    Id = I.AncientSites.Id,
                    Name = I.AncientSites.Name,
                    PictureUrl = _configuration["BASEURL"] + I.AncientSites.PictureUrl

                }).ToList()                
            };
            return wishListDto;
        }
        public async Task<bool> AddToWishlistAsync(string userId, int? ancientSiteId)
        {
            var wishlist = await _wishListRepository.GetWithItemsAsync(userId);

            if (wishlist == null)
            {
                wishlist = new WishList { AppUserId = userId, Items = new List<Items>() };
                await _unitOfWork.Repository<WishList,int>().AddAsync(wishlist);
            }

            if (!wishlist.Items.Any(i => i.AncientSitesId == ancientSiteId))
            {
                wishlist.Items.Add(new Items { AncientSitesId = ancientSiteId });
                await _unitOfWork.CompleteAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveFromWishlistAsync(string userId, int ancientSiteId)
        {
            var wishlist = await _wishListRepository.GetWithItemsAsync(userId);
            if (wishlist != null)
            {
                var item = wishlist.Items.FirstOrDefault(I => I.AncientSitesId == ancientSiteId);
                if(item != null)
                {
                    wishlist.Items.Remove(item);
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
            }

            return false;

        }

        public async Task<bool> ClearWishlistAsync(string userId)
        {
            var wishlist = await _wishListRepository.GetWithItemsAsync(userId);
            if (wishlist != null && wishlist.Items.Any())
            {
                    wishlist.Items.Clear();
                    await _unitOfWork.CompleteAsync();
                    return true;
            }

            return false;
        }

    }
}
